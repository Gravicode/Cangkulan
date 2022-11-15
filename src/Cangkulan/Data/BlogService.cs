using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class BlogService : ICrud<Blog>
    {
        CangkulanDB db;

        public BlogService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Blogs.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Blogs.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<Blog> FindByKeyword(string Keyword)
        {
            var data = from x in db.Blogs
                       where x.Title.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Blog> GetAllData()
        {
            return db.Blogs.OrderBy(x => x.Id).ToList();
        }

        public Blog GetDataById(object Id)
        {
            return db.Blogs.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Blog data)
        {
            try
            {
                db.Blogs.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(Blog data)
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
            return db.Blogs.Max(x => x.Id);
        }
    }

}
/*











 */
