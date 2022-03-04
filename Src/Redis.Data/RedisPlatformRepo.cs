namespace Redis.Data;

public class RedisPlatformRepo : IPlatformRepo
{
    private IConnectionMultiplexer Redis { get; }

    public RedisPlatformRepo(IConnectionMultiplexer redis)
    {
        Redis = redis;
    }

    Task IPlatformRepo.CreatePlatform(Platform platform, CancellationToken cancellationToken)
    {
        if (platform == null)
        {
            var e = new ArgumentOutOfRangeException(nameof(platform));
            Task.FromException(e);
            throw e;
        }
        IDatabase db = Redis.GetDatabase();
        string serialPlat = JsonSerializer.Serialize(platform);
        //db.StringSet(platform.Id, serialPlat);
        //db.SetAdd("PlatformSet",serialPlat);
        db.HashSet("hashplatform", new HashEntry[]{
            new(platform.Id, serialPlat)
        });
        return Task.CompletedTask;
    }

    IEnumerable<Platform?>? IPlatformRepo.GetAllPlatforms()
    {
        var db = Redis.GetDatabase();
        //var completeSet = db.SetMembers("PlatformSet");
        var completeHash = db.HashGetAll("hashplatform");
        if (!completeHash.Any()) return null;
        return Array.ConvertAll(completeHash, val => JsonSerializer.Deserialize<Platform>(val.Value)).ToList();
    }

    Platform? IPlatformRepo.GetPlatformById(string id)
    {
        IDatabase db = Redis.GetDatabase();
        //var platform = db.StringGet(id);
        var platform = db.HashGet("completeHash",id);
        if (string.IsNullOrEmpty(platform))
        {
            return null;
        }
        return JsonSerializer.Deserialize<Platform>(platform);
    }
}
