using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public class IIstatistikDal : IRepository<Istatistik>
    {
        public void Delete(Istatistik p)
        {
            throw new NotImplementedException();
        }

        public Istatistik Get(Expression<Func<Istatistik, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Insert(Istatistik p)
        {
            throw new NotImplementedException();
        }

        public List<Istatistik> List()
        {
            throw new NotImplementedException();
        }

        public List<Istatistik> List(Expression<Func<Istatistik, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(Istatistik p)
        {
            throw new NotImplementedException();
        }
    }
}
