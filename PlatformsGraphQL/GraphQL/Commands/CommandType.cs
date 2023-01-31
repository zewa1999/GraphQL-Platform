using Microsoft.EntityFrameworkCore;
using PlatformsGraphQL.Data;
using PlatformsGraphQL.Models;

namespace PlatformsGraphQL.GraphQL.Commands;
 
public class CommandType : ObjectType<Command>
{
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Description("Represents any executable command");

        descriptor
            .Field(c => c.Platform)
            .ResolveWith<Resolvers>(c => c.GetPlatform(default!, default!))
            .UseDbContext<AppDbContext>()
            .Description("This is the platform to which the command belongs");
    }

    private class Resolvers
    {
        public Platform GetPlatform([Parent]Command command, [ScopedService] AppDbContext context)
        {
            return context.Platforms.FirstOrDefault(x => x.Id == command.PlatformId);
        }
    }
}
