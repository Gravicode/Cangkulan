using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class JobService : ICrud<Job>
    {
        CangkulanDB db;

        public JobService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Jobs.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Jobs.Remove(selData);
            db.SaveChanges();
            return true;
        }

		public List<Job> GetSimilar(Job item,int Limit = 2)
		{
            if (item == null) return new List<Job>();
			var data = from x in db.Jobs
					   where x.JobCategory == item.JobCategory && x.JobType == item.JobType && x.Active && x.Id!=item.Id
                       orderby x.CreatedDate descending
					   select x;
			return data.Take(Limit).ToList();
		}

		public List<Job> FindByKeyword(string Keyword)
        {

            var data = from x in db.Jobs
                       where x.JobTitle.Contains(Keyword) || x.JobDesc.Contains(Keyword)
                       select x;
            return data.ToList();
        }
		public long GetJobCount()
		{
			return db.Jobs.Count();
		}
		public List<Job> GetAllData()
        {
            return db.Jobs.OrderBy(x => x.Id).ToList();
        }
        
        public List<Job> GetLatestJob(int Limit=10)
        {
			return db.Jobs.Include(c=>c.Company).Where(x => x.Active).OrderByDescending(x => x.CreatedDate).Take(Limit).ToList();
		}
        public List<Job> GetAllData(UserProfile user)
        {
            return db.Jobs.Include(c=>c.Employer).Where(x=>x.EmployerId == user.Id).OrderBy(x => x.Id).ToList();
        }
        public List<JobCategoryCls> GetCategoriesCount()
        {
            
			var tmp = db.Jobs.GroupBy(x => x.JobCategory)
					  .Select(y => new JobCategoryCls {
						  Category = y.Key,
						  Count = y.Count(),
                          PicUrl=""
					  });
            return tmp.ToList();
		}
        public Job GetDataById(object Id)
        {
            return db.Jobs.Include(x=>x.Company).Include(x=>x.Employer).Include(x=>x.JobCandidates).Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Job data)
        {
            try
            {
                db.Jobs.Add(data);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;

        }



        public bool UpdateData(Job data)
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
            return db.Jobs.Max(x => x.Id);
        }
    }

}
/*











 */
