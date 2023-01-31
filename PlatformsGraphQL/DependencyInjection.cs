using GraphQL.Server.Ui.Voyager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformsGraphQL.Data;
using PlatformsGraphQL.GraphQL;
using PlatformsGraphQL.GraphQL.Commands;
using PlatformsGraphQL.GraphQL.Platforms;

namespace PlatformsGraphQL;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddSqlServerDependecies(configurationManager);
        services.AddGraphQLServer();

        return services;
    }

    public static WebApplication AddGraphQL(this WebApplication app)
    {
        var voyagerOptions = new VoyagerOptions
        {
            GraphQLEndPoint = "/graphql"
        };

        app.MapGraphQL();
        app.UseGraphQLVoyager("/graphql-voyager", voyagerOptions);
        app.UseWebSockets();
        return app;
    }

    public static IServiceCollection AddGraphQLServer(this IServiceCollection services)
    {
        HotChocolateAspNetCoreServiceCollectionExtensions.AddGraphQLServer(services)
            .AddQueryType<Query>()
            .AddType<PlatformType>()
            .AddType<CommandType>()
            .AddMutationType<Mutation>()
            .AddSubscriptionType<Subscription>()
            .AddFiltering()
            .AddSorting()
            .AddInMemorySubscriptions();

        return services;
    }

    public static IServiceCollection AddSqlServerDependecies(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var connectionString = configurationManager.GetConnectionString("SqlServerConnectionString");
        services.AddPooledDbContextFactory<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }

}
