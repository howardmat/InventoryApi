using Data;
using System.Threading.Tasks;

namespace Api.Services
{
    public class UserQueryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserQueryService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int?> GetUserIdOrDefaultByAuthProviderIdAsync(string authProviderUserId)
        {
            int? userId = null;
            var user = await _unitOfWork.UserRepository.FindByLocalIdAsync(authProviderUserId);
            
            return user?.Id ?? userId;
        }
    }
}
