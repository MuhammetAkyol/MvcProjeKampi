using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Repositories
{
    public class IGenericRepository<T> : IRepository<T> where T : class
    {
        Context c = new Context();
        DbSet<T> _object;
        public IGenericRepository()
        {
            _object=c.Set<T>();
        }
        public void Delete(T p)
        {
            _object.Remove(p);
            c.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)//geriye tek değer döndürme - back is one value rotate
        {
            return _object.SingleOrDefault(filter);
        }

        public void Insert(T p)
        {
            _object.Add(p);
            c.SaveChanges();
        }

        public List<T> List(Expression<Func<T, bool>> filter)//geriye çok değer döndürme - back is multiple value rotate
        {
           return _object.Where(filter).ToList();
        }          

        public List<T> List()
        {
                return _object.ToList();
            
        }

        public void Update(T p)
        {
            _object.AddOrUpdate(p);
            c.SaveChanges();
        }
    }
}

    