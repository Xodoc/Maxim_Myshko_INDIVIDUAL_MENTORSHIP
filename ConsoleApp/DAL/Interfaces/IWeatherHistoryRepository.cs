using System.Threading;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWeatherHistoryRepository
    {
        Task AddWeatherHistoryAsync(CancellationToken ct);
    }
}
