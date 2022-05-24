using Microsoft.EntityFrameworkCore;
using RuntimePerformance.Shared.Models;
using RuntimePerformance.Shared.Services;
using RuntimePerformance.WebApi.Models;

namespace RuntimePerformance.WebApi.Services
{
    public class SpeakersService : ISpeakersService
    {
        private readonly SampleDatabaseContext _context;

        public SpeakersService(SampleDatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Task<List<Speaker>> GetCollectionAsync(CollectionRequest request)
        {
            var result = string.IsNullOrWhiteSpace(request.SearchTerm)
                ? _context.Speakers
                : _context.Speakers.Where(c => c.FirstName.Contains(request.SearchTerm) || c.LastName.Contains(request.SearchTerm));
            return result.Take(request.Take).ToListAsync();
        }
    }
}
