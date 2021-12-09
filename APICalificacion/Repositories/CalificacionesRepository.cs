using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICalificacion.Repositories
{
    public class CalificacionesRepository<T> where T : class
    {
        public virtual DbContext Context { get; }
        //public alumnosContext Context { get; }

        public CalificacionesRepository(/*alumnosContext context*/DbContext context)
        {
            Context = context;
        }

        public virtual void Insert(T entidad)
        {
            Context.Add(entidad);
            Context.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public virtual T Get(object id)
        {
            return Context.Find<T>(id);
        }

        public virtual T GetMaterias()
        {
            return Context.Find<T>();
        }

        public virtual void Update(T entidad)
        {
            Context.Update(entidad);
            Context.SaveChanges();
        }

        public virtual void Delete(T entidad)
        {
            Context.Remove(entidad);
            Context.SaveChanges();
        }
    }
}
