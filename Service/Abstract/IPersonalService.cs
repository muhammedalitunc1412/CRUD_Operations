using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IPersonalService
    {
        Personal GetById(int id);

        List<Personal> GetAll();

        void Create(Personal entity);

        void Update(Personal entity);
        void Delete(Personal entity);
    }
}
