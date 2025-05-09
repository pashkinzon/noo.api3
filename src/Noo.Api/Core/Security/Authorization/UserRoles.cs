namespace Noo.Api.Core.Security.Authorization;

public enum UserRoles
{
    /// <summary>
    /// Admin role - has almost all permissions
    /// </summary>
    Admin,

    /// <summary>
    /// Teacher role - almost admin but without dangerous actinos like delete all courses and managing critical entities like subjects
    /// </summary>
    Teacher,

    /// <summary>
    /// Mentor role - mainly for work checks
    /// </summary>
    Mentor,

    /// <summary>
    /// Assistant role - mainly for statistics and small functionalities
    /// </summary>
    Assistant,

    /// <summary>
    /// Student role - default role everyone gets after registration
    /// </summary>
    Student
}
