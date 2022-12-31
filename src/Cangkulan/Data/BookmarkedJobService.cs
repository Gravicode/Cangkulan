using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class BookmarkedJobService : ICrud<BookmarkedJob>
    {
        CangkulanDB db;

        public BookmarkedJobService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.BookmarkedJobs.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.BookmarkedJobs.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public bool UnBookmark(long UserId,long JobId)
        {
            var item = db.BookmarkedJobs.Where(x => x.UserId == UserId && x.JobId == JobId).FirstOrDefault();
            if (item != null)
            {
                db.BookmarkedJobs.Remove(item);
            }
            else
            {
                return false;
            }
            var res = db.SaveChanges();
            if (res > 0) return true;
            return false;
        } 
        public bool Bookmark(long UserId,long JobId)
        {
            var item = db.BookmarkedJobs.Where(x => x.UserId == UserId && x.JobId == JobId).FirstOrDefault();
            if (item != null)
            {
                return false;
            }
            item = new BookmarkedJob() { JobId = JobId, UserId = UserId };
            db.BookmarkedJobs.Add(item);
            var res = db.SaveChanges();
            if (res > 0) return true;
            return false;
        }
        public List<BookmarkedJob> FindByKeyword(string Keyword)
        {
            var data = from x in db.BookmarkedJobs.Include(c=>c.Job)
                       where x.Job.JobTitle.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<BookmarkedJob> GetAllData()
        {
            return db.BookmarkedJobs.OrderBy(x => x.Id).ToList();
        }

        public BookmarkedJob GetDataById(object Id)
        {
            return db.BookmarkedJobs.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(BookmarkedJob data)
        {
            try
            {
                db.BookmarkedJobs.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(BookmarkedJob data)
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
            return db.BookmarkedJobs.Max(x => x.Id);
        }
    }

}
/*











 */
