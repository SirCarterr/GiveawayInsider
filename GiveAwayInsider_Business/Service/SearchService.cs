using GiveAwayInsider_Models;
using GiveAwayInsider_Business.Service.IService;
using System.Text.RegularExpressions;

namespace GiveAwayInsider_Business.Service
{
    public class SearchService : ISearchService
    {
        private const string platform_pattern = @"\|pf:(?<platform>(\.?[\w\d-]+)+)";
        private const string type_pattern = @"\|t:(?<type>\w+)";
        private const string sortBy_pattern = @"\|s:(?<sort>\w+)";

        private readonly string[] sortValues = { "date", "popularity", "value" };

        public SearchDTO GetSearchModel(string searchPhrase)
        {
            SearchDTO model = new();

            Match match = Regex.Match(searchPhrase, platform_pattern);
            if (match.Success)
                model.Platform = match.Groups["platform"].Value.ToLower();
            
            match = Regex.Match(searchPhrase, type_pattern);
            if (match.Success)
                model.Type = match.Groups["type"].Value.ToLower();

            match = Regex.Match(searchPhrase, sortBy_pattern); 
            if (match.Success)
                if (sortValues.Contains(match.Groups["sort"].Value.ToLower()))
                    model.SortBy = match.Groups["sort"].Value.ToLower();

            model.Search = searchPhrase.Split('|')[0].ToLower();

            return model;
        }

    }
}
