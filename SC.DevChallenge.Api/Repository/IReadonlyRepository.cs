using System;
using System.Collections.Generic;

namespace SC.DevChallenge.Api.Repository
{
    public interface IReadonlyRepository<T>
    {
        T GetById(Guid Id);

        IEnumerable<T> GetAll();
    }
}
