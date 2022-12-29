using Microsoft.EntityFrameworkCore;
using Cangkulan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Cangkulan.Data
{
    public class CangkulanDB : DbContext
    {

        public CangkulanDB()
        {
        }

        public CangkulanDB(DbContextOptions<CangkulanDB> options)
            : base(options)
        {
        }

         
        public DbSet<ProjectCategory> ProjectCategorys { get; set; }     
        public DbSet<CompanyCategory> CompanyCategorys { get; set; }     
        public DbSet<JobCategory> JobCategorys { get; set; }     
        public DbSet<UserProfile> UserProfiles { get; set; }     
        public DbSet<MessageHeader> MessageHeaders { get; set; }      
        public DbSet<MessageDetail> MessageDetails { get; set; }      
        public DbSet<Notification> Notifications { get; set; }      
        public DbSet<UserProfileAttachment> UserProfileAttachments { get; set; }      
        public DbSet<EmploymentHistory> EmploymentHistorys { get; set; }      
        public DbSet<PageView> PageViews { get; set; }      
        public DbSet<Note> Notes { get; set; }      
        public DbSet<Order> Orders { get; set; }      
        public DbSet<BookmarkedCompany> BookmarkedCompanys { get; set; }      
        public DbSet<BookmarkedJob> BookmarkedJobs { get; set; }      
        public DbSet<BookmarkedFreelancer> BookmarkedFreelancers { get; set; }      
        public DbSet<Review> Reviews { get; set; }      
        public DbSet<ReviewCompany> ReviewCompanys { get; set; }      
        public DbSet<Testimonial> Testimonials { get; set; }      
        public DbSet<Company> Companys { get; set; }      
        public DbSet<Job> Jobs { get; set; }      
        public DbSet<JobCandidate> JobCandidates { get; set; }      
        public DbSet<Project> Projects { get; set; }      
        public DbSet<ProjectBidder> ProjectBidders { get; set; }      
        public DbSet<Blog> Blogs { get; set; }      
        public DbSet<BlogComment> BlogComments { get; set; }      
        public DbSet<Contact> Contacts { get; set; }      
        public DbSet<Log> Logs { get; set; }      
       
       
       
      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*
            builder.Entity<DataEventRecord>().HasKey(m => m.DataEventRecordId);
            builder.Entity<SourceInfo>().HasKey(m => m.SourceInfoId);

            // shadow properties
            builder.Entity<DataEventRecord>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<SourceInfo>().Property<DateTime>("UpdatedTimestamp");
            */
          
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            /*
            ChangeTracker.DetectChanges();

            updateUpdatedProperty<SourceInfo>();
            updateUpdatedProperty<DataEventRecord>();
            */
            return base.SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(AppConstants.SQLConn,ServerVersion.AutoDetect(AppConstants.SQLConn));
            }
        }
        private void updateUpdatedProperty<T>() where T : class
        {
            /*
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
            */
        }

    }
}
