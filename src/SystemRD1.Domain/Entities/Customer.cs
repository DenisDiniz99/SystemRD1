using System;
using SystemRD1.Domain.Enums;
using SystemRD1.Domain.ValueObjects;

namespace SystemRD1.Domain.Entities
{
    public class Customer : Entity
    {
        /*Ctor. Default : EF Core*/
        public Customer() { }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public EDocumentType DocumentType { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }  
        public Address Address { get; set; }
        public bool Active { get; set; }
    }
}
