using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YDIAB.Models;

namespace YDIAB.Repositories
{
    public interface IAccountRepository
    {
        public void registerUserInDb(StoreUser user);

        //public void removeUserInDb(StoreUser user, string username);
    }
}
