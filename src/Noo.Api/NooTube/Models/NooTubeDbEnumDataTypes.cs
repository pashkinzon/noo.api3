namespace Noo.Api.NooTube.Models;

public static class NooTubeDbEnumDataTypes
{
    public const string NooTubeServiceType = "ENUM('NooTubeServiceType', 'NooTube, YouTube, VkVideo, Rutube')";
    public const string VideoState = "ENUM('VideoState', 'NotUploaded, Uploading, Uploaded, Published')";
    public const string VideoReaction = "ENUM('VideoReaction', 'Like', 'Dislike', 'Heart', 'Laugh', 'Sad', 'Mindblowing')";
}
