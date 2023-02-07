using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckPlanTools.Models;

namespace TruckPlanTools.Interfaces.Repository
{
    internal interface IRepository<T> where T : EntityId
    {
        T GetById(Guid id);
        void Update(T entity);
        List<T> GetAll();
        void Add(T entity);
    }
}
