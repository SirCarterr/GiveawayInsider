using GiveAwayInsider_Models;

namespace GiveAwayInsider_Client.Service.IService
{
    public interface IAuthenticationService
    {
        Task<SignInResponseDTO> LoginUser(SignInRequestDTO requestDTO);
        Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO requestDTO);
        Task LogoutUser();
        Task<ResponseModelDTO> SendRecoveryLetter(string email);
        Task<ResponseModelDTO> ResetUserPassword(ResetPasswordDTO resetPasswordDTO);
    }
}
