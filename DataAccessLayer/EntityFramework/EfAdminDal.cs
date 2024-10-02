using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfAdminDal : IGenericRepository<Admin>, IAdminDal
    {
        public Admin GetByUsernameAndPassword(string username, string password)
        {
            using (var context = new Context())
            {
                return context.Admins.FirstOrDefault(x => x.AdminUserName == username && x.AdminPassword == password);
            }
        }
    }
}
