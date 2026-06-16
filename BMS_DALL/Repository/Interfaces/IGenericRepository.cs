using Azure;
using BMS_DALL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DALL.Repository.Interfaces
{
  public  interface IGenericRepository< T> where T:BaseModel
    {
        public int Add(T entity);
        public T GetById(int id);
        public int Delete(T Entity);
        public int Update(T entity);
        public List<T> GetAll();

    }
}
