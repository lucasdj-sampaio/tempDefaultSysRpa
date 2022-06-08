﻿// <auto-generated />
using System;
using COE000.Portal.NomeProjeto.Reposity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace COE000.Portal.NomeProjeto.Migrations.DataBase
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
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

                    b.Property<string>("Nick")
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

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.DataChargeModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("ChargeId")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("ClientDocument")
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime?>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("EC")
                        .HasColumnType("bigint");

                    b.Property<Guid>("HistoricId")
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("HistoricCode");

                    b.Property<string>("Observation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("UraPhone")
                        .HasColumnType("bigint");

                    b.Property<long>("WppNumber")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("HistoricId");

                    b.ToTable("TB_DataCharge", (string)null);
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.EnvironmentModel", b =>
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

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.FunctionTypeModel", b =>
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

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.HistoricModel", b =>
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

                    b.Property<string>("Observation")
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR(200)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(450)")
                        .HasColumnName("UserCode");

                    b.HasKey("Id");

                    b.HasIndex("FunctionTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("TB_Historic", (string)null);
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.RpaCredentialModel", b =>
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

                    b.HasIndex("EnvironmentId");

                    b.ToTable("TB_RpaCredential", (string)null);
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.StatusModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("StatusCode")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("DateOn")
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
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR(200)");

                    b.Property<int>("StatusTypeId")
                        .HasColumnType("int")
                        .HasColumnName("TypeCode");

                    b.HasKey("Id");

                    b.HasIndex("EnvironmentId");

                    b.HasIndex("HistoricId");

                    b.HasIndex("StatusTypeId");

                    b.ToTable("TB_Status", (string)null);
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.StatusTypeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TypeCode");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR(30)");

                    b.HasKey("Id");

                    b.ToTable("TB_StatusType", (string)null);
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.DataChargeModel", b =>
                {
                    b.HasOne("COE334.Portal.FirstData.Models.Entity.HistoricModel", "Historic")
                        .WithMany("DataChargeGroup")
                        .HasForeignKey("HistoricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Historic");
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.HistoricModel", b =>
                {
                    b.HasOne("COE334.Portal.FirstData.Models.Entity.FunctionTypeModel", "FunctionType")
                        .WithMany("HistoricGroup")
                        .HasForeignKey("FunctionTypeId")
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

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.RpaCredentialModel", b =>
                {
                    b.HasOne("COE334.Portal.FirstData.Models.Entity.EnvironmentModel", "Environment")
                        .WithMany("RpaCredentiaGroup")
                        .HasForeignKey("EnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Environment");
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.StatusModel", b =>
                {
                    b.HasOne("COE334.Portal.FirstData.Models.Entity.EnvironmentModel", "Environment")
                        .WithMany("StatusGroup")
                        .HasForeignKey("EnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("COE334.Portal.FirstData.Models.Entity.HistoricModel", "Historic")
                        .WithMany("StatusGroup")
                        .HasForeignKey("HistoricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("COE334.Portal.FirstData.Models.Entity.StatusTypeModel", "StatusType")
                        .WithMany("StatusGroup")
                        .HasForeignKey("StatusTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Environment");

                    b.Navigation("Historic");

                    b.Navigation("StatusType");
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Areas.Identity.Data.IncriseUserModel", b =>
                {
                    b.Navigation("HistoricCollection");
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.EnvironmentModel", b =>
                {
                    b.Navigation("RpaCredentiaGroup");

                    b.Navigation("StatusGroup");
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.FunctionTypeModel", b =>
                {
                    b.Navigation("HistoricGroup");
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.HistoricModel", b =>
                {
                    b.Navigation("DataChargeGroup");

                    b.Navigation("StatusGroup");
                });

            modelBuilder.Entity("COE334.Portal.FirstData.Models.Entity.StatusTypeModel", b =>
                {
                    b.Navigation("StatusGroup");
                });
#pragma warning restore 612, 618
        }
    }
}