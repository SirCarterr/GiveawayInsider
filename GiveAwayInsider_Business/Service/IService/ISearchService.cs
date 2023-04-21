using GiveAwayInsider_Models;

namespace GiveAwayInsider_Business.Service.IService
{
    public interface ISearchService
    {
        SearchDTO GetSearchModel(string searchPhrase);
    }
}
