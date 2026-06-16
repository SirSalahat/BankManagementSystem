using Azure.Core;
using Azure;
using BMS_DALL.Classes;
using BMS_DALL.DBContextFolder;
using BMS_DALL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DALL.Repository.Classes
{
    public class GenericRepository< T> : IGenericRepository< T> where T : BaseModel
    {
        private readonly ApplicationDb_Context context;

        public GenericRepository(DBContextFolder.ApplicationDb_Context _Context)
        {
            context = _Context;
        }
        public int Add(T entity)
        {
            context.Set<T>().Add(entity);
            return context.SaveChanges();
        }

        public int Delete(T Entity)
        {

            context.Set<T>().Remove(Entity);
        return context.SaveChanges();
        }

        public List<T> GetAll()
        {
           return context.Set<T>().ToList();   
        }

        public T GetById(int id)
        {
            return context.Set<T>().FirstOrDefault(x=>x.Id==id)==null?null : context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public int Update(T entity)
        {
            context.Set<T>().Update(entity);
            return context.SaveChanges();
        }
    }
}
