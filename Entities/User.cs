using Microsoft.AspNetCore.Identity;
using Shared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class User: IdentityUser,IEntity
    {       
    }
}
