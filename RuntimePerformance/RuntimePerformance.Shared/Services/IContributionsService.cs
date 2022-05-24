using RuntimePerformance.Shared.Models;
using System.ServiceModel;

namespace RuntimePerformance.Shared.Services
{
    [ServiceContract]
    public interface IContributionsService : IDataService<Contribution>
    {
    }
}
