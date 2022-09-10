using FreeDictionary.Application.Model;

namespace FreeDictionary.Application.Interface
{
    public interface IUserBusiness
    {
        Task<UserModel> GetProfileAsync(string userId);
        Task<PaginationModel<UserWordAddedModel>> GetHistoryAsync(string userId, int page, int limit);
        Task<PaginationModel<UserWordAddedModel>> GetFavoritesAsync(string userId, int page, int limit);
    }
}
