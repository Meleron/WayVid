using System.Collections.Generic;

namespace WayVid.Infrastructure.Model
{
    public class ResponseModel
    {
        bool IsSucceeded { get; set; }
        List<object> Errors { get; set; }
    }
}
