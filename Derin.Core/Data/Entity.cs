//using System.ComponentModel.DataAnnotations;

using Derin.Core.Cqrs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Derin.Core.Data
{
    public abstract class Entity 
    {        
        

    }

    public interface IIdentity
    {
        string Value { get; }
    }

    public interface IEntity
    {
        IIdentity GetIdentity();
    }

    public interface IEntity<out TIdentity> : IEntity
        where TIdentity : IIdentity
    {
        TIdentity Id { get; }
    }
    //public abstract class Entity<TIdentity> : ValueObject, IEntity<TIdentity>
    //    where TIdentity : IIdentity
    //{
    //    protected Entity(TIdentity id)
    //    {
    //        if (id == null) throw new ArgumentNullException(nameof(id));

    //        Id = id;
    //    }

    //    public TIdentity Id { get; }

    //    public IIdentity GetIdentity()
    //    {
    //        return Id;
    //    }

    //    protected override IEnumerable<object> GetEqualityComponents()
    //    {
    //        yield return Id;
    //    }
    //}
}
