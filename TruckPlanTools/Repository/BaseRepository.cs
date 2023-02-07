using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckPlanTools.Interfaces;
using TruckPlanTools.Models;

namespace TruckPlanTools.Repository
{
    internal abstract class BaseRepository<T> : IRepository<T> where T : EntityId
    {
        internal static List<T> _collection = new List<T>();

        public void Add(T entity)
        {
            _collection.Add(entity);
        }

        public List<T> GetAll()
        {
            return _collection;
            
        }

        public T GetById(Guid id)
        {
            return _collection.FirstOrDefault(x => x.Id == id);

        }

        public void Update(T entity)
        {
            _collection.RemoveAll(x => x.Id == entity.Id);
            _collection.Add(entity);
        }

    }
}
