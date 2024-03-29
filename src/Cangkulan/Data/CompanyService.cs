﻿using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class CompanyService : ICrud<Company>
    {
        CangkulanDB db;

        public CompanyService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public List<Company> GetSimilar(Company item, int Limit = 2)
        {
            if (item == null) return new List<Company>();
            var data = from x in db.Companys
                       where x.Category == item.Category && x.Id != item.Id
                       orderby x.CreatedDate descending
                       select x;
            return data.Take(Limit).ToList();
        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Companys.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Companys.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<Company> FindByKeyword(string Keyword)
        {
            var data = from x in db.Companys
                       where x.FullName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Company> GetAllData()
        {
            return db.Companys.OrderBy(x => x.FullName).ToList();
        }
        public List<Company> GetAllData(string Alphabet)
        {
            return db.Companys.Where(x=>x.FullName.StartsWith(Alphabet)).OrderBy(x => x.FullName).ToList();
        }
        public Company GetDataById(object Id)
        {
            return db.Companys.Include(c=>c.User).Include(c=>c.Reviews).Include(c=>c.Jobs).Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Company data)
        {
            try
            {
                db.Companys.Add(data);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;

        }



        public bool UpdateData(Company data)
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
            return db.Companys.Max(x => x.Id);
        }
    }

}
/*











 */
