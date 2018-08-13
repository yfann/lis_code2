using System;
using System.Runtime.CompilerServices;

namespace Lis.Domain
{
    public class Entity
    {
        public override bool Equals(object obj)
        {
            if (!((obj != null) && (obj is Entity)))
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            Entity entity = obj as Entity;
            if (entity.IsTransient() || this.IsTransient())
            {
                return false;
            }
            return this.UniqueId.Equals(entity.UniqueId);
        }

        public override int GetHashCode()
        {
            return ((this.UniqueId == null) ? base.GetHashCode() : this.UniqueId.GetHashCode());
        }

        public virtual bool IsTransient()
        {
            return ((this.UniqueId == null) || (this.UniqueId == null));
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (object.Equals(left, null))
            {
                return object.Equals(right, null);
            }
            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        public virtual object UniqueId { get; set; }
    }
}
