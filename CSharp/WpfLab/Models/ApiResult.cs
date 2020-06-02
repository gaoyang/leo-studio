using System.Collections.Generic;

namespace WpfLab.Models
{
    public class ApiResult
    {
        public ApiState State { get; set; }
        public string Msg { get; set; }
        public Dictionary<string, string> Errors { get; set; }
        public object Data { get; set; }
    }

    public enum ApiState
    {
        Success = 1000,
        Fail = 1001,
        Invalid = 1002
    }
}
