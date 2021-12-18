using DeviceManager.DataAcess.EF.AppDBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace DeviceManager.DataAcess.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DeviceManagerDBContext _db;

        public BaseRepository(DeviceManagerDBContext db)
        {
            _db = db;
        }

        private string GetTableName()
        {
            var entityType = typeof(T);
            var modelEntityType = _db.Model.FindEntityType(entityType);

            string tableName = modelEntityType.GetSchemaQualifiedTableName();

            if (tableName == null)
            {
                throw new ArgumentException("There's no coresponding table mapped to the input class");
            }

            return tableName;

        }
        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }
        public void AddRange(IList<T> list)
        {
            _db.Set<T>().AddRange(list);
        }

        public async Task<int> Count()
        {
            return await _db.Set<T>().CountAsync();
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }
        public void DeleteRange(ICollection<T> list)
        {
            _db.Set<T>().RemoveRange(list);
        }


        public async Task<IList<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await _db.Set<T>().Where(expression).ToListAsync();
        }
        public async Task<IList<T>> RawFind(string column, string value)
        {
            var filterItem = new Dictionary<string, string>() {
                {column,value},
            };
            return await this.RawFind(filterItem);
        }
        public async Task<IList<T>> RawFind(Dictionary<string, string> columnValuePairs)
        {
            string tableName = this.GetTableName();

            int pairsNumber = columnValuePairs.Count;
            var columns = columnValuePairs.Keys;
            var values = columnValuePairs.Values;

            var query = $"Select * FROM {tableName} where ";

            for (int i = 0; i < pairsNumber; i++)
            {
                query = query + $" {columns.ElementAt(i)} = '{values.ElementAt(i)}'";

                if (i != pairsNumber - 1)
                    query += "and ";
            }


            return await _db.Set<T>().FromSqlRaw(query).ToListAsync();

        }
        public async Task<T> Get(Object id)
        {
            return await _db.Set<T>().FindAsync(id);
        }
        public async Task<IList<T>> GetAll()
        {
            return await _db.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IList<TType>> Selector<TType>(Expression<Func<T, bool>> where, Expression<Func<T, TType>> select)
        {
            return await _db.Set<T>().Where(where).Select(select).ToListAsync();
        }

        public async Task<T> FindSingle(Expression<Func<T, bool>> expression)
        {
            return (await _db.Set<T>().Where(expression).ToListAsync()).FirstOrDefault();

        }
        public void Detach(T entity)
        {
            _db.Entry(entity).State = EntityState.Detached;
        }

        public void DetachRange(ICollection<T> list)
        {
            foreach (var entity in list)
            {
                this.Detach(entity);
            }
        }


        public async Task<int> Count(Expression<Func<T, bool>> expression)
        {
            return await _db.Set<T>().CountAsync(expression);
        }


        /// <summary>
        /// Modified existing entity's proprieites and changes are reflected when SaveChanges is called.Long story short : Overrides the existing entity with the new entity given as parameter.
        /// Only applies to Value type, string proprieties and nullabels

        /// </summary>
        /// <param name="existingEntity">Entity that was retrieved from DB (actual values live in DB).If it is not tracked by the context already, it will be tracked after calling this method</param>
        /// <param name="modifiedExistingEntity">Entity recieved in http PUT endpoint or which the client knows exist in DB</param>
        /// <param name="nameOfPrimaryKeyProperty">Name of the primary key, the primary key property will be ignored when checking for changes</param>
        public void UpdateIfModified<T>(T existingEntity, T modifiedExistingEntity, string nameOfPrimaryKeyProperty) where T : class
        {
            var proprieties = GetProprietiesExceptPrimaryKey(existingEntity, nameOfPrimaryKeyProperty);


            foreach (var property in proprieties)
            {
                if (IsValueTypeOrString(property) == false)
                    continue;


                object valueExistingEmployee = GetValueForProperty(property, existingEntity);
                object valueExistingEmployeeModified = GetValueForProperty(property, modifiedExistingEntity);

                if (valueExistingEmployee == null)
                    _db.Entry(existingEntity).Property(property.Name).CurrentValue = valueExistingEmployeeModified;


                if (valueExistingEmployee != null && !valueExistingEmployee.Equals(valueExistingEmployeeModified))
                    _db.Entry(existingEntity).Property(property.Name).CurrentValue = valueExistingEmployeeModified;
            }
        }

        PropertyInfo[] GetProprietiesExceptPrimaryKey<T>(T existingEntity, string nameOfPrimaryKeyProperty) where T : class
        {
            PropertyInfo[] propritiesOfExistingEntity = existingEntity.GetType().GetProperties();

            return propritiesOfExistingEntity.Where(property => property.Name != nameOfPrimaryKeyProperty).ToArray();
        }

        object GetValueForProperty<T>(PropertyInfo property, T entity) where T : class
        {
            return typeof(T).GetProperty(property.Name).GetValue(entity);
        }

        /// <summary>
        /// Not taking into consideration if the reference property has changed since only the foreign key prop is relevant.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private static bool IsValueTypeOrString(System.Reflection.PropertyInfo property)
        {
            return (property.PropertyType == typeof(string) || property.PropertyType.IsValueType);
        }
    }
}
