using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.DataAcess.Repositories
{
    public interface IBaseRepository<T> where T:class
    {
        Task<IList<T>> GetAll();
        Task<IList<T>> Find(Expression<Func<T, bool>> expression);
        Task<IList<T>> RawFind(string column, string value);
        Task<IList<T>> RawFind(Dictionary<string, string> columnValuePairs);
        Task<int> Count();
        Task<int> Count(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IList<T> list);
        void Detach(T entity);
        void DetachRange(ICollection<T> list);
        void Delete(T entity);
        void DeleteRange(ICollection<T> list);
        Task<IList<TType>> Selector<TType>(Expression<Func<T, bool>> where, Expression<Func<T, TType>> select);
        Task<T> FindSingle(Expression<Func<T, bool>> expression);
        void UpdateIfModified<T>(T existingEntity, T modifiedExistingEntity, string nameOfPrimaryKeyProperty) where T : class;



    }
}
