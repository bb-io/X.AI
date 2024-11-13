using Blackbird.Applications.Sdk.Common;

namespace Apps.X.AI;

public class Application : IApplication
{
    public string Name
    {
        get => "X.AI";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}