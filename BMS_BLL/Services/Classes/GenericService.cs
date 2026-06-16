using BMS_BLL.Services.Interfaces;
using BMS_DALL.Classes;
using BMS_DALL.DBContextFolder;
using BMS_DALL.Migrations;
using BMS_DALL.Repository.Classes;
using BMS_DALL.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_BLL.Services.Classes
{
    public class GenericService<TRequest, TResponse, T> :IGenericService<TRequest, TResponse, T> where T : BaseModel
    {
      
        private readonly IGenericRepository< T> repo;

        public GenericService(IGenericRepository< T> _repo)
        {
            repo = _repo;
        }
        public TResponse Add(T entity)
        {
            repo.Add(entity);
            return entity.Adapt<TResponse>();
        }

        public int Delete(int id)
        {
            var userid = repo.GetById(id);
            userid.Adapt<T>();
            repo.Delete(userid);
            return 1;

        }

        public List<TResponse> GetAll()
        {
            
           return repo.GetAll().Adapt<List<TResponse>>();
        }

        public TResponse GetById(int id)
        {
            var entity = repo.GetById(id);
            return entity.Adapt<TResponse>();
        }
        public int Update(T entity)
        {
            var userid=repo.GetById(entity.Id);
            if (userid == null)
            {
                return 0;
            }
            else
            {
                repo.Update(entity.Adapt<T>());
                return 1;
            }
        }
    }
}
