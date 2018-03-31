using System.Threading.Tasks;
using GlowGlider.Shared;

namespace GlowGlider.Server.Data
{
    public interface IAnalyticsRepository
    {
        Task StoreDataAsync(AnalyticsData data);
    }
}