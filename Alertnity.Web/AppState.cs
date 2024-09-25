using Alertnity.ArchiveDataAnalysis;
using Alertnity.PoliceApi;
using Alertnity.Web.Models;

namespace Alertnity.Web
{
    public class AppState
    {
        public CrimeCheckModel formInfo { get; set; } = new();
        public List<CrimeInfo> crimeResults { get; set; } = new();
        public List<CrimeRecord> archiveDataResult { get; set; } = new();
        public Dictionary<string, int> categoryCounts { get; set; } = new();
        public List<StreetCrimeData> streetCounts { get; set; } = new();
        public bool showMap { get; set; } = false; //
    }
}