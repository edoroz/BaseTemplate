using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseTemplate.Core.Interface {
    public interface IRepository<T> where T : class {
        
        T GetById<TKey>(TKey id);
        
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null );
        
        T GetFirstOrDefault(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null );
        
        void Add(T entity);
        
        void Delete(T entity);
        
        void Delete(int id);
        
        void Update<T>(T dbEntity, T newValues) where T : class;
    }
}
