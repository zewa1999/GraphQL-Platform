using PlatformsGraphQL.Models;
using System.Reflection.Metadata.Ecma335;

namespace PlatformsGraphQL.GraphQL.Platforms;

public class Subscription
{
    [Subscribe]
    [Topic]
    public Platform OnPlatformAdded([EventMessage] Platform platform)
    {
        return platform;
    }
}
