using Microsoft.EntityFrameworkCore;
using RuntimePerformance.Shared.Models;
using RuntimePerformance.Shared.Services;
using RuntimePerformance.WebApi.Models;

namespace RuntimePerformance.WebApi.Services
{
    public class ConferencesService : IConferenceService
    {
        private readonly SampleDatabaseContext _context;

        public ConferencesService(SampleDatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Task<List<Conference>> GetCollectionAsync(CollectionRequest request)
        {
            var result = string.IsNullOrWhiteSpace(request.SearchTerm)
                ? _context.Conferences
                : _context.Conferences.Where(c => c.Title.Contains(request.SearchTerm));
            var take = request.Take > 1000 ? 1000 : request.Take;
            return result.Take(take).ToListAsync();
        }
    }
}
