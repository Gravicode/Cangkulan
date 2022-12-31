using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class ProjectBidderService : ICrud<ProjectBidder>
    {
        CangkulanDB db;

        public ProjectBidderService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.ProjectBidders.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.ProjectBidders.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<ProjectBidder> FindByKeyword(string Keyword)
        {
            var data = from x in db.ProjectBidders.Include(c=>c.Project).Include(c=>c.UserBidder)
                       where x.Project.ProjectName.Contains(Keyword) || x.UserBidder.FullName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<ProjectBidder> GetAllData()
        {
            return db.ProjectBidders.OrderBy(x => x.Id).ToList();
        }  
        
        public List<ProjectBidder> GetAllData(long UserId)
        {
            return db.ProjectBidders.Where(x=>x.UserBidderId == UserId).Include(c=>c.Project).Include(c=>c.UserBidder).OrderBy(x => x.Id).ToList();
        }

        public ProjectBidder GetDataById(object Id)
        {
            return db.ProjectBidders.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(ProjectBidder data)
        {
            try
            {
                db.ProjectBidders.Add(data);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;

        }



        public bool UpdateData(ProjectBidder data)
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
            return db.ProjectBidders.Max(x => x.Id);
        }
    }

}
/*











 */
