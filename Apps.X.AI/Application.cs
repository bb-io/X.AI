using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.X.AI;

public class Application : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.ArtificialIntelligence];
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}