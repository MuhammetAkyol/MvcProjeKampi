﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public Message GetByID(int id)
        {
            return _messageDal.Get(x => x.MessageID == id);
        }

        public List<Message> GetListInbox(string p)
        {
            return _messageDal.List(x => x.ReceiverMail == p && x.messageremove == false);
        }

        public List<Message> GetListSendInbox(string p)
        {
            return _messageDal.List(x => x.SenderMail == p && x.messagestatus == true && x.messageremove == false);
        }

        public List<Message> GetListDraft()
        {
            return _messageDal.List(x => x.SenderMail == "admin@gmail.com" && x.messagestatus == false && x.messageremove == false);
        }
        public List<Message> GetListDelete()
        {
            return _messageDal.List(x=>x.messageremove == true);
        }
        public void MessageAdd(Message message)
        {
            _messageDal.Insert(message);
        }

        public void MessageDelete(Message message)
        {            
            _messageDal.Update(message); // Güncelle            
        }

        public void MessageUpdate(Message message)
        {
            _messageDal.Update(message);
            

        }        
    }
}
