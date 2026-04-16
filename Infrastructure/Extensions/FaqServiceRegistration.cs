using Application.Faq;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class FaqRegistrationExtension
{
    public static IServiceCollection AddFaqServices(this IServiceCollection services)
    {
        services.AddScoped<IFaqService, FaqService>();
        return services;
    }
}
