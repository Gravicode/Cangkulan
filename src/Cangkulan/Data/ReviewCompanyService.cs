using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class ReviewCompanyService : ICrud<ReviewCompany>
    {
        CangkulanDB db;

        public ReviewCompanyService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.ReviewCompanys.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.ReviewCompanys.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<ReviewCompany> FindByKeyword(string Keyword)
        {
            var data = from x in db.ReviewCompanys
                       where x.Message.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<ReviewCompany> GetAllData()
        {
            return db.ReviewCompanys.OrderBy(x => x.Id).ToList();
        }

        public ReviewCompany GetDataById(object Id)
        {
            return db.ReviewCompanys.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(ReviewCompany data)
        {
            try
            {
                db.ReviewCompanys.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(ReviewCompany data)
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
            return db.ReviewCompanys.Max(x => x.Id);
        }
    }

}
/*











 */
