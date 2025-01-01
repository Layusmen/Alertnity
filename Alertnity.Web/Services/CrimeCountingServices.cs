using Alertnity.Web.Models;

namespace Alertnity.Web.Services
{
    public class CrimeCountingServices
    {
        private readonly AppState _appState;
        public CrimeCountingServices(AppState appState)
        {
            _appState = appState;
        }
        public void CountCategories()
        {
            _appState.categoryCounts = _appState.crimeResults
                  .GroupBy(ci => ci.category)
                  .ToDictionary(g => g.Key, g => g.Count());
        }
        public void CountStreets()
        {
            _appState.streetCounts = _appState.crimeResults
                .GroupBy(ci => new { ci.location?.street?.Name, ci.location?.latitude, ci.location?.longitude })
                .Select(g => new StreetCrimeData
                {
                    StreetName = g.Key.Name,
                    CrimeCount = g.Count(),
                    Latitude = g.Key.latitude,
                    Longitude = g.Key.longitude,
                    Crimes = string.Join(", ", g.Select(ci => ci.category)),
                    CrimeCountsByType = g.GroupBy(ci => ci.category).ToDictionary(grp => grp.Key, grp => grp.Count())
                }).ToList();
        }
        public void CountCategoriesArchive()
        {
            _appState.categoryCounts = _appState.archiveDataResult
                .GroupBy(ci => ci.CrimeType)
                .ToDictionary(g => g.Key, g => g.Count());
        }
        public void CountStreetsArchive()
        {
            _appState.streetCounts = _appState.archiveDataResult
                .GroupBy(ci => new { ci.Street, ci.Latitude, ci.Longitude })
                .Select(g => new StreetCrimeData
                {
                    StreetName = g.Key.Street,
                    CrimeCount = g.Count(),
                    Latitude = g.Key.Latitude.ToString(),
                    Longitude = g.Key.Longitude.ToString(),
                    Crimes = string.Join(", ", g.Select(ci => ci.CrimeType)),
                    CrimeCountsByType = g.GroupBy(ci => ci.CrimeType).ToDictionary(grp => grp.Key, grp => grp.Count())
                }).ToList();
        }
    }
}
