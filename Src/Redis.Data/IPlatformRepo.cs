namespace Redis.Data;

public interface IPlatformRepo
{
    Task CreatePlatform(Platform platform,CancellationToken cancellationToken);
    Platform? GetPlatformById(string id);
    IEnumerable<Platform?>? GetAllPlatforms();
}
