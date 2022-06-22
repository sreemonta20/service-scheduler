using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace service_scheduler.Helpers
{
    /// <summary>
    /// This class contains all the constant values.
    /// </summary>
    public class ConstantSupplier
    {
        /// <summary>
        /// Common Constants
        /// </summary>
        public const string EMPTY_STRING = "";
        public const string STRING_YES = "Y";
        public const string STRING_NO = "N";

        /// <summary>
        /// Common Constant Messages
        /// </summary>
        public const string CANNOT_BE_EMPTY_MSG = " cannot be empty";
        public const string BAD_REQUEST_MSG = "Bad Request";
        public const string INTERNAL_SERVER_ERROR_MSG = "Internal Server Error";
        public const string SUCCESS_MSG = "Success";
        public const string ACCEPTED_MSG = "Accepted";

        /// <summary>
        /// Serilog Log Constants
        /// </summary>
        public const string LOG_INFO_SCHEDULER_START_MSG = "Starting Service Scheduler"; 
        public const string LOG_ERROR_SCHEDULER_TERMINATE_MSG = "Service Scheduler terminated unexpectedly";

        /// <summary>
        /// Name Constants
        /// </summary>
        public const string APP_SETTINGS_FILE_NAME = "appsettings.json";

        /// <summary>
        /// Sftp Service Constants
        /// </summary>
        public const string REMOTE_TYPE_SFTP = "SFTP";
        public const string REMOTE_TYPE_FTP = "FTP";

        /// <summary>
        /// Worker Constants
        /// </summary>
        public const string BACKGROUND_WORK_ERROR_MSG = "Something went wrong in the {0} and exception message is {1}"; 

        /// <summary>
        /// Api related all constants from start to finish.
        /// </summary>
        public const string HTTP_CLIENT_LOGICAL_NAME = "SFTPclient"; 
        public const string API_GET_DOWNLOAD_URL = "/api/SftpService/downloadsftpfiles";
        public const string HTTP_HEADERS_CONTENT_TYPE_NAME = "Accept";
        public const string HTTP_HEADERS_CONTENT_TYPE_VALUE = "application/json";

    }
}
