using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class EmploymentHistoryService : ICrud<EmploymentHistory>
    {
        CangkulanDB db;

        public EmploymentHistoryService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.EmploymentHistorys.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.EmploymentHistorys.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<EmploymentHistory> FindByKeyword(string Keyword)
        {
            var data = from x in db.EmploymentHistorys
                       where x.CompanyName.Contains(Keyword) || x.Description.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<EmploymentHistory> GetAllData()
        {
            return db.EmploymentHistorys.OrderBy(x => x.Id).ToList();
        }

        public EmploymentHistory GetDataById(object Id)
        {
            return db.EmploymentHistorys.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(EmploymentHistory data)
        {
            try
            {
                db.EmploymentHistorys.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(EmploymentHistory data)
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
            return db.EmploymentHistorys.Max(x => x.Id);
        }
    }

}
/*











 */
