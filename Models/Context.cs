using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using HelperPlan.DTO.JobDTO;
using HelperPlan.DTO.JobDTO.JobSubBranches.AboutYouClass;
using HelperPlan.DTO.JobDTO.JobSubBranches.JobRequirmentsClass;
using HelperPlan.DTO.JobDTO.JobSubBranches.AboutYouClass.OffersToCandidate;

namespace HelperPlan.Models
{
    public class Context : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobRequirments> JobRequirments { get; set; }
        public DbSet<BasicInformation>  BasicInformation { get; set; }
        public DbSet<RequiredSkillsAndDuties> RequiredSkillsAndDuties { get; set; }
        public DbSet<CandidatePreference> CandidatePreference { get; set; }
        public DbSet<JobDetails> JobDetails { get; set; }
        public DbSet<AboutYou> AboutYou { get; set; }
        public DbSet<AboutYouSub> AboutYouSub { get; set; }
        public DbSet<offersToCandidate> offersToCandidate { get; set; }
        public DbSet<MonthlySalaryOffer> MonthlySalaryOffer { get; set; }
        public DbSet<EmployerType> EmployerType { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<SubscribtionHistory> SubscribtionHistories { get; set; }
        public DbSet<AspNetFaceBookUsers> AspNetFaceBookUsers { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().UseTptMappingStrategy();
            builder.Entity<Subscribtion>(entity =>
                { 
                entity.HasKey(e => e.ID);

                // Employer relationship
                entity.HasOne(e => e.Employer)
                    .WithMany()
                    .HasForeignKey(e => e.UserID)
                    .OnDelete(DeleteBehavior.Restrict); // Use appropriate delete behavior

                // Plan relationship
                entity.HasOne(e => e.Plan)
                    .WithMany()
                    .HasForeignKey(e => e.PlanID)
                    .OnDelete(DeleteBehavior.Restrict); // Use appropriate delete behavior

                // Ensure EmployerID and PlanID are not mapped twice
                entity.Property(e => e.UserID).IsRequired();
                entity.Property(e => e.PlanID).IsRequired();
           }
        );
            
            base.OnModelCreating(builder);  
        }
    }
}
