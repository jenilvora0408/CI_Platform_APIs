using AutoMapper;
using Common.Constants;
using Common.CustomExceptions;
using Common.Enums;
using Common.Utils;
using Common.Utils.Models;
using DataAccessLayer.Repositories.Interfaces;
using Entities.DataModels;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Services.Interfaces;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Security.Claims;

namespace Services.Implementations;

public class AccountService : BaseService<User>, IAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ITokenBlacklistService _tokenBlacklistService;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostEnvironment _env;

    public AccountService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, ITokenBlacklistService tokenBlacklistService, IEmailService emailService, IHttpContextAccessor httpContextAccessor, IHostEnvironment env) : base(unitOfWork.UserRepo, unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _mapper = mapper;
        _tokenBlacklistService = tokenBlacklistService;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
        _env = env;
    }

    public async Task<TokenDTO> LoginAsync(LoginDTO dto)
    {
        User? user = await GetByEmailAsync(dto.Email);

        if (user == null)
        {
            throw new AuthenticationException(ExceptionMessage.UNAUTHENTICATED);
        }
        if (!VerifyUser(dto, user)) { throw new AuthenticationException(ExceptionMessage.UNAUTHENTICATED); }

        JwtSetting jwtSetting = GetJwtSetting();
        _configuration.GetSection(SystemConstant.JWT_SETTING).Bind(jwtSetting);
        SessionUserModel currentUser = _mapper.Map<SessionUserModel>(user);

        string authToken = JwtHelper.GenerateToken(jwtSetting, currentUser);

        return new TokenDTO() { UserName = $"{user.FirstName} {user.LastName}", UserType = user.UserType, Token = authToken };
    }

    public void Logout(string token)
    {
        DateTime expireToken = JwtHelper.GetTokenExpirationTime(token);
        _tokenBlacklistService.AddTokenToBlacklist(token, expireToken);
    }

    public async Task ForgotPasswordAsync(ForgotPasswordDTO dto)
    {
        User? user = await GetByEmailAsync(dto.Email);
        if (user == null)
        {
            return;
        }

        ResetPasswordJwtSetting setting = GetResetPasswordJwtSetting();
        ResetPasswordModel model = SetResetPasswordModel(user.Id, setting.ExpiryMinutes);

        string token = JwtHelper.GenerateToken(setting, model);

        user.Token = token;
        user.ModifiedOn = DateTime.UtcNow;

        string url = GetResetPasswordUrl(token);

        EmailMessage emailMessage = new EmailMessage(new string[] { user.Email }, SystemConstant.EMAIL_HEADING_RESET_PASSWORD, url);
        await _emailService.SendEmailAsync(emailMessage);

        _unitOfWork.UserRepo.Update(user);
        await _unitOfWork.SaveAsync();

    }

    public async Task ResetPasswordAsync(string token, ResetPasswordDTO dto)
    {
        ResetPasswordJwtSetting setting = GetResetPasswordJwtSetting();

        ResetPasswordModel model = GetResetPasswordModel(setting, token);

        User user = await _unitOfWork.UserRepo.GetByIdAsync(model.Id);

        //check if there is token with associated user in db
        if (user.Token != token) throw new AuthenticationException(ExceptionMessage.UNAUTHORIZED);

        user.ModifiedOn = DateTime.UtcNow;
        user.Token = null;

        if (JwtHelper.IsTokenExpired(token))
        {
            _unitOfWork.UserRepo.Update(user);
            await _unitOfWork.SaveAsync();

            throw new AuthenticationException(ExceptionMessage.RESET_PASSWORD_TOKEN_EXPIRED);
        }

        byte[] salt;
        user.Password = PasswordHelper.HashPassword(dto.Password, out salt);
        user.Salt = Convert.ToHexString(salt);
        _unitOfWork.UserRepo.Update(user);
        await _unitOfWork.SaveAsync();
    }

    private async Task<User?> GetByEmailAsync(string email)
    {
        User? user = await _unitOfWork.UserRepo.GetAsync(u => u.Email == email, null);
        return user;
    }

    private static bool VerifyUser(LoginDTO dto, User user) => PasswordHelper.VerifyPassword(dto.Password, user.Password, Convert.FromHexString(user.Salt));


    private JwtSetting GetJwtSetting()
    {
        JwtSetting jwtSetting = new JwtSetting();
        _configuration.GetSection(SystemConstant.JWT_SETTING).Bind(jwtSetting);
        return jwtSetting;
    }

    private ResetPasswordJwtSetting GetResetPasswordJwtSetting()
    {
        ResetPasswordJwtSetting resetPasswordJwtSetting = new();
        _configuration.GetSection(SystemConstant.RESET_PASSWORD_JWT_SETTING).Bind(resetPasswordJwtSetting);
        return resetPasswordJwtSetting;
    }

    private ResetPasswordModel SetResetPasswordModel(long id, int expiryMinutes)
    {
        DateTime validTill = DateTime.UtcNow.AddMinutes(expiryMinutes);

        ResetPasswordModel model = new()
        {
            Id = id,
            VaildTill = validTill,
        };
        return model;
    }

    private ResetPasswordModel GetResetPasswordModel(ResetPasswordJwtSetting setting, string token)
    {
        ClaimsPrincipal? claimsPrincipal = JwtHelper.ValidateJwtToken(setting, token);

        if (claimsPrincipal == null)
        {
            throw new UnauthorizedAccessException();
        }

        Claim? idClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        Claim? validTillClaim = claimsPrincipal.FindFirst("ValidTill");

        if (idClaim == null || validTillClaim == null)
        {
            throw new InvalidOperationException(ExceptionMessage.INVALID_CLAIMS);
        }

        long id = long.Parse(idClaim.Value);
        DateTime validTill = DateTime.Parse(validTillClaim.Value);

        ResetPasswordModel model = new ResetPasswordModel
        {
            Id = id,
            VaildTill = validTill
        };

        return model;
    }

    private string GetResetPasswordUrl(string token)
    {
        HttpRequest request = _httpContextAccessor.HttpContext.Request;
        string baseUrl = $"{request.Scheme}://{request.Host.Value}";
        string url = $"{baseUrl}{SystemConstant.ENDPOINT_RESET_PASSWORD}?token={token}";
        return url;
    }


    #region Volunteer
    public async Task VolunteerRegistration(VolunteerRegistrationDTO dto)
    {
        if (await _unitOfWork.UserRepo.IsEntityExist(user => user.Email == dto.Email)) throw new DataAlreadyExistsException(ExceptionMessage.EMAIL_ALREADY_REGISTERED);
        Volunteer volunteer = new Volunteer()
        {
            User = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = PasswordHelper.HashPassword(dto.Password, out byte[] salt),
                Salt = Convert.ToHexString(salt),
                UserType = UserType.Volunteer,
                Status = UserStatus.ACTIVE
            },
            PhoneNumber = dto.PhoneNumber,
            CreatedBy = 1,
            ModifiedBy = 1,
            CityId = 1
        };

        await _unitOfWork.VolunteerRepo.AddAsync(volunteer);
        await _unitOfWork.SaveAsync();
    }
    #endregion
}