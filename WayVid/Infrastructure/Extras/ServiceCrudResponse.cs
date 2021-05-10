using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Interfaces.Core;

namespace WayVid.Infrastructure.Extras
{
    public class ServiceCrudResponse<TModel> where TModel : class, IModel
    {

        public ServiceCrudResponse(TModel model, bool success, string message)
        {
            Success = success;
            Message = message;
            Model = model;
        }

        public TModel Model { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ServiceCrudResponse
    {

        public ServiceCrudResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
