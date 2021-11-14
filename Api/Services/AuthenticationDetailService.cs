using Api.Claims;
using Data;
using Data.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Services
{
    public class AuthenticationDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationDetailService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetAuthenticationProviderUserIdOrDefault(ClaimsPrincipal principal)
        {
            return principal.Claims.Where(c => c.Type.Equals(CustomClaimTypes.UserId, StringComparison.InvariantCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault();
        }

        public async Task<UserProfile> GetUserBasedOnClaimsAsync(ClaimsPrincipal principal)
        {
            var authProviderUserId = GetAuthenticationProviderUserIdOrDefault(principal);

            var user = await _unitOfWork.UserRepository.FindByLocalIdAsync(authProviderUserId);
            if (user == null)
            {
                throw new Exception("UserId not found for currently authenticated user.");
            }

            return user;
        }
    }
}
