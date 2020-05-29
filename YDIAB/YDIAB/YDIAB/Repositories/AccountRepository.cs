using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YDIAB.Data;
using YDIAB.Models;

namespace YDIAB.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public void registerUserInDb(StoreUser user)
        {

            PasswordHasher<StoreUser> ph = new PasswordHasher<StoreUser>();
            user.PasswordHash = ph.HashPassword(user, user.Password);

            _context.Users.Add(user);
            _context.SaveChanges();
        }


        //public void removeUserInDb(StoreUser user, string username)
        //{

        //    var removableUser = _context.Users.SingleOrDefault(u => u.UserName == user.UserName);
        //    if (removableUser.UserName == username)
        //    {
        //        var list = _context.Users.Remove(removableUser).Entity;
        //        _context.SaveChanges();
        //    }
        //    else
        //    {
        //        throw new Exception("Incorrect username");
        //    }
        //}

    }
}
