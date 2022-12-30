using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class TestimonialService : ICrud<Testimonial>
    {
        CangkulanDB db;

        public TestimonialService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Testimonials.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Testimonials.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<Testimonial> FindByKeyword(string Keyword)
        {
            var data = from x in db.Testimonials
                       where x.Message.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Testimonial> GetAllData()
        {
            return db.Testimonials.OrderBy(x => x.Id).ToList();
        }
        
        public List<Testimonial> GetLatest(int Limit=5)
        {
            return db.Testimonials.Include(c=>c.User).OrderByDescending(x => x.CreatedDate).Take(Limit).ToList();
        }

        public Testimonial GetDataById(object Id)
        {
            return db.Testimonials.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Testimonial data)
        {
            try
            {
                db.Testimonials.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(Testimonial data)
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
            return db.Testimonials.Max(x => x.Id);
        }
    }

}
/*











 */
