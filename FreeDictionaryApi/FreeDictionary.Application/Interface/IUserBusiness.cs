using FreeDictionary.Application.Model;

namespace FreeDictionary.Application.Interface
{
    public interface IUserBusiness
    {
        Task<(UserModel, bool)> GetProfileAsync(string userId);
        Task<(PaginationModel<UserWordAddedModel>, bool)> GetHistoryAsync(string userId, int page, int limit);
        Task<(PaginationModel<UserWordAddedModel>, bool)> GetFavoritesAsync(string userId, int page, int limit);
    }
}
