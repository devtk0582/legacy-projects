using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityWeb.Models
{
    public class UploadStatus
    {
        public int PercentComplete { get; set; }
        public string message { get; set; }
        public string fileName { get; set; }
        public string downloadByte { get; set; }

        public UploadStatus(int percent, string msg, string name, string bytes)
        {
            PercentComplete = percent;
            message = msg;
            fileName = name;
            downloadByte = bytes;
        }
    }
}