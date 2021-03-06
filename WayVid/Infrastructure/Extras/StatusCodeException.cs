using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WayVid.Infrastructure.Extras
{
    public class StatusCodeException : Exception
    {
        public HttpStatusCode StatusCode;

        public StatusCodeException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

    }
}
