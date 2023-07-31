using API.CustomExceptions;
using API.ExtAuthorization;
using API.Extension;
using API.Utils;
using Common.Constants;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Net;

namespace API.Areas.Admin.Controllers;
[Route("[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountsController(IAccountService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        if (!ModelState.IsValid)
        {
            throw new InvalidModelStateException(ModelState);
        }

        TokenDTO token = await _service.LoginAsync(dto);

        return new SuccessResponseHelper<TokenDTO>()
               .GetSuccessResponse((int)HttpStatusCode.OK, SuccessMessage.LOGIN_SUCCESS, token);
    }

    [HttpGet("logout")]
    [ExtAuthorize]
    public IActionResult Logout()
    {
        string token = HttpContext.GetAuthToken();
        _service.Logout(token);
        return new SuccessResponseHelper<object>()
               .GetSuccessResponse((int)HttpStatusCode.OK, SuccessMessage.LOGOUT_SUCCESS);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
    {
        await _service.ForgotPasswordAsync(dto);

        return new SuccessResponseHelper<object>()
            .GetSuccessResponse((int)HttpStatusCode.OK, SuccessMessage.FORGOT_PASSWORD_MAIL_SENT);
    }


    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(string token, [FromBody] ResetPasswordDTO dto)
    {
        if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

        await _service.ResetPasswordAsync(token, dto);

        return new SuccessResponseHelper<object>()
            .GetSuccessResponse((int)HttpStatusCode.OK, SuccessMessage.PASSWORD_CHANGE_SUCCESS);
    }


    #region Volunteer

    [HttpPost("registration")]
    public async Task<IActionResult> Registration(VolunteerRegistrationDTO dto)
    {
        if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

        await _service.VolunteerRegistration(dto);

        return new SuccessResponseHelper<object>()
            .GetSuccessResponse((int)HttpStatusCode.Created, SuccessMessage.REGISTRATION_VOLUNTEER);
    }
    #endregion
}