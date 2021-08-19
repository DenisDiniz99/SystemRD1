using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SystemRD1.WebApp.Extensions;

namespace SystemRD1.WebApp.Controllers
{
    public class WebController: Controller
    {
        protected bool HasErrors()
        {
            return ModelState.ErrorCount == 0;
        }

        protected void AddModelStateErrors(string message)
        {
            ModelState.AddModelError(string.Empty, message);
        }

        protected bool ResponseModelStateErrors(ResponseResult response)
        {
            if(response != null && response.Errors.Messages.Any())
            {
                foreach(var msg in response.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, msg);
                }
                return true;
            }
            return false;
        }
    }
}
