﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cangkulan.Models;
using GemBox.Spreadsheet;
using System.Drawing;
using Cangkulan.Tools;
using Cangkulan.Helpers;

namespace Cangkulan.Data
{
    public class UserProfileService : ICrud<UserProfile>
    {
        CangkulanDB db;
        public UserProfileService()
        {
            if (db == null) db = new CangkulanDB();
            //db.Database.EnsureCreated();
        }
        public bool DeleteData(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.UserProfiles
                           where x.Id == FID
                           select x;
                foreach (var item in data)
                {
                    db.UserProfiles.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Login(string username, string password)
        {
            bool isAuthenticate = false;
            var usr = db.UserProfiles.Where(x => x.Username == username).FirstOrDefault();
            if (usr != null)
            {
                var enc = new Encryption();
                var pass = enc.Decrypt(usr.Password);
                isAuthenticate = pass == password;
            }
            return isAuthenticate;
        }
        public UserProfile GetItemByUsername2(string UName)
        {
            if (string.IsNullOrEmpty(UName)) return null;
            var selItem = db.UserProfiles.Include(c=>c.UserMessage).Include(c => c.ProjectList).Include(c => c.ProjectBids).Include(c => c.VacancyList).Where(x => x.Username.ToLower() == UName.ToLower()).FirstOrDefault();
            return selItem;
        }
        public UserProfile GetItemByUsername(string UName)
        {
            if (string.IsNullOrEmpty(UName)) return null;
            var selItem = db.UserProfiles.Include(c=>c.BookmarkedCompanys).Include(c=>c.ProjectBids).Include(c => c.BookmarkedFreelancers).Include(c => c.BookmarkedProjects).Include(c => c.BookmarkedJobs).Include(c=>c.Attachments).Where(x => x.Username.ToLower() == UName.ToLower()).FirstOrDefault();
            return selItem;
        }
        public UserProfile GetItemByEmail(string Email)
        {
            if (string.IsNullOrEmpty(Email)) return null;
            var selItem = db.UserProfiles.Include(c => c.BookmarkedFreelancers).ThenInclude(x=>x.FreelancerUser).Include(c => c.BookmarkedJobs).ThenInclude(x => x.Job).ThenInclude(x=>x.Company).Include(c => c.Attachments).Where(x => x.Email.ToLower() == Email.ToLower()).FirstOrDefault();
            return selItem;
        }
        public Roles GetUserRole(string Email)
        {
            var selItem = db.UserProfiles.Where(x => x.Username == Email).FirstOrDefault();
            return selItem.Role;
        } 
        
        public UserProfile GetUserByEmail(string Email)
        {
            var selItem = db.UserProfiles.Where(x => x.Username == Email).FirstOrDefault();
            return selItem;
        }
        public UserProfile GetItemByPhone(string Phone)
        {
            var selItem = db.UserProfiles.Where(x => x.Phone.ToLower() == Phone.ToLower()).FirstOrDefault();
            return selItem;
        }
        public List<UserProfile> FindByKeyword(string Keyword)
        {
            var data = from x in db.UserProfiles
                       where x.Email.Contains(Keyword) || x.FullName.Contains(Keyword)
                       select x;
            return data.ToList();
        }
        
        

        public List<UserProfile> GetAllData()
        {
            var data = from x in db.UserProfiles
                       select x;
            return data.ToList();
        }

        public UserProfile GetDataById(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.UserProfiles.Include(c=>c.ReviewByEmployers).Include(c => c.BookmarkedCompanys).Include(c => c.ProjectBids).ThenInclude(c=>c.Project).Include(c => c.BookmarkedFreelancers).Include(c => c.BookmarkedProjects).Include(c => c.BookmarkedJobs).Include(c => c.Attachments)
                           where x.Id == FID
                           select x;
                return data.FirstOrDefault();
            }
            return default;
        }

        public long GetLastId()
        {
            var lastId = db.UserProfiles.OrderByDescending(x => x.Id).FirstOrDefault();
            return lastId.Id + 1;
        } 
        
        public long GetFreelancerCount()
        {
            var count = db.UserProfiles.Where(x=>x.AccountType == AccountTypes.Freelancer).Count();
            return count;
        }  
        public long GetUsersCount()
        {
            var count = db.UserProfiles.Count();
            return count;
        }
        public bool IsUserExists(string Email)
        {
            if (string.IsNullOrEmpty(Email)) return true;
            //if (db.UserProfiles.Count() <= 0 ) return false;
            var exists = db.UserProfiles.Any(x => x.Username.ToLower() == Email.ToLower());
            return exists;
        }
        public bool InsertData(UserProfile data)
        {
            try
            {
                db.UserProfiles.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }

        }

        public bool UpdateData(UserProfile data)
        {
            try
            {
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;


            }

        }
        #region Excel
        public static List<UserProfile> ReadExcel(string PathFile)
        {
            try
            {
                Encryption enc = new Encryption();
                var DefaultPass = enc.Encrypt("354jaya");
                var DataUserProfile = new List<UserProfile>();
                //SpreadsheetInfo.SetLicense(AppConstants.GemLic);

                if (!File.Exists(PathFile)) return null;
                var workbook = ExcelFile.Load(PathFile);


                // Iterate through all worksheets in an Excel workbook.
                var worksheet = workbook.Worksheets[0];
                var counter = 0;
                // Iterate through all rows in an Excel worksheet.
                foreach (var row in worksheet.Rows)
                {
                    if (counter > 0)
                    {
                        var newUserProfile = new UserProfile();
                        //newUserProfile.KK = row.Cells[0].Value?.ToString();
                        //newUserProfile.NoUrut = int.Parse(row.Cells[1].Value?.ToString());

                        newUserProfile.FullName = row.Cells[1].Value?.ToString().Trim();
                        newUserProfile.Alamat = row.Cells[2].Value?.ToString().Trim();
                        newUserProfile.Phone = row.Cells[3].Value?.ToString().Trim();
                        newUserProfile.Email = row.Cells[4].Value?.ToString().Trim();
                        newUserProfile.Aktif = row.Cells[5].Value?.ToString().Trim() == "Ya" ? true:false;
                        newUserProfile.KTP = row.Cells[6].Value?.ToString().Trim();
                        newUserProfile.Daerah = row.Cells[7].Value?.ToString().Trim();
                        newUserProfile.Desa = row.Cells[8].Value?.ToString().Trim();
                        newUserProfile.Kelompok = row.Cells[9].Value?.ToString().Trim();
                        newUserProfile.Username = string.IsNullOrEmpty(row.Cells[10].Value?.ToString().Trim()) ? MailHelper.GenerateEmailFromName(newUserProfile.FullName.Trim(), "Cangkulan.online") :  row.Cells[10].Value?.ToString().Trim();
                        if (string.IsNullOrEmpty(newUserProfile.Email))
                        {
                            newUserProfile.Email = newUserProfile.Username;
                        }
                        newUserProfile.Role = row.Cells[11].Value?.ToString().Trim() == "Ya" ? Roles.Pengurus  : Roles.User;
                        newUserProfile.Password = DefaultPass;
                        DataUserProfile.Add(newUserProfile);
                    }
                    counter++;


                }

                return DataUserProfile;
            }
            catch
            {
                return null;
            }

        }

        public OutputCls ImportData(List<UserProfile> NewData)
        {
            var output = new OutputCls();
            try
            {
                Encryption enc = new Encryption();
                var currentData = db.UserProfiles.ToList();
                foreach (var item in NewData)
                {
                    //UserProfile? existing = null;
                    //foreach(var x in currentData)
                    //{
                    //    if(x.Username == item.Username)
                    //    {
                    //        existing = x; break;
                    //    }
                    //}
                    var existing = currentData.Where(x => x.Username.Equals(item.Username,StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    if (existing == null)
                    {
                        item.Password = enc.Encrypt(AppConstants.DefaultPass);
                        db.UserProfiles.Add(item);
                    }
                    else
                    {
                        existing.FullName = item.FullName;
                        existing.Alamat = item.Alamat;
                        existing.Phone = item.Phone;
                        existing.Email = item.Email;
                        existing.Aktif = item.Aktif;
                        existing.KTP = item.KTP;
                        existing.Daerah = item.Daerah;
                        existing.Desa = item.Desa;
                        existing.Kelompok = item.Kelompok;
                        //existing.Username =   item.Username  ;
                        existing.Role = item.Role;
                    }
                }
                db.SaveChanges();
                output.IsSucceed = true;
               
            }
            catch (Exception ex)
            {
                output.IsSucceed = false;
                output.Message = ex.ToString();
                Console.WriteLine(ex);
             
            }
            return output;
        }
        public List<UserProfile> FindByKeyword(string Keyword, string Category, string Location, double RateHour, string[] Skills, string SortBy)
        {
            var data = db.UserProfiles.Include(x=>x.JobCategory).Where(x=>x.AccountType== AccountTypes.Freelancer).AsQueryable();
            if (!string.IsNullOrEmpty(Keyword))
            {
                data = data.Where(x => x.FullName.Contains(Keyword) || x.TagLine.Contains(Keyword) || x.AboutMe.Contains(Keyword));
            }
            if (!string.IsNullOrEmpty(Category) && Category != "All")
            {
                data = data.Where(x => x.JobCategory.Category == Category);
            }
            if (!string.IsNullOrEmpty(Location))
            {
                data = data.Where(x => x.Alamat.Contains(Location));
            }
            if (RateHour > 0)
            {
                data = data.Where(x =>  x.RatePerHour <= RateHour);
            }
            switch (SortBy)
            {
                case "Newest":
                    data = data.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Oldest":
                    data = data.OrderBy(x => x.CreatedDate);
                    break;
                default:
                case "Relevance":
                    break;
            }
           
            if (Skills.Length > 0)
            {
                data = data.ToList().Where(x => x.Skill.ContainsAny(Skills)).AsQueryable();
            }

            return data.ToList();


        }
        public byte[] ExportToExcel()
        {
            // If using Professional version, put your serial key below.
            //SpreadsheetInfo.SetLicense(AppConstants.GemLic);

            var workbook = new ExcelFile();
            var worksheet = workbook.Worksheets.Add("Anggota");
            var datas = GetAllData();
            int row = 1;

            var styleHeader = new CellStyle();
            styleHeader.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            styleHeader.VerticalAlignment = VerticalAlignmentStyle.Center;
            styleHeader.Font.Weight = ExcelFont.BoldWeight;
            styleHeader.Font.Color = Color.Black;
            styleHeader.WrapText = true;
            styleHeader.Borders.SetBorders(MultipleBorders.Left | MultipleBorders.Right | MultipleBorders.Top | MultipleBorders.Bottom, Color.Black, LineStyle.Thin);

         

            worksheet.Cells[0, 0].Value = "No";
            worksheet.Cells[0, 1].Value = "Nama";
            worksheet.Cells[0, 2].Value = "Alamat";
            worksheet.Cells[0, 3].Value = "Telepon";
            worksheet.Cells[0, 4].Value = "Email";
            worksheet.Cells[0, 5].Value = "Aktif";
            worksheet.Cells[0, 6].Value = "KTP";
            worksheet.Cells[0, 7].Value = "Daerah";
            worksheet.Cells[0, 8].Value = "Desa";
            worksheet.Cells[0, 9].Value = "Kelompok";
            worksheet.Cells[0, 10].Value = "Username";
            worksheet.Cells[0, 11].Value = "Pengurus";
          

            worksheet.Cells[0, 0].Style = styleHeader;
            worksheet.Cells[0, 1].Style = styleHeader;
            worksheet.Cells[0, 2].Style = styleHeader;
            worksheet.Cells[0, 3].Style = styleHeader;
            worksheet.Cells[0, 4].Style = styleHeader;
            worksheet.Cells[0, 5].Style = styleHeader;
            worksheet.Cells[0, 6].Style = styleHeader;
            worksheet.Cells[0, 7].Style = styleHeader;
            worksheet.Cells[0, 8].Style = styleHeader;
            worksheet.Cells[0, 9].Style = styleHeader;
            worksheet.Cells[0, 10].Style = styleHeader;
            worksheet.Cells[0, 11].Style = styleHeader;
         

            var style = new CellStyle();
            style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            style.VerticalAlignment = VerticalAlignmentStyle.Center;
            style.Font.Weight = ExcelFont.NormalWeight;
            style.Font.Color = Color.Black;
            style.WrapText = true;
            style.Borders.SetBorders(MultipleBorders.Left | MultipleBorders.Right | MultipleBorders.Top | MultipleBorders.Bottom, Color.Black, LineStyle.Thin);

            foreach (var item in datas)
            {

             

                worksheet.Cells[row, 0].Value = row;
                worksheet.Cells[row, 1].Value = item.FullName;
                worksheet.Cells[row, 2].Value = item.Alamat;
                worksheet.Cells[row, 3].Value = item.Phone;
                worksheet.Cells[row, 4].Value = item.Email;
                worksheet.Cells[row, 5].Value = item.Aktif? "Ya":"Tidak";
                worksheet.Cells[row, 6].Value = item.KTP;
                worksheet.Cells[row, 7].Value = item.Daerah;
                worksheet.Cells[row, 8].Value = item.Desa;
                worksheet.Cells[row, 9].Value = item.Kelompok;
                worksheet.Cells[row, 10].Value = item.Username?.ToString();
                worksheet.Cells[row, 11].Value = item.Role == Roles.Pengurus?"Ya":"Tidak";
           

                worksheet.Cells[row, 0].Style = style;
                worksheet.Cells[row, 1].Style = style;
                worksheet.Cells[row, 2].Style = style;
                worksheet.Cells[row, 3].Style = style;
                worksheet.Cells[row, 4].Style = style;
                worksheet.Cells[row, 5].Style = style;
                worksheet.Cells[row, 6].Style = style;
                worksheet.Cells[row, 7].Style = style;
                worksheet.Cells[row, 8].Style = style;
                worksheet.Cells[row, 9].Style = style;
                worksheet.Cells[row, 10].Style = style;
                worksheet.Cells[row, 11].Style = style;
           
              
                row++;
            }
            var tmpfile = Path.GetTempFileName() + ".xlsx";

            workbook.Save(tmpfile);
            return File.ReadAllBytes(tmpfile);
        }
        #endregion
    }
}

