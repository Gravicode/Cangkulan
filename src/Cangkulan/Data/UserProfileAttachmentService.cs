using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class UserProfileAttachmentService : ICrud<UserProfileAttachment>
    {
        CangkulanDB db;

        public UserProfileAttachmentService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.UserProfileAttachments.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.UserProfileAttachments.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<UserProfileAttachment> FindByKeyword(string Keyword)
        {
            var data = from x in db.UserProfileAttachments
                       where x.LinkUrl.Contains(Keyword) || x.Title.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<UserProfileAttachment> GetAllData()
        {
            return db.UserProfileAttachments.OrderBy(x => x.Id).ToList();
        }

        public UserProfileAttachment GetDataById(object Id)
        {
            return db.UserProfileAttachments.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(UserProfileAttachment data)
        {
            try
            {
                db.UserProfileAttachments.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(UserProfileAttachment data)
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
            return db.UserProfileAttachments.Max(x => x.Id);
        }
    }

}
/*











 */
