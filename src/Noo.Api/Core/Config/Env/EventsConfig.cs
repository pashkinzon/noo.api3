using System.ComponentModel.DataAnnotations;

namespace Noo.Api.Core.Config.Env;

public class EventsConfig : IConfig
{
    public static string SectionName => "Events";

    [Range(1, 1048576)]
    public int QueueCapacity { get; set; } = 2048;
}
