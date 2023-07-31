using Entities.DataModels;
using Entities.DTOs;

namespace Services.Interfaces
{
    public interface IAccountService : IBaseService<User>
    {
        Task<TokenDTO> LoginAsync(LoginDTO dto);

        void Logout(string token);

        Task ForgotPasswordAsync(ForgotPasswordDTO dto);

        Task ResetPasswordAsync(string token, ResetPasswordDTO dto);

        Task VolunteerRegistration(VolunteerRegistrationDTO dto);
    }
}
