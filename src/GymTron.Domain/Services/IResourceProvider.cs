namespace GymTron.Domain.Services;

public interface IResourceProvider
{
    Stream GetResourceStream(string resourceName);
}
