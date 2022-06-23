namespace service_scheduler.Models
{
    /// <summary>
    /// This class reads the api settings from the appsettings, which used to call the sftp api service
    /// </summary>
    public class ApiSettingsModel
    {
        public string? APIBaseURL { get; set; }
    }
}
