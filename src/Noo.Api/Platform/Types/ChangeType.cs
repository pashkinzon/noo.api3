namespace Noo.Api.Platform.Types;

public enum ChangeType
{
    /// <summary>
    /// Represents a new feature added to the platform.
    /// </summary>
    Feature,

    /// <summary>
    /// Represents a bug fix in the platform.
    /// </summary>
    BugFix,

    /// <summary>
    /// Represents an improvement or enhancement made to the platform.
    /// </summary>
    Optimization,

    /// <summary>
    /// Represents a refactor of existing code without changing its external behavior.
    /// </summary>
    Refactor
}
