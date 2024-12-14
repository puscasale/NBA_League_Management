using Jr._NBA_League_Romania.Domain;

namespace Jr._NBA_League_Romania.Repository;

using System;
using System.Collections.Generic;

public interface IRepository<ID, E> where E : Entity<ID>
{
    E FindOne(ID id);

    IEnumerable<E> FindAll();

    E Save(E entity);

    bool Delete(ID id);
} 