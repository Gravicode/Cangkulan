using GemBox.Document;
using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Reflection;

namespace Cangkulan.Models
{
    #region helpers model
    public class StorageObject
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string FileUrl { get; set; }
        public string ContentType { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? LastAccess { get; set; }
    }
    public class StorageSetting
    {
        public string EndpointUrl { get; set; } = "https://is3.cloudhost.id";
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Region { get; set; } = "USWest1";
        public string Bucket { get; set; }
        public string BaseUrl { get; set; }
        public bool Ssl { get; set; } = true;
        public StorageSetting()
        {

        }
        public StorageSetting(string Endpoint, string Accesskey, string Secretkey, string Region, string Bucket)
        {
            this.EndpointUrl = Endpoint;
            this.AccessKey = Accesskey;
            this.SecretKey = Secretkey;
            this.Region = Region;
            this.Bucket = Bucket;
            GenerateBaseUrl();
        }
        public void GenerateBaseUrl()
        {
            this.BaseUrl = EndpointUrl + "/{bucket}/{key}";
        }
    }
    public class OutputCls
    {
        public OutputCls()
        {
            this.IsSucceed = false;
            this.Message = "";
        }
        public object Data { get; set; }
        public string Message { get; set; }
        public bool IsSucceed { get; set; }
    }
    #endregion

    [Table("message_header")]
    public class MessageHeader
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public string Uid { set; get; }
        [ForeignKey(nameof(FromUser)), Column(Order = 0)]
        public long FromUserId { set; get; }
        public UserProfile FromUser { set; get; }
        [ForeignKey(nameof(User)), Column(Order = 1)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string Title { set; get; }
        public DateTime CreatedDate { set; get; }
        public MessageTypes MessageType { set; get; }
        public string MemberIds { set; get; }
        public int MemberCount { set; get; }
        public DateTime LastMessage { set; get; }
        public DateTime LastUpdate { set; get; }
    }

    [Table("message_detail")]
    public class MessageDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Uid { set; get; }
        [ForeignKey("MessageHeader")]
        public long MessageHeaderId { set; get; }
        public DateTime CreatedDate { set; get; }
        [ForeignKey(nameof(FromUser)), Column(Order = 0)]
        public long FromUserId { set; get; }
        public UserProfile FromUser { set; get; }
        public string Message { set; get; }
        public string AttachmentUrls { set; get; }
        public string ImageUrls { set; get; }
        public string LinkUrls { set; get; }
    }

    [Table("notification")]
    public class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public DateTime CreatedDate { set; get; }
        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string Action { set; get; }
        public string LinkUrl { set; get; }
        public string LinkDesc { set; get; }
        public string Message { set; get; }
        public bool IsRead { set; get; } = false;
    }

    public enum MessageTypes { Personal, Group };

 


    public enum LogCategory
    {
        Info, Error, Warning
    }
    [Table("logs")]
    public class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LogDate { get; set; }
        public string Message { get; set; }
        public LogCategory Category { get; set; }
    }

    [Table("userprofile")]
    public class UserProfile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Alamat { get; set; }
        public string? KTP { get; set; }
        public string? PicUrl { get; set; }
        public bool Aktif { get; set; } = true;
        public string? Daerah { get; set; }
        public string? Desa { get; set; }
        public string? Kelompok { get; set; }
        public Char Gender { get; set; } = 'N';
        public Roles Role { set; get; } = Roles.User;
        public DateTime CreatedDate { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public AccountTypes AccountType { set; get; } = AccountTypes.Freelancer;
        public string? FirstName { set; get; }
        public string? LastName { set; get; }
        public string? Skill { set; get; }
        public double? RatePerMonth { set; get; }
        public double? RatePerHour { set; get; }
        public string? TagLine { set; get; }
        public string? Nationality { set; get; }
        public string? AboutMe { set; get; }
        public int Rating { set; get; } = 0;
        public int? JobSuccessPercent { set; get; } = 0;
        public string? FBUrl { set; get; }
        public string? TwitterUrl { set; get; }
        public string? GithubUrl { set; get; }
        public string? InstagramUrl { set; get; }
        public string? LinkedIdUrl { set; get; }

        public bool IsOnline { get; set; } = true;

        [InverseProperty(nameof(MessageHeader.User))]
        public ICollection<MessageHeader> UserMessage { get; set; }

        [InverseProperty(nameof(BookmarkedFreelancer.User))]
        public ICollection<BookmarkedFreelancer> BookmarkedFreelancers { get; set; }

        [InverseProperty(nameof(BookmarkedJob.User))]
        public ICollection<BookmarkedJob> BookmarkedJobs { get; set; }

        [InverseProperty(nameof(Review.FreelancerUser))]
        public ICollection<Review> ReviewByEmployers { get; set; }

        public ICollection<UserProfileAttachment> Attachments { get; set; }
        public ICollection<EmploymentHistory> EmploymentHistories { get; set; }
        public ICollection<UserProfile> PageViews { get; set; }
        public ICollection<Note> Notes { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Testimonial> Testimonials { get; set; }
        [InverseProperty(nameof(Job.Employer))]
        public ICollection<Job> VacancyList { get; set; }


    }

    [Table("userprofile_attachment")]
    public class UserProfileAttachment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey("UserProfile")]
        public long UserId { set; get; }
        public UserProfile User { get; set; }
        public string AttachmentType { set; get; }
        public string Title { set; get; }
        public string Extension { set; get; }
        public string LinkUrl { set; get; }
    }

    [Table("bookmarked_job")]
    public class BookmarkedJob
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(Job))]
        public long JobId { set; get; }
        public Job Job { set; get; }
        [ForeignKey(nameof(User))]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public DateTime CreatedDate { set; get; }
    }

    [Table("employment_history")]
    public class EmploymentHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey("UserProfile")]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string CompanyName { set; get; }
        public DateTime? StartDate { set; get; }
        public DateTime? EndDate { set; get; }
        public string Description { set; get; }
        public string? CompanyPicUrl { set; get; }
    }

    [Table("page_view")]
    public class PageView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public string PageName { set; get; }
        public string PageUrl { set; get; }
        [ForeignKey("UserProfile")]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string Agent { set; get; }
        public DateTime HitDate { set; get; }
    }

    [Table("note")]
    public class Note
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public DateTime CreatedDate { set; get; }
        public string Message { set; get; }
        public NotePriority Priority { set; get; }
        [ForeignKey("UserProfile")]
        public long UserId { set; get; }
        public UserProfile User { set; get; }

    }

    [Table("order")]
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public string OrderNo { set; get; }
        public string PackageName { set; get; }
        public double Amount { set; get; }
        public DateTime DueDate { set; get; }
        public string BillType { set; get; }
        public string PaymentMethod { set; get; }
        public double VAT { set; get; }
        public double Total { set; get; }
        public bool IsPaid { set; get; } = false;
        [ForeignKey("UserProfile")]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string AttachmentUrl { set; get; }
    }

    [Table("company")]
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(User))]
        public long UserId { set; get; }
        public UserProfile User { set; get; }

        public string? FullName { set; get; }
        public string? Category { set; get; }
        public int Rating { set; get; }
        public string? Location { set; get; }
        public string? About { set; get; }
        public string? Longitude { set; get; }
        public string? Latitude { set; get; }
        public string? Address { set; get; }
        public string? FBLink { set; get; }
        public string? TwitterLink { set; get; }
        public string? GithubLink { set; get; }
        public string? WebUrl { set; get; }
        public bool IsVerified { set; get; }

        public ICollection<BookmarkedCompany> BookmarkedCompanies { get; set; }
    }

    [Table("bookmarked_company")]
    public class BookmarkedCompany
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(Company))]
        public long CompanyId { set; get; }
        public Company Company { get; set; }
        [ForeignKey(nameof(User))]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string CreatedDate { set; get; }
    }

    [Table("bookmarked_freelancer")]
    public class BookmarkedFreelancer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(FreelancerUser)), Column(Order = 0)]
        public long FreelancerId { set; get; }
        public UserProfile FreelancerUser { set; get; }
        [ForeignKey(nameof(User)), Column(Order = 1)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public DateTime CreatedDate { set; get; }
    }
    [Table("review")]
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public ReviewTypes ReviewType { set; get; }
        public DateTime CreatedDate { set; get; }
        public bool IsReviewed { set; get; }
        public int Rating { set; get; }
        public bool OnBudget { set; get; }
        public bool OnTime { set; get; }
        public string Message { set; get; }
        [ForeignKey(nameof(Project)), Column(Order = 0)]
        public long ProjectId { set; get; }
        public Project Project { set; get; }
        [ForeignKey(nameof(Employer)), Column(Order = 1)]
        public long EmployerId { set; get; }
        public UserProfile Employer { set; get; }
        [ForeignKey(nameof(FreelancerUser)), Column(Order = 2)]
        public long FreelancerUserId { set; get; }
        public UserProfile FreelancerUser { set; get; }
    }

    [Table("project")]
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public string ProjectName { set; get; }
        public string Category { set; get; }
        public string Location { set; get; }
        public string? Longitude { set; get; }
        public string? Latitude { set; get; }
        public double BudgetMin { set; get; }
        public double BudgetMax { set; get; }
        public string Skills { set; get; }
        public ProjectPaymentTypes ProjectPaymentType { set; get; }
        public string ProjectDesc { set; get; }
        public string AttachmentUrls { set; get; }
        public bool IsCompleted { set; get; } = false;
        public bool Active { set; get; } = true;
        public bool IsBidActive { set; get; } = true;
        [ForeignKey(nameof(Winner)), Column(Order = 0)]
        public long WinnerId { set; get; }
        public UserProfile Winner { set; get; }
        public DateTime ExpiryDate { set; get; }

        public ICollection<Review> Reviews { get; set; }
    }

    [Table("review_company")]
    public class ReviewCompany
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(Company)), Column(Order = 0)]
        public long CompanyId { set; get; }
        public Company Company { set; get; }
        public string Fullname { set; get; }
        public string Title { set; get; }
        public string Message { set; get; }
        public int Rating { set; get; }
        public DateTime CreatedDate { set; get; }
    }

    [Table("testimonial")]
    public class Testimonial
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(User)), Column(Order = 1)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string Message { set; get; }
        public string CreatedDate { set; get; }
    }

    [Table("job")]
    public class Job
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(Company)), Column(Order = 1)]
        public long CompanyId { set; get; }
        public Company Company { set; get; }
        [ForeignKey(nameof(Employer)), Column(Order = 1)]
        public long EmployerId { set; get; }
        public UserProfile Employer { set; get; }
        public string? JobTitle { set; get; }
        public string? JobType { set; get; }
        public string? JobCategory { set; get; }
        public string? Location { set; get; }
        public string? Longitude { set; get; }
        public string? Latitude { set; get; }
        public double SalaryMin { set; get; }
        public double SalaryMax { set; get; }
        public string? Tags { set; get; }
        public string? JobDesc { set; get; }
        public string AttachmentUrls { set; get; }
        public bool Active { set; get; } = true;
        public DateTime CreatedDate { set; get; }
        public ICollection<JobCandidate> JobCandidates { get; set; }
    }

    [Table("job_candidate")]
    public class JobCandidate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [ForeignKey(nameof(Job)), Column(Order = 0)]
        public long JobId { set; get; }
        public Job Job { set; get; }
        [ForeignKey(nameof(Candidate)), Column(Order = 1)]
        public long CandidateId { set; get; }
        public UserProfile Candidate { set; get; }
        public string CVUrl { set; get; }
        public DateTime ApplyDate { set; get; }

        public ICollection<ProjectBidder> ProjectBidders { get; set; }
    }

    [Table("project_bidder")]
    public class ProjectBidder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(Project)), Column(Order = 0)]
        public long ProjectId { set; get; }
        public Project Project { set; get; }
        [ForeignKey(nameof(UserBidder)), Column(Order = 1)]
        public long UserBidderId { set; get; }
        public UserProfile UserBidder { set; get; }
        public TimeSpan DeliveryTime { set; get; }
        public double OfferingPrice { set; get; }
        public bool IsApproved { set; get; } = false;
        public DateTime BidDate { get; set; }
    }

    [Table("blog")]
    public class Blog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Uid { set; get; }
        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public DateTime CreatedDate { set; get; }
        public string Body { set; get; }
        public string Title { set; get; }
        public string? Tags { set; get; }
        public string? ImageUrl { set; get; }

        public ICollection<BlogComment> BlogComments { get; set; }

    }

    [Table("blog_comment")]
    public class BlogComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(Blog)), Column(Order = 0)]
        public long BlogId { set; get; }
        public Blog Blog { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Comment { set; get; }
        public DateTime CreatedDate { set; get; }
    }

    [Table("contact")]
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public string FullName { set; get; }
        public string Email { set; get; }
        public string Subject { set; get; }
        public string Message { set; get; }
        public DateTime CreatedDate { set; get; }
        public string? ReplyMessage { set; get; }
        public string? ReplyBy { set; get; }
        public DateTime? ReplyDate { set; get; }
    }
    public enum ProjectPaymentTypes { Fix, Hourly};

    public enum ReviewTypes { Project, Freelancer };
    public enum NotePriority { Low, Med, High }

    public enum AccountTypes { Employeers, Freelancer }

    public enum Roles { Admin, User, Pengurus }


}
