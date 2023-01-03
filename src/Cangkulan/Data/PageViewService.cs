using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
	public class PageViewService : ICrud<PageView>
	{
		CangkulanDB db;

		public PageViewService()
		{
			if (db == null) db = new CangkulanDB();

		}
		public bool DeleteData(object Id)
		{
			var selData = (db.PageViews.Where(x => x.Id == (long)Id).FirstOrDefault());
			db.PageViews.Remove(selData);
			db.SaveChanges();
			return true;
		}


		public long GetMyPageView(UserProfile user, DateTime StartDate, DateTime EndDate)
		{
			var data = from x in db.PageViews
					   where x.PageName.Contains($"freelance-detail/{user.Id}") && x.HitDate >= StartDate && x.HitDate <= EndDate
					   select x;
			return data.Count();
		}
		public List<PageViewMonth> GetMyPageViewYear(UserProfile user)
		{
			try
			{
				var start = new DateTime(DateHelper.GetLocalTimeNow().Year, 1, 1);
				var end = new DateTime(DateHelper.GetLocalTimeNow().Year + 1, 1, 1).AddDays(-1);
				var data = from x in db.PageViews
						   where x.PageName.Contains($"freelance-detail/{user.Id}") && x.HitDate >= start && x.HitDate <= end
						   select x;
				var tmp = data.ToList().GroupBy(x => x.HitDate.Month.ToString())
								.Select(y => new PageViewMonth
								{
									Month = y.Key,
									Hit = y.Count()
								});
				return tmp.ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			return new List<PageViewMonth>();
		}
		public List<PageView> FindByKeyword(string Keyword)
		{
			var data = from x in db.PageViews
					   where x.PageName.Contains(Keyword)
					   select x;
			return data.ToList();
		}

		public List<PageView> GetAllData()
		{
			return db.PageViews.OrderBy(x => x.Id).ToList();
		}

		public PageView GetDataById(object Id)
		{
			return db.PageViews.Where(x => x.Id == (long)Id).FirstOrDefault();
		}


		public bool InsertData(PageView data)
		{
			try
			{
				db.PageViews.Add(data);
				db.SaveChanges();
				return true;
			}
			catch
			{

			}
			return false;

		}



		public bool UpdateData(PageView data)
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
			return db.PageViews.Max(x => x.Id);
		}
	}

}
/*











 */
