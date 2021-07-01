using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemRD1.Business.Contracts.Notifiers;
using SystemRD1.Business.Notifications;

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

            return BadRequest(_notifier.GetNotifications());
        }

        protected ActionResult ResponseDelete(Guid id)
        {
            if(IsValidOperation())
            {
                if (id == null)
                    return NoContent();

                return Ok(id);
            }

            return BadRequest(_notifier.GetNotifications());
        }


        protected ActionResult ResponsePost(object result = null)
        {
            if(IsValidOperation())
            {
                if (result == null)
                    return NoContent();

                return Ok(result);
            }

            return BadRequest(_notifier.GetNotifications());
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
