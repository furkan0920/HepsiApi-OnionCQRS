using Hepsi.Application.Bases;

namespace Hepsi.Application.Features.Auth.Exceptions
{
    public class RefreshTokenShouldNotExpiredException : BaseException
    {
        public RefreshTokenShouldNotExpiredException() : base("Oturum süresi sona ermiştir. Lütfen tekrar giriş yapın.") { }
    }
}
