using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemRD1.Api.Extension;
using SystemRD1.Business.Contracts.Notifiers;
using SystemRD1.Business.Notifications;
using Microsoft.AspNetCore.Http;

namespace SystemRD1.Api.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected readonly INotifier _notifier;

        public ApiController(INotifier notifier)
        {
            _notifier = notifier;
        }



        private bool IsValidOperation()
        {
            return !_notifier.HaveNotification();
        }

        protected void NotifyError(string message)
        {
            _notifier.Handle(new Notification(message));
        }


        protected ActionResult<IEnumerable<T>> ResponseGet<T>(IEnumerable<T> result)
        {
            if (result == null || !result.Any())
                return NoContent();

            return Ok(result);
        }

        protected ActionResult<T> ResponseGet<T>(T result)
        {
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        protected ActionResult ResponsePut()
        {
            if (IsValidOperation())
                return NoContent();

            var notifications = _notifier.GetNotifications();
            var errors = new List<string>();

            foreach (var item in notifications)
            {
                errors.Add(item.Message);
            }

            return BadRequest(new ResponseResultErrors
            {
                Title = "Error",
                Status = StatusCodes.Status400BadRequest,
                Errors = new ResponseResultMessageErrors
                {
                    Messages = errors
                }
            });
        }

        protected ActionResult ResponseDelete(Guid id)
        {
            if(IsValidOperation())
            {
                if (id == null)
                    return NoContent();

                return Ok(id);
            }

            var notifications = _notifier.GetNotifications();
            var errors = new List<string>();

            foreach (var item in notifications)
            {
                errors.Add(item.Message);
            }

            return BadRequest(new ResponseResultErrors
            {
                Title = "Error",
                Status = StatusCodes.Status400BadRequest,
                Errors = new ResponseResultMessageErrors
                {
                    Messages = errors
                }
            });
        }


        protected ActionResult ResponsePost(object result = null)
        {
            if(IsValidOperation())
            {
                if (result == null)
                    return NoContent();

                return Ok(result);
            }

            var notifications = _notifier.GetNotifications();
            var errors = new List<string>();

            foreach(var item in notifications)
            {
                errors.Add(item.Message);
            }

            return BadRequest(new ResponseResultErrors
            {
                Title = "Error",
                Status = StatusCodes.Status400BadRequest,
                Errors = new ResponseResultMessageErrors
                {
                    Messages = errors
                }
            });
        }

        protected void NotifyInvalidModelError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var e in errors)
            {
                var errorMessage = e.Exception == null ? e.ErrorMessage : e.Exception.Message;
                NotifyError(errorMessage);
            }
        }

        protected ActionResult ModelStateErrorResponseError()
        {
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

    }
}
