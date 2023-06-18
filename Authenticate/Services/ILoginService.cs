using Authenticate.Models;

namespace Authenticate.Services
{
    public interface ILoginService
    {
        Task<ResponseModel> Login(LoginModel model);
        Task<ResponseModel> RegisterAdminAsync(RegisterModel model);
        Task<ResponseModel?> RegisterOperatorAsync(RegisterModel model);
        Task<ResponseModel?> RegisterUserAsync(RegisterModel model);
        Task<ResponseModel> VerifyLogin(LoginModel model);
    }
}