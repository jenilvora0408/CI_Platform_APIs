using Common.Constants;
using Common.Enums;
using Common.Utils;
using Entities.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => (IsBaseEntity(e.Entity.GetType()) || IsTimestampedEntity(e.Entity.GetType())) &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (IsBaseEntity(entityEntry.Entity.GetType()))
                {
                    var baseEntity = (dynamic)entityEntry.Entity;
                    long userId = GetUserId();

                    if (entityEntry.State == EntityState.Added)
                    {
                        baseEntity.CreatedOn = DateTimeProvider.GetCurrentDateTime();
                        baseEntity.CreatedBy = userId;
                    }
                    baseEntity.ModifiedOn = DateTimeProvider.GetCurrentDateTime();
                    baseEntity.ModifiedBy = userId;
                }
                else if (IsTimestampedEntity(entityEntry.Entity.GetType()))
                {
                    var timeStampedEntity = (dynamic)entityEntry.Entity;

                    if (entityEntry.State == EntityState.Added)
                    {
                        timeStampedEntity.CreatedOn = DateTimeProvider.GetCurrentDateTime();
                    }
                    timeStampedEntity.ModifiedOn = DateTimeProvider.GetCurrentDateTime();
                }
                else if (IsAuditableEntity(entityEntry.Entity.GetType()))
                {
                    var auditableEnity = (dynamic)entityEntry.Entity;
                    long userId = GetUserId();
                    if (entityEntry.State == EntityState.Added)
                    {
                        auditableEnity.CreatedBy = userId;
                    }
                    auditableEnity.ModifiedBy = userId;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private long GetUserId()
        {
            return long.Parse(_httpContextAccessor.HttpContext.User.Identity?.Name ?? "0");
        }

        private static bool IsAuditableEntity(Type entityType)
        {
            var baseType = entityType.BaseType;
            while (baseType != null)
            {
                if (baseType.IsGenericType &&
                    baseType.GetGenericTypeDefinition() == typeof(AuditableEntity<>))
                {
                    return true;
                }
                baseType = baseType.BaseType;
            }
            return false;
        }

        private static bool IsTimestampedEntity(Type entityType)
        {
            var baseType = entityType.BaseType;
            while (baseType != null)
            {
                if (baseType.IsGenericType &&
                    baseType.GetGenericTypeDefinition() == typeof(TimestampedEntity<>))
                {
                    return true;
                }
                baseType = baseType.BaseType;
            }
            return false;
        }

        private static bool IsBaseEntity(Type entityType)
        {
            var baseType = entityType.BaseType;
            while (baseType != null)
            {
                if (baseType.IsGenericType &&
                    baseType.GetGenericTypeDefinition() == typeof(BaseEntity<>))
                {
                    return true;
                }
                baseType = baseType.BaseType;
            }
            return false;
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Admin> Admins { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<MissionTheme> MissionThemes { get; set; }

        public virtual DbSet<Skill> Skills { get; set; }

        public virtual DbSet<Mission> Missions { get; set; }

        public virtual DbSet<MissionMedia> MissionMedias { get; set; }

        public virtual DbSet<MissionSkills> MissionSkills { get; set; }

        public virtual DbSet<Volunteer> Volunteers { get; set; }

        public virtual DbSet<VolunteerSkills> VolunteerSkills { get; set; }

        public virtual DbSet<Banner> Banners { get; set; }

        public virtual DbSet<CMSPage> CMSPages { get; set; }

        public virtual DbSet<MissionRating> MissionRatings { get; set; }

        public virtual DbSet<FavouriteMission> FavouriteMissions { get; set; }

        public virtual DbSet<Story> Stories { get; set; }

        public virtual DbSet<StoryMedia> StoryMedias { get; set; }

        public virtual DbSet<MissionComment> MissionComments { get; set; }
        
        public virtual DbSet<Timesheet> Timesheets { get; set; }
        
        public virtual DbSet<MissionApplication> MissionApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Avatar)
                    .HasDefaultValueSql<string>(SystemConstant.DEFAULT_AVATAR);
                entity.Property(e => e.CreatedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.ModifiedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.UserType).HasDefaultValue(UserType.Volunteer);
                entity.Property(e => e.Status).HasDefaultValue(UserStatus.ACTIVE);
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admins");
                entity
                    .HasOne(a => a.User)
                    .WithOne()
                    .HasForeignKey<Admin>(a => a.UserId)
                    .IsRequired();
                entity
                    .HasOne(a => a.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<MissionTheme>(entity =>
            {
                entity.ToTable("mission_themes");
                entity.HasIndex(e => e.Title).IsUnique();
                entity.Property(e => e.CreatedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.ModifiedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.Status).HasDefaultValue(MissionThemeStatus.ACTIVE);
                entity
                    .HasOne(a => a.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("countries");
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.CreatedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.ModifiedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.Status).HasDefaultValue(CountryStatus.ACTIVE);
                entity
                    .HasOne(a => a.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("cities");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.ModifiedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.Status).HasDefaultValue(CityStatus.ACTIVE);
                entity
                    .HasOne(a => a.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("skills");
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.CreatedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.ModifiedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.Status).HasDefaultValue(SkillStatus.ACTIVE);
                entity
                    .HasOne(a => a.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.ToTable("volunteers");
                entity
                    .HasOne(a => a.User)
                    .WithOne()
                    .HasForeignKey<Volunteer>(a => a.UserId)
                    .IsRequired();
                entity
                    .HasOne(a => a.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.City)
                    .WithMany()
                    .HasForeignKey(a => a.CityId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.Property(e => e.Availability).HasDefaultValue(AvailabilityType.Daily);
            });

            modelBuilder.Entity<Mission>(entity =>
            {
                entity.ToTable("missions");
                entity
                    .HasOne(a => a.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.City)
                    .WithMany()
                    .HasForeignKey(a => a.CityId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.MissionTheme)
                    .WithMany()
                    .HasForeignKey(a => a.MissionThemeId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<MissionSkills>(entity =>
            {
                entity.ToTable("mission_skills");
            });

            modelBuilder.Entity<MissionMedia>(entity =>
            {
                entity.ToTable("mission_media");
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.ToTable("banners");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.ModifiedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.Status).HasDefaultValue(BannerStatus.ACTIVE);
                //entity.HasIndex(e => e.SortOrder).IsUnique();
                entity
                   .HasOne(a => a.CreatedByUser)
                   .WithMany()
                   .HasForeignKey(a => a.CreatedBy)
                   .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<CMSPage>(entity =>
            {
                entity.ToTable("cms_pages");
                entity.HasIndex(e => e.Slug).IsUnique();
                entity.Property(e => e.CreatedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.ModifiedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.Status).HasDefaultValue(CMSPageStatus.ACTIVE);
                entity
                    .HasOne(a => a.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<MissionRating>(entity =>
            {
                entity.ToTable("mission_ratings");
                entity.Property(e => e.CreatedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.ModifiedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);

                entity
                    .HasIndex(e => new { e.MissionId, e.VolunteerId })
                    .IsUnique();
            });

            modelBuilder.Entity<Story>(entity =>
            {
                entity.ToTable("stories");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.ModifiedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.Status).HasDefaultValue(StoryStatus.DRAFT);
                entity
                   .HasOne(a => a.CreatedByUser)
                   .WithMany()
                   .HasForeignKey(a => a.CreatedBy)
                   .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                   .HasOne(a => a.Mission)
                   .WithMany()
                   .HasForeignKey(a => a.MissionId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();
                entity
                    .HasOne(a => a.Volunteer)
                    .WithMany()
                    .HasForeignKey(a => a.VolunteerId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            modelBuilder.Entity<FavouriteMission>(entity =>
            {
                entity.ToTable("favourite_missions");
                entity.Property(e => e.CreatedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.ModifiedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity
                    .HasIndex(e => new { e.MissionId, e.VolunteerId })
                    .IsUnique();

            });

            modelBuilder.Entity<StoryMedia>(entity =>
            {
                entity.ToTable("story_media");

            });

            modelBuilder.Entity<MissionApplication>(entity =>
            {
                entity.ToTable("mission_applications");

                entity
                   .HasOne(a => a.CreatedByUser)
                   .WithMany()
                   .HasForeignKey(a => a.CreatedBy)
                   .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasIndex(e => new { e.MissionId, e.VolunteerId })
                    .IsUnique();
            });
            modelBuilder.Entity<VolunteerSkills>(entity =>
            {
                entity.ToTable("volunteer_skills");
            });

            modelBuilder.Entity<MissionComment>(entity =>
            {
                entity.ToTable("mission_comments");
                entity.Property(e => e.CreatedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.ModifiedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.Status).HasDefaultValue(CommentStatus.PENDING);
                entity
                    .HasOne(missionComment => missionComment.Volunteer)
                    .WithMany()
                    .HasForeignKey(missionComment => missionComment.VolunteerId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Timesheet>(entity =>
            {
                entity.ToTable("timesheets");
                entity.Property(e => e.CreatedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.ModifiedOn).HasDefaultValueSql(SystemConstant.DEFAULT_DATETIME);
                entity.Property(e => e.Status).HasDefaultValue(TimesheetStatus.PENDING);
                entity
                    .HasOne(a => a.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(a => a.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            //Seed data
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                FirstName = "admin",
                LastName = "admin",
                Email = "noreply.ci.tatvasoft@gmail.com",
                Password = "2ED0227C628B3FE95C30B387A0B4AC0382648BCF1AA59882C2D68F216080DEBE994A9A3DA9CA59E5B67E8A73DE2583B1220A098B20F3C4BE2511B737B09BBB6D",
                Salt = "E29C23D44A0CD818CDD21A8D01C3C7326A372A172B4071359FFA76379A8D1D6443265653161768992CC36D262780A79BA38F1ED697729FA6B35A536148355157",
                Avatar = "/assets/avatar/profile.png",
                UserType = UserType.Admin,
                Status = UserStatus.ACTIVE,
                CreatedOn = new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(3991), new TimeSpan(0, 5, 30, 0, 0)),
                ModifiedOn = new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(3991), new TimeSpan(0, 5, 30, 0, 0)),
            });

            modelBuilder.Entity<Admin>().HasData(new Admin()
            {
                Id = 1,
                UserId = 1,
                CreatedBy = 1,
                ModifiedBy = 1
            });

            modelBuilder.Entity<Banner>().HasData(new Banner()
            {
                Id = 1,
                Image = "/assets/banner/CSR-initiative-stands-for-Coffee--and-Farmer-Equity.png",
                Description = @"At [Organization Name], our mission is to promote a sustainable and greener future by actively participating in tree planting initiatives. We firmly believe that trees play a vital role in maintaining a healthy environment, combating climate change, and improving the overall well-being of our communities. Through our dedicated efforts, we aim to foster a culture of tree planting, nurture ecosystems, and inspire individuals to become stewards of the environment.
Our mission begins with raising awareness about the importance of trees and their positive impact on the planet. We strive to educate individuals, schools, businesses, and local communities about the numerous benefits of trees, such as improving air quality, conserving water, providing habitat for wildlife, and reducing the carbon footprint. By highlighting the intrinsic value of trees, we aim to inspire a sense of responsibility and motivate people to actively participate in our tree planting initiatives.",
                Status = BannerStatus.ACTIVE,
                SortOrder = 1,
                CreatedOn = new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(3991), new TimeSpan(0, 5, 30, 0, 0)),
                ModifiedOn = new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(3991), new TimeSpan(0, 5, 30, 0, 0)),
                CreatedBy = 1,
                ModifiedBy = 1
            });
        }
    }
}