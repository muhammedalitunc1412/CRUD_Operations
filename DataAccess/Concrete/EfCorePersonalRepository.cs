using DataAccess.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfCorePersonalRepository : EfCoreGenericRepository<Personal,CRUD_Context>, IPersonalRepository
    {
       
    }
}
