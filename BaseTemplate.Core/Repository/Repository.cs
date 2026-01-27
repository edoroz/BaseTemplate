using BaseTemplate.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseTemplate.Core.Repository {
    public class Repository<T> : IRepository<T> where T : class {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context) {
            Context = context;
            this.dbSet = context.Set<T>();
        }

        public void Add(T entity) {
            dbSet.Add(entity);
        }

        public void Delete(T entity) {
            dbSet.Remove(entity);
        }

        public void Delete(int id) {
            Delete(GetById(id));
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null) {
            // Crea una consulta IQuerable a partir del DbSet
            IQueryable<T> query = dbSet;

            //Se aplica el filtro si se proporciona
            if(filter != null) {
                query = query.Where(filter);
            }

            // Se incluyen propiedades de navegación si se proporcionan, es decir data relacionada
            if(includeProperties != null) {
                // Se divide la cadena de propiedades por coma y se itera sobre ellas
                foreach(var property in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            // Se aplica el ordenamiento si se proporciona.
            if(orderBy != null) {
                return orderBy(query).ToList();
            }

            // No se aplica el ordenamiento. 
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null) {
            // Crea una consulta IQuerable a partir del DbSet
            IQueryable<T> query = dbSet;

            //Se aplica el filtro si se proporciona
            if(filter != null) {
                query = query.Where(filter);
            }

            // Se incluyen propiedades de navegación si se proporcionan, es decir data relacionada
            if(includeProperties != null) {
                // Se divide la cadena de propiedades por coma y se itera sobre ellas
                foreach(var property in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return query.FirstOrDefault();
        }

        public T GetById<TKey>(TKey id) {
            return dbSet.Find(id);
        }

        public void Update<T>(T dbEntity, T newValues) where T : class {
            Context.Entry(dbEntity).CurrentValues.SetValues(newValues);
        }
    }
}
