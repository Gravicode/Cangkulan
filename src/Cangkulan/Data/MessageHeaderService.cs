using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class MessageHeaderService : ICrud<MessageHeader>
    {
        CangkulanDB db;

        public MessageHeaderService()
        {
            if (db == null) db = new CangkulanDB();

        }

		public int GetUnreadMessageByUser(string username)
		{

			var data = db.MessageHeaders.Include(c => c.ToUser).Include(c=>c.FromUser).Include(c=>c.Chats).Where(x => x.Username == username && !x.IsRead).Count();
			return data;
		}

		public MessageHeader GetInboxByUid(string uid, string username)
		{
			var data = db.MessageHeaders.Include(c => c.ToUser).Include(c => c.FromUser).Include(c => c.Chats).Where(x => x.Username == username && x.Uid == uid).FirstOrDefault();
			return data;
		}

		public MessageHeader GetInboxByFromAndTo(string FromUsername, string ToUsername)
		{
			var data = db.MessageHeaders.Include(c => c.ToUser).Include(c => c.FromUser).Include(c => c.Chats).Where(x => x.Username == FromUsername && x.ToUsername == ToUsername).FirstOrDefault();
			return data;
		}

		public List<MessageHeader> FindByKeyword(string Keyword)
		{
			var data = db.MessageHeaders.Include(c => c.ToUser).Include(c => c.FromUser).Include(c => c.Chats).Where(x => x.Title.Contains(Keyword));
			return data.ToList();
		}
		public List<MessageHeader> GetLatestMessage(string username)
		{
			var data = db.MessageHeaders.Include(c => c.ToUser).Include(c => c.FromUser).Include(c => c.Chats).Where(x => x.Username == username).ToList();
			return data.OrderByDescending(x => x.CreatedDate).ToList();
		}
		public List<Inbox> LoadInbox(string username)
		{
			try
			{
				var messages = GetLatestMessage(username);
                var list_inbox = new List<Inbox>();
                
                foreach (var item in messages)
                {
                    var chats = item.Chats; 
                    list_inbox.Add(new Inbox() { Message = item, User = item.ToUser, Chats = chats == null ? new List<MessageDetail>() : chats.ToList() });
                }
                
                return list_inbox;
              
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			return default;

		}

		public bool DeleteData(object Id)
        {
            var selData = (db.MessageHeaders.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.MessageHeaders.Remove(selData);
            db.SaveChanges();
            return true;
        }
        public List<MessageHeader> GetLatestMessage(long userid)
        {
            var data = db.MessageHeaders.Where(x => x.UserId == userid && !x.IsRead).OrderByDescending(x => x.CreatedDate).ToList();
            return data;
        }
      

        public List<MessageHeader> GetAllData()
        {
            return db.MessageHeaders.OrderBy(x => x.Id).ToList();
        }

        public MessageHeader GetDataById(object Id)
        {
            return db.MessageHeaders.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(MessageHeader data)
        {
            try
            {
                db.MessageHeaders.Add(data);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;

        }



        public bool UpdateData(MessageHeader data)
        {
            try
            {
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                /*
                if (sel != null)
                {
                    sel.Nama = data.Nama;
                    sel.Keterangan = data.Keterangan;
                    sel.Tanggal = data.Tanggal;
                    sel.DocumentUrl = data.DocumentUrl;
                    sel.StreamUrl = data.StreamUrl;
                    return true;

                }*/
                return true;
            }
            catch
            {

            }
            return false;
        }

        public long GetLastId()
        {
            return db.MessageHeaders.Max(x => x.Id);
        }
    }

}
/*











 */
