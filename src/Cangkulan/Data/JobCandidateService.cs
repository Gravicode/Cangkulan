using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class JobCandidateService : ICrud<JobCandidate>
    {
        CangkulanDB db;

        public JobCandidateService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.JobCandidates.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.JobCandidates.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<JobCandidate> FindByKeyword(string Keyword)
        {
            var data = from x in db.JobCandidates.Include(c=>c.Job).Include(c=>c.Candidate)
                       where x.Candidate.FullName.Contains(Keyword) || x.Job.JobTitle.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<JobCandidate> GetAllData()
        {
            return db.JobCandidates.OrderBy(x => x.Id).ToList();
        }

        public JobCandidate GetDataById(object Id)
        {
            return db.JobCandidates.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(JobCandidate data)
        {
            try
            {
                db.JobCandidates.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(JobCandidate data)
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
            return db.JobCandidates.Max(x => x.Id);
        }
    }

}
/*











 */
