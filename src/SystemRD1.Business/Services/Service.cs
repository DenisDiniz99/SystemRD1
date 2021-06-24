using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using SystemRD1.Business.Contracts.Notifiers;
using SystemRD1.Business.Notifications;
using SystemRD1.Domain.Entities;

namespace SystemRD1.Business.Services
{
    public abstract class Service
    {
        private readonly INotifier _notifier;

        public Service(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationResult)
        {    
            foreach(var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected bool PerformValidation<TValidation, TEntity>(TValidation validation, TEntity entity) 
                                        where TValidation : AbstractValidator<TEntity> where TEntity : Entity
        {
            var result = validation.Validate(entity);

            if (result.IsValid) return true;

            Notify(result);

            return false;
        }
    }
}
