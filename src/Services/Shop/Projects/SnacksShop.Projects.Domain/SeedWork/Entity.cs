using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SnacksShop.Projects.Domain.SeedWork
{
    public abstract class Entity
    {
        int? _requestedHashCode;
        int _Id;

        private List<INotification> _domainEvents;

        public virtual int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        public List<INotification> DomainEvents => _domainEvents;

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            if (_domainEvents is null) return;

            _domainEvents.Remove(eventItem);
        }

        public bool IsTransient()
        {
            return this.Id == default(Int32);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                {
                    _requestedHashCode = this.Id.GetHashCode() ^ 31;

                }
                return _requestedHashCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }

        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return Object.Equals(right, null) ? true : false;

            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        { 
            return !(left == right);
        } 
    }
}
