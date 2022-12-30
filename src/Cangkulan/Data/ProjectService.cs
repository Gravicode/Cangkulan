using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class ProjectService : ICrud<Project>
    {
        CangkulanDB db;

        public ProjectService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Projects.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Projects.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<Project> FindByKeyword(string Keyword)
        {
            var data = from x in db.Projects
                       where x.ProjectName.Contains(Keyword)
                       select x;
            return data.ToList();
        }
		public List<Project> GetAllData(UserProfile user)
		{
			return db.Projects.Include(c => c.Employer).Where(x => x.EmployerId == user.Id).OrderBy(x => x.Id).ToList();
		}
		public List<Project> GetAllData()
        {
            return db.Projects.OrderBy(x => x.Id).ToList();
        }
        public List<Project> GetLatestProject(int Limit = 10)
        {
            return db.Projects.Where(x => x.Active).OrderByDescending(x => x.CreatedDate).Take(Limit).ToList();
        }
		public long GetProjectCount()
		{
			return db.Projects.Count();
		}
		public long GetJobCount()
		{
			return db.Projects.Count();
		}

		public Project GetDataById(object Id)
        {
            return db.Projects.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Project data)
        {
            try
            {
                db.Projects.Add(data);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex);
            }
            return false;

        }



        public bool UpdateData(Project data)
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
            return db.Projects.Max(x => x.Id);
        }
    }

}
/*











 */
