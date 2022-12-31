using Microsoft.EntityFrameworkCore;
using Cangkulan.Data;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
	public class BookmarkedProjectService : ICrud<BookmarkedProject>
	{
		CangkulanDB db;

		public BookmarkedProjectService()
		{
			if (db == null) db = new CangkulanDB();

		}
		public bool DeleteData(object Id)
		{
			var selData = (db.BookmarkedProjects.Where(x => x.Id == (long)Id).FirstOrDefault());
			db.BookmarkedProjects.Remove(selData);
			db.SaveChanges();
			return true;
		}

		public bool UnBookmark(long UserId, long ProjectId)
		{
			var item = db.BookmarkedProjects.Where(x => x.UserId == UserId && x.ProjectId == ProjectId).FirstOrDefault();
			if (item != null)
			{
				db.BookmarkedProjects.Remove(item);
			}
			else
			{
				return false;
			}
			var res = db.SaveChanges();
			if (res > 0) return true;
			return false;
		}
		public bool Bookmark(long UserId, long ProjectId)
		{
			var item = db.BookmarkedProjects.Where(x => x.UserId == UserId && x.ProjectId == ProjectId).FirstOrDefault();
			if (item != null)
			{
				return false;
			}
			item = new BookmarkedProject() { ProjectId = ProjectId, UserId = UserId };
			db.BookmarkedProjects.Add(item);
			var res = db.SaveChanges();
			if (res > 0) return true;
			return false;
		}
		public List<BookmarkedProject> FindByKeyword(string Keyword)
		{
			var data = from x in db.BookmarkedProjects.Include(c => c.Project)
					   where x.Project.ProjectName.Contains(Keyword)
					   select x;
			return data.ToList();
		}

		public List<BookmarkedProject> GetAllData()
		{
			return db.BookmarkedProjects.OrderBy(x => x.Id).ToList();
		}

		public BookmarkedProject GetDataById(object Id)
		{
			return db.BookmarkedProjects.Where(x => x.Id == (long)Id).FirstOrDefault();
		}


		public bool InsertData(BookmarkedProject data)
		{
			try
			{
				db.BookmarkedProjects.Add(data);
				db.SaveChanges();
				return true;
			}
			catch
			{

			}
			return false;

		}



		public bool UpdateData(BookmarkedProject data)
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
			return db.BookmarkedProjects.Max(x => x.Id);
		}
	}

}
/*











 */
