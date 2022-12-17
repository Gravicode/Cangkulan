using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
	public class JobCategoryService : ICrud<JobCategory>
	{
		CangkulanDB db;

		public JobCategoryService()
		{
			if (db == null) db = new CangkulanDB();

		}
		public bool DeleteData(object Id)
		{
			var selData = (db.JobCategorys.Where(x => x.Id == (long)Id).FirstOrDefault());
			db.JobCategorys.Remove(selData);
			db.SaveChanges();
			return true;
		}

		public List<JobCategory> FindByKeyword(string Keyword)
		{
			var data = from x in db.JobCategorys
					   where x.Category.Contains(Keyword)
					   select x;
			return data.ToList();
		}

		public List<JobCategory> GetAllData()
		{
			return db.JobCategorys.OrderBy(x => x.Category).ToList();
		}

		public JobCategory GetDataById(object Id)
		{
			return db.JobCategorys.Where(x => x.Id == (long)Id).FirstOrDefault();
		}


		public bool InsertData(JobCategory data)
		{
			try
			{
				db.JobCategorys.Add(data);
				db.SaveChanges();
				return true;
			}
			catch
			{

			}
			return false;

		}



		public bool UpdateData(JobCategory data)
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
			return db.JobCategorys.Max(x => x.Id);
		}
	}

}
/*











 */
