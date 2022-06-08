using COE000.Portal.NomeProjeto.Reposity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable

namespace COE000.Portal.NomeProjeto.Migrations.DataBase
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20220502193211_IdentityMigration")]
    partial class IdentityMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("COE334.Portal.FirstData.Areas.Identity.Data.IncriseUserModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserGender")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)")
                        .HasColumnName("Gender");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.DataChargeModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("ChargeId")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("ClientDocument")
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("DateEnd")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("EC")
                        .HasColumnType("int");

                    b.Property<Guid>("HistoricId")
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("HistoricCode");

                    b.Property<string>("Observation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HistoricId");

                    b.ToTable("TB_DataCharge", (string)null);
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.EnvironmentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EnvCode");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("EnvironmentName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR(30)");

                    b.HasKey("Id");

                    b.ToTable("TB_Environment", (string)null);
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.FunctionTypeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FunctionCode");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR(200)");

                    b.Property<string>("FunctionName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR(30)");

                    b.HasKey("Id");

                    b.ToTable("TB_FunctionType", (string)null);
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.HistoricModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("HistoricCode")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("DateOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("FunctionTypeId")
                        .HasColumnType("int")
                        .HasColumnName("FunctionCode");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(450)")
                        .HasColumnName("UserCode");

                    b.HasKey("Id");

                    b.HasIndex("FunctionTypeId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("TB_Historic", (string)null);
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.RpaCredentialModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("CredentionCode")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<int>("EnvironmentId")
                        .HasColumnType("int")
                        .HasColumnName("EnvCode");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR(200)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR(80)");

                    b.HasKey("Id");

                    b.HasIndex("EnvironmentId")
                        .IsUnique();

                    b.ToTable("TB_RpaCredential", (string)null);
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.StatusModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("StatusCode")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime?>("DateOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("EnvironmentId")
                        .HasColumnType("int")
                        .HasColumnName("EnvCode");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR(200)");

                    b.Property<Guid>("HistoricId")
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("HistoricCode");

                    b.Property<string>("Observation")
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR(80)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR(200)");

                    b.HasKey("Id");

                    b.HasIndex("EnvironmentId")
                        .IsUnique();

                    b.HasIndex("HistoricId");

                    b.ToTable("TB_Status", (string)null);
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.DataChargeModel", b =>
                {
                    b.HasOne("COE334.Portal.FirstData.Models.HistoricModel", "Historic")
                        .WithMany("dataChargeGroup")
                        .HasForeignKey("HistoricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Historic");
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.HistoricModel", b =>
                {
                    b.HasOne("COE334.Portal.FirstData.Models.FunctionTypeModel", "FunctionType")
                        .WithOne("Historic")
                        .HasForeignKey("COE334.Portal.FirstData.Models.HistoricModel", "FunctionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("COE334.Portal.FirstData.Areas.Identity.Data.IncriseUserModel", "User")
                        .WithMany("HistoricCollection")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FunctionType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.RpaCredentialModel", b =>
                {
                    b.HasOne("COE334.Portal.FirstData.Models.EnvironmentModel", "Environment")
                        .WithOne("RpaCredentialModel")
                        .HasForeignKey("COE334.Portal.FirstData.Models.RpaCredentialModel", "EnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Environment");
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.StatusModel", b =>
                {
                    b.HasOne("COE334.Portal.FirstData.Models.EnvironmentModel", "Environment")
                        .WithOne("StatusModel")
                        .HasForeignKey("COE334.Portal.FirstData.Models.StatusModel", "EnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("COE334.Portal.FirstData.Models.HistoricModel", "Historic")
                        .WithMany("StatusGroup")
                        .HasForeignKey("HistoricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Environment");

                    b.Navigation("Historic");
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Areas.Identity.Data.IncriseUserModel", b =>
                {
                    b.Navigation("HistoricCollection");
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.EnvironmentModel", b =>
                {
                    b.Navigation("RpaCredentialModel")
                        .IsRequired();

                    b.Navigation("StatusModel")
                        .IsRequired();
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.FunctionTypeModel", b =>
                {
                    b.Navigation("Historic")
                        .IsRequired();
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.HistoricModel", b =>
                {
                    b.Navigation("StatusGroup");

                    b.Navigation("dataChargeGroup");
                });
#pragma warning restore 612, 618
        }
    }
}
