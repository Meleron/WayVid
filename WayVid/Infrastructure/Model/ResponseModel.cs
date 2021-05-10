using System.Collections.Generic;
using WayVid.Infrastructure.Interfaces.Core;

namespace WayVid.Infrastructure.Model
{
    public class ResponseModel : IModel
    {
        bool IsSucceeded { get; set; }
        List<object> Errors { get; set; }
    }
}
