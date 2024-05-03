using Hepsi.Application.Bases;
using Hepsi.Application.Features.Auth.Rules;
using Hepsi.Application.Interface.AutoMapper;
using Hepsi.Application.Interface.Token;
using Hepsi.Application.Interface.UnitOfWorks;
using Hepsi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hepsi.Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandHandler :BaseHandler, IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {
        private readonly AuthRules authRules;
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;
        public RefreshTokenCommandHandler(IMapper mapper,AuthRules authRules, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, ITokenService tokenService) : base(mapper, unitOfWork, httpContextAccessor)
        {
            this.authRules = authRules;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal? principal =tokenService.GetClaimsPrincipalFromExpiredToken(request.AccessToken);
            string email=principal.FindFirstValue(ClaimTypes.Email);

            User? user =await userManager.FindByNameAsync(email);
            IList<string> roles =await userManager.GetRolesAsync(user);
            await authRules.RefreshTokenShouldNotExpired(user.RefreshTokenExpiryTime);
            JwtSecurityToken newAccessToken=await tokenService.CreateToken(user,roles);
            string newRefreshToken = tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await userManager.UpdateAsync(user);
            return new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
            };
        }
    }
}
