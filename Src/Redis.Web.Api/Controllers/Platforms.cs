namespace Redis.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Platforms : ControllerBase
{
    private IPlatformRepo PlatformRepo { get; }

    public Platforms(IPlatformRepo platformRepo)
    {
        PlatformRepo = platformRepo;
    }

    [HttpGet("{id}",Name = "GetPlatformById")]
    public ActionResult<Platform?> GetPlatformById(string id)
    {
        var platform = PlatformRepo.GetPlatformById(id);
        if(platform == null)
        {
            return NotFound();
        }
        return Ok(platform);
    }

    [HttpPost]
    public ActionResult<Platform> CreatePlatform(Platform platform,CancellationToken cancellationToken)
    {
        PlatformRepo.CreatePlatform(platform, cancellationToken);
        return CreatedAtRoute(nameof(GetPlatformById), new { platform.Id }, platform);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Platform>> GetAllPlatforms()
    {
        return Ok(PlatformRepo.GetAllPlatforms());
    } 
}
