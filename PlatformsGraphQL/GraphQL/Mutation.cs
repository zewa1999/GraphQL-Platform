﻿using HotChocolate.Subscriptions;
using PlatformsGraphQL.Data;
using PlatformsGraphQL.GraphQL.Commands;
using PlatformsGraphQL.GraphQL.Platforms;
using PlatformsGraphQL.Models;

namespace PlatformsGraphQL.GraphQL;

public class Mutation
{
    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input,
        [ScopedService] AppDbContext context,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        var platform = new Platform()
        {
            Name = input.Name
        };

        context.Platforms.Add(platform);
        await context.SaveChangesAsync(cancellationToken);

        await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);

        return new AddPlatformPayload(platform);
    }

    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context)
    {
        var command = new Command()
        {
            CommandLine = input.CommandLine,
            HowTo = input.HowTo,
            PlatformId= input.PlatformId
        };

        context.Commands.Add(command);
        await context.SaveChangesAsync();

        return new AddCommandPayload(command);
    }
}
