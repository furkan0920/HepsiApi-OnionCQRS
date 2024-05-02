using Hepsi.Application.Bases;

namespace Hepsi.Application.Features.Auth.Exceptions
{
    public class EmailOrPasswordShouldNotBeInvalidException : BaseException
    {
        public EmailOrPasswordShouldNotBeInvalidException() : base("kullanıcı adı veya şifre yanlistir.") { }
    }
}
