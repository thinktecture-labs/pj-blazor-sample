using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
using RuntimePerformance.Shared.Services;

namespace RuntimePerformance.Contributions
{
    public static class ContributionsFeatureExtensions
    {
        public static void AddContributionsFeature(this IServiceCollection services)
        {
            services.AddScoped(serviceProvider =>
            {
                var channel = serviceProvider.GetRequiredService<GrpcChannel>();
                return serviceProvider.GetRequiredService<GrpcChannel>().CreateGrpcService<IContributionsService>();
            });
        }
    }
}
