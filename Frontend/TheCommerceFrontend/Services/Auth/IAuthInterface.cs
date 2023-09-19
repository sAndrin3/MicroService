using TheCommerceFrontend.Models;
using TheCommerceFrontend.Services.Auth;
using TheCommerceFrontend.Models.Auth;

namespace TheCommerceFrontend.Services.Auth{
    public interface IAuthInterface{
        Task<ResponseDto> Register(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> Login (LoginRequestDto loginRequestDto);
    }
}