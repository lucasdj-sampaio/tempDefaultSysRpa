#region - Imports
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using COE000.Portal.NomeProjeto.Models.Entity;
using COE000.Portal.NomeProjeto.Areas.Identity.Data;
#pragma warning disable CS8618
#endregion

namespace COE000.Portal.NomeProjeto.Reposity.Entity
{
    public sealed class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> preferences) 
            : base(preferences) { }

        public DbSet<EnvironmentModel> DbEnvironment { get; set; }

        public DbSet<HistoricModel> DbHistoric { get; set; }

        public DbSet<HashModel> DdHash { get; set; }
        
        public DbSet<RpaCredentialModel> DbRpaCredential { get; set; }

        public DbSet<IncriseUserModel> DbUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            SetEnvironmentBuilder(builder);
                
            SetRpaCredentialBuilder(builder);
            
            SetHistoricBuilder(builder);

            SetIncreaseUserBuilder(builder);

            SetHashBuilder(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #region - Builder's set

        private static void SetEnvironmentBuilder(ModelBuilder builder)
        {
            builder.Entity<EnvironmentModel>().ToTable("TB_Environment");

            builder.Entity<EnvironmentModel>().Property(e => e.Id)
                .HasColumnName("EnvCode");
                
            builder.Entity<EnvironmentModel>().Property(e => e.EnvironmentName)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(30);
        }

        private static void SetRpaCredentialBuilder(ModelBuilder builder)
        {
            builder.Entity<RpaCredentialModel>().ToTable("TB_RpaCredential");

            builder.Entity<RpaCredentialModel>().Property(e => e.Id)
                .HasColumnName("CredentionCode")
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasDefaultValueSql("NEWID()");

            builder.Entity<RpaCredentialModel>().Property(e => e.UserName)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(80); 

            builder.Entity<RpaCredentialModel>().Property(e => e.Password)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Entity<RpaCredentialModel>().Property(e => e.Url)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);  

            builder.Entity<RpaCredentialModel>().Property(e => e.EnvironmentId)
                .HasColumnName("EnvCode");

            builder.Entity<RpaCredentialModel>()
                .HasOne<EnvironmentModel>(r => r.Environment)
                .WithMany(e => e.RpaCredentiaGroup)
                .HasForeignKey(fk => fk.EnvironmentId);
        }

        private static void SetHashBuilder(ModelBuilder builder)
        {
            builder.Entity<HashModel>().ToTable("TB_UserConfig");
            builder.Entity<HashModel>().Property(h => h.Id)
                .HasColumnName("ConfigCode")
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasDefaultValueSql("NEWID()");
        }

        private static void SetHistoricBuilder(ModelBuilder builder)
        {
            builder.Entity<HistoricModel>().ToTable("TB_Legacy");

            builder.Entity<HistoricModel>().Property(e => e.Id)
                .HasColumnName("LegacyCode")
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasDefaultValueSql("NEWID()");
            
            builder.Entity<HistoricModel>().Property(e => e.UserId)
                .IsRequired()
                .HasColumnName("UserCode")
                .HasColumnType("NVARCHAR(450)");

            builder.Entity<HistoricModel>().Property(e => e.Observation)
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);

            builder.Entity<HistoricModel>().Property(e => e.FunctionTypeId)
                .IsRequired()
                .HasColumnName("FunctionCode");
            
            builder.Entity<HistoricModel>().Property(e => e.DateOn)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<HistoricModel>()
                .HasOne<IncriseUserModel>(i => i.User)
                .WithMany(h => h.HistoricCollection)
                .HasForeignKey(fk => fk.UserId);

            builder.Entity<HistoricModel>()
                .HasOne<FunctionTypeModel>(h => h.FunctionType)
                .WithMany(f => f.HistoricGroup)
                .HasForeignKey(fk => fk.FunctionTypeId);
        }

        private static void SetIncreaseUserBuilder(ModelBuilder builder) =>
            builder.Entity<IncriseUserModel>().ToTable("AspNetUsers");

        #endregion
    }
}