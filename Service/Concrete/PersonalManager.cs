using DataAccess.Abstract;
using Entities;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class PersonalManager : IPersonalService
    {
        private IPersonalRepository _personalRepository;
        public PersonalManager(IPersonalRepository personalRepository)
        {
            _personalRepository = personalRepository;
        }
        public void Create(Personal entity)
        {
            _personalRepository.Create(entity);
        }

        public void Delete(Personal entity)
        {
            _personalRepository.Delete(entity);
        }

        public List<Personal> GetAll()
        {
            return _personalRepository.GetAll();
        }

        public Personal GetById(int id)
        {
            return _personalRepository.GetById(id);
        }

        public void Update(Personal entity)
        {
            _personalRepository.Update(entity);
        }
    }
}
