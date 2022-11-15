using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class OrderService : ICrud<Order>
    {
        CangkulanDB db;

        public OrderService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Orders.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Orders.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<Order> FindByKeyword(string Keyword)
        {
            var data = from x in db.Orders
                       where x.PackageName.Contains(Keyword) || x.OrderNo.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Order> GetAllData()
        {
            return db.Orders.OrderBy(x => x.Id).ToList();
        }

        public Order GetDataById(object Id)
        {
            return db.Orders.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Order data)
        {
            try
            {
                db.Orders.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(Order data)
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
            return db.Orders.Max(x => x.Id);
        }
    }

}
/*











 */
