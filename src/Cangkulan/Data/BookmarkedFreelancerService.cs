using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class BookmarkedFreelancerService : ICrud<BookmarkedFreelancer>
    {
        CangkulanDB db;

        public BookmarkedFreelancerService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.BookmarkedFreelancers.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.BookmarkedFreelancers.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<BookmarkedFreelancer> FindByKeyword(string Keyword)
        {
            var data = from x in db.BookmarkedFreelancers.Include(c=>c.FreelancerUser)
                       where x.FreelancerUser.FullName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<BookmarkedFreelancer> GetAllData()
        {
            return db.BookmarkedFreelancers.OrderBy(x => x.Id).ToList();
        }

        public BookmarkedFreelancer GetDataById(object Id)
        {
            return db.BookmarkedFreelancers.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(BookmarkedFreelancer data)
        {
            try
            {
                db.BookmarkedFreelancers.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(BookmarkedFreelancer data)
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
            return db.BookmarkedFreelancers.Max(x => x.Id);
        }
    }

}
/*











 */
