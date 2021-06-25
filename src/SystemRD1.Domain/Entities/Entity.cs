using System;

namespace SystemRD1.Domain.Entities
{
    public abstract class Entity
    {
        public Entity() { }

        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
