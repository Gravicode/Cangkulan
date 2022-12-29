using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class CompanyCategoryService : ICrud<CompanyCategory>
    {
        CangkulanDB db;

        public CompanyCategoryService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.CompanyCategorys.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.CompanyCategorys.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<CompanyCategory> FindByKeyword(string Keyword)
        {
            var data = from x in db.CompanyCategorys
                       where x.Category.Contains(Keyword) || x.SubCategory.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<CompanyCategory> GetAllData()
        {
            return db.CompanyCategorys.OrderBy(x => x.Category).ToList();
        }

        public CompanyCategory GetDataById(object Id)
        {
            return db.CompanyCategorys.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(CompanyCategory data)
        {
            try
            {
                db.CompanyCategorys.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(CompanyCategory data)
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
            return db.CompanyCategorys.Max(x => x.Id);
        }
    }

}
/*











 */
