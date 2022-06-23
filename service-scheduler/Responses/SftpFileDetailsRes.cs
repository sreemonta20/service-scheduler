using Newtonsoft.Json;
using service_scheduler.Models;

namespace service_scheduler.Responses
{
    /// <summary>
    /// It carries the service response of sftp service operation.
    /// </summary>
    public class SftpFileDetailsRes
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = String.Empty;
        public List<SftpFileDetails>? ListData { get; set; }
        public SftpFileDetails? Data { get; set; }
        public int? State { get; set; }

    }
}
