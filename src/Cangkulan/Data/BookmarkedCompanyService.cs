using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class BookmarkedCompanyService : ICrud<BookmarkedCompany>
    {
        CangkulanDB db;

        public BookmarkedCompanyService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool UnBookmark(long UserId, long CompanyId)
        {
            var item = db.BookmarkedCompanys.Where(x => x.UserId == UserId && x.CompanyId == CompanyId).FirstOrDefault();
            if (item != null)
            {
                db.BookmarkedCompanys.Remove(item);
            }
            else
            {
                return false;
            }
            var res = db.SaveChanges();
            if (res > 0) return true;
            return false;
        }
        public bool Bookmark(long UserId, long CompanyId)
        {
            var item = db.BookmarkedCompanys.Where(x => x.UserId == UserId && x.CompanyId == CompanyId).FirstOrDefault();
            if (item != null)
            {
                return false;
            }
            item = new BookmarkedCompany() { CompanyId = CompanyId, UserId = UserId };
            db.BookmarkedCompanys.Add(item);
            var res = db.SaveChanges();
            if (res > 0) return true;
            return false;
        }
        public bool DeleteData(object Id)
        {
            var selData = (db.BookmarkedCompanys.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.BookmarkedCompanys.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<BookmarkedCompany> FindByKeyword(string Keyword)
        {
            var data = from x in db.BookmarkedCompanys.Include(c=>c.Company)
                       where x.Company.FullName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<BookmarkedCompany> GetAllData()
        {
            return db.BookmarkedCompanys.OrderBy(x => x.Id).ToList();
        }

        public BookmarkedCompany GetDataById(object Id)
        {
            return db.BookmarkedCompanys.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(BookmarkedCompany data)
        {
            try
            {
                db.BookmarkedCompanys.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(BookmarkedCompany data)
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
            return db.BookmarkedCompanys.Max(x => x.Id);
        }
    }

}
/*











 */
