using Microsoft.EntityFrameworkCore;
using RuntimePerformance.Shared.Models;
using RuntimePerformance.Shared.Services;
using RuntimePerformance.WebApi.Models;

namespace RuntimePerformance.WebApi.Services
{
    public class ContributionsService : IContributionsService
    {
        private readonly SampleDatabaseContext _context;

        public ContributionsService(SampleDatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Task<List<Contribution>> GetCollectionAsync(CollectionRequest request)
        {
            var result = string.IsNullOrWhiteSpace(request.SearchTerm)
                ? _context.Contributions
                : _context.Contributions.Where(c => c.Title.Contains(request.SearchTerm));
            var take = request.Take > 1000 ? 1000 : request.Take;
            return result.Take(take).ToListAsync();
        }
    }
}
