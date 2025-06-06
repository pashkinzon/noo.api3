namespace Noo.Api.GoogleSheetsIntegrations.Types;

public enum GoogleSheetsIntegrationStatus
{
    /// <summary>
    /// The integration is currently active and running as scheduled
    /// </summary>
    Active,

    /// <summary>
    /// The integration is inactive, meaning it has not been set up or is not currently operational
    /// </summary>
    Inactive,

    /// <summary>
    /// The integration encountered an error during its last run and will not run until the issue is resolved or set manually to active
    /// </summary>
    Error,
}
