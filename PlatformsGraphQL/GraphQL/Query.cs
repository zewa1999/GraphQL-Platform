using PlatformsGraphQL.Data;
using PlatformsGraphQL.Models;

namespace PlatformsGraphQL.GraphQL;

public class Query
{
    [UseDbContext(typeof(AppDbContext))]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext dbContext)
    {
        return dbContext.Platforms;
    }

    [UseDbContext(typeof(AppDbContext))]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Command> GetCommand([ScopedService] AppDbContext dbContext)
    {
        return dbContext.Commands;
    }

}

