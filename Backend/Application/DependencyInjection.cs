using Application.Activities.Queries;
using Application.Activities.Validators;
using Application.Core;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetActivities.Query>());
        services.AddAutoMapper(x => x.AddMaps(typeof(MappingProfiles).Assembly));
        services.AddValidatorsFromAssemblyContaining<CreateActivityValidator>();
        return services;
    }
}