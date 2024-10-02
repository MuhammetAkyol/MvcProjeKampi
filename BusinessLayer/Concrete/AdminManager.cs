using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AdminManager : IAdminService
    {
        IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public AdminManager()
        {
        }

        public void UpdateAdminPasswords()
        {
            HashExistingPasswords(); // Mevcut şifreleri hash'le
        }

        private void HashExistingPasswords()
        {
            using (var context = new Context())
            {
                var admins = context.Admins.ToList(); // Tüm admin kayıtlarını al

                foreach (var admin in admins)
                {
                    // Şifreyi hash'le ve güncelle
                    admin.AdminPassword = PasswordHasher.HashPassword(admin.AdminPassword);
                }

                context.SaveChanges(); // Değişiklikleri kaydet
            }
        }
        public void AdminAdd(Admin admin)
        {
            throw new NotImplementedException();
        }

        public void AdminDelete(Admin admin)
        {
            throw new NotImplementedException();
        }

        public void AdminUpdate(Admin admin)
        {
            throw new NotImplementedException();
        }

        public Admin GetByID(int id)
        {
            return _adminDal.Get(x => x.AdminID == id);
        }

        public List<Admin> GetList()
        {
            return _adminDal.List();
        }

        public Admin Login(string username, string password)
        {
            return _adminDal.GetByUsernameAndPassword(username, password);
        }

        void IAdminService.HashExistingPasswords()
        {
            throw new NotImplementedException();
        }
    }
}
