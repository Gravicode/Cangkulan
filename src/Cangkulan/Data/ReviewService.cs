using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class ReviewService : ICrud<Review>
    {
        CangkulanDB db;

        public ReviewService()
        {
            if (db == null) db = new CangkulanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Reviews.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Reviews.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<Review> FindByKeyword(string Keyword)
        {
            var data = from x in db.Reviews
                       where x.Message.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Review> GetAllData()
        {
            return db.Reviews.OrderBy(x => x.Id).ToList();
        }
        
        public List<Review> GetEmployerReview(long UserId)
        {
            var reviewed =  db.Reviews.Include(c=>c.Project).Include(c=>c.Employer).Include(c=>c.FreelancerUser).AsNoTracking().Where(x=> x.FreelancerUserId == UserId && x.ReviewType == ReviewTypes.Employer).ToList();
            var now = DateHelper.GetLocalTimeNow();
			var unreviewed =  db.Projects.Where(x=> x.WinnerId == UserId).ToList().Where(x=>!reviewed.Any(a=>a.ProjectId == x.Id)).Select(x=>new Review() { EmployerId = x.EmployerId, FreelancerUserId = x.WinnerId.Value, CreatedDate =now, IsReviewed = false, ProjectId = x.Id, ReviewType = ReviewTypes.Employer, Project = x }).ToList();
            return reviewed.Union(unreviewed).ToList();
        } 
        
        public List<Review> GetFreelancerReview(long UserId)
        {
            var reviewed =  db.Reviews.Include(c => c.Project).Include(c => c.Employer).Include(c => c.FreelancerUser).AsNoTracking().Where(x=> x.EmployerId == UserId && x.ReviewType == ReviewTypes.Freelancer).ToList();
            var now = DateHelper.GetLocalTimeNow();
			var unreviewed =  db.Projects.Where(x=> x.EmployerId == UserId && x.WinnerId>0).ToList().Where(x => !reviewed.Any(a=>a.ProjectId == x.Id)).Select(x=>new Review() { EmployerId = x.EmployerId, FreelancerUserId = x.WinnerId.Value, CreatedDate =now, IsReviewed = false, ProjectId = x.Id, ReviewType = ReviewTypes.Freelancer, Project = x }).ToList();
            return reviewed.Union(unreviewed).ToList();
        }

        public Review GetDataById(object Id)
        {
            return db.Reviews.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Review data)
        {
            try
            {
                db.Reviews.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(Review data)
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
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public long GetLastId()
        {
            return db.Reviews.Max(x => x.Id);
        }
    }

}
/*











 */
