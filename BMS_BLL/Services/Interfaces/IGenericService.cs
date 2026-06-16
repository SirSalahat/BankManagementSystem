using BMS_DALL.Classes;
using BMS_DALL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_BLL.Services.Interfaces
{
   public interface IGenericService<TRequest, TResponse,T>
    {
        public TResponse Add(T entity);
        public TResponse GetById(int id);

        public int Delete(int id);
        public int Update(T entity);
        public List<TResponse> GetAll();

    }
}
