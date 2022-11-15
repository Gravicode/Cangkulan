using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class ReviewService : ICrud<Review>
    {
        CangkulanDB db;

        public ReviewService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Reviews.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Reviews.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<Review> FindByKeyword(string Keyword)
        {
            var data = from x in db.Reviews
                       where x.Message.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Review> GetAllData()
        {
            return db.Reviews.OrderBy(x => x.Id).ToList();
        }

        public Review GetDataById(object Id)
        {
            return db.Reviews.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Review data)
        {
            try
            {
                db.Reviews.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(Review data)
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
            return db.Reviews.Max(x => x.Id);
        }
    }

}
/*











 */
