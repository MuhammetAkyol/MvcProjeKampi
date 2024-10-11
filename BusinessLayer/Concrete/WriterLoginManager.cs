using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class WriterLoginManager : IWriterLoginService
    {
        IWriterDal _writerDal;

        public WriterLoginManager(IWriterDal ıwriterDal)
        {
            _writerDal = ıwriterDal;
        }

        public Writer GetWriter(string usarname, string password)
        {
            return _writerDal.Get(x => x.WriterMail == usarname && x.WriterPassword == password);
        }
    }
}
