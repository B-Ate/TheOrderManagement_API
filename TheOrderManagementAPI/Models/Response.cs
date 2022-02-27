using System;

namespace TheOrderManagementAPI.Models
{
    public class Response
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public object data { get; set; } = null;
    }
}
