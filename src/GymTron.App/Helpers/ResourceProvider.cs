using GymTron.Domain.Services;
using System.Reflection;

namespace GymTron.App.Helpers;

public class ResourceProvider : IResourceProvider
{
    public Stream GetResourceStream(string resourceName)
    {
        Assembly assembly = typeof(App).GetTypeInfo().Assembly;

        return assembly.GetManifestResourceStream(resourceName) 
            ?? throw new FileNotFoundException($"Resource '{resourceName}' not found.");
    }
}
