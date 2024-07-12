﻿// <auto-generated />
using System;
using BridgeIntelligence.iCompass.Shm.Common.DbDesigner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BridgeIntelligence.iCompass.Shm.Common.DbDesigner.Migrations
{
    [DbContext(typeof(DesignerDbContext))]
    [Migration("20240301001230_Init2")]
    partial class Init2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.AlertRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AlertType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateLastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpressionString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFailedState")
                        .HasColumnType("bit");

                    b.Property<string>("ItemCreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemLastModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LastAlertRuleFailureId")
                        .HasColumnType("int");

                    b.Property<string>("RuleName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("SensorOutputId")
                        .HasColumnType("int");

                    b.Property<string>("ToEmailAddresses")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LastAlertRuleFailureId")
                        .IsUnique()
                        .HasFilter("[LastAlertRuleFailureId] IS NOT NULL");

                    b.HasIndex("SensorOutputId");

                    b.ToTable("SHM_AlertRules");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.AlertRuleEmailNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlertRuleId")
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DeliveryStatus")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.HasKey("Id");

                    b.HasIndex("AlertRuleId");

                    b.ToTable("SHM_AlertRuleEmailNotifications");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.AlertRuleFailure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlertRuleId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("EndTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsNotificationSentForFailure")
                        .HasColumnType("bit");

                    b.Property<bool>("IsNotificationSentForResolved")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ToEmailAddresses")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AlertRuleId");

                    b.ToTable("SHM_AlertRuleFailures");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.Bridge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BridgeAbbreviation")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("BridgeName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateLastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ItemCreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemLastModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SHM_Bridges");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.Daq", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BridgeId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateLastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFailedState")
                        .HasColumnType("bit");

                    b.Property<string>("ItemCreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemLastModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastSuccessTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Location")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Model")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<bool>("NotifiedForFailure")
                        .HasColumnType("bit");

                    b.Property<string>("PingUrl")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("SpanOrPier")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("UpstreamDownstream")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("BridgeId");

                    b.ToTable("SHM_Daqs");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.Sensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("AlternateName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("DaqId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateLastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ItemCreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemLastModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Member")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Model")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Position")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("SpanOrPier")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("System")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("UpstreamDownstream")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("DaqId");

                    b.ToTable("SHM_Sensors");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.SensorOutput", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ColumnName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateLastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFailedState")
                        .HasColumnType("bit");

                    b.Property<string>("ItemCreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemLastModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastSuccessTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<float?>("LowerBound")
                        .HasColumnType("real");

                    b.Property<string>("OutputType")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("SamplingFrequency")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("SensorId")
                        .HasColumnType("int");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Unit")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<float?>("UpperBound")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("SHM_SensorOutputs");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.SensorOutputSummary", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float?>("Average")
                        .HasColumnType("real");

                    b.Property<int>("BridgeId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<float?>("FirstQuartile")
                        .HasColumnType("real");

                    b.Property<float?>("Maximum")
                        .HasColumnType("real");

                    b.Property<float?>("Median")
                        .HasColumnType("real");

                    b.Property<float?>("Minimum")
                        .HasColumnType("real");

                    b.Property<int>("SensorOutputId")
                        .HasColumnType("int");

                    b.Property<float?>("StandardDev")
                        .HasColumnType("real");

                    b.Property<float?>("ThirdQuartile")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("BridgeId");

                    b.HasIndex("SensorOutputId");

                    b.ToTable("SHM_SensorOutputSummary");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFailedState")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWorker")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LastSuccessTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("NotifiedForFailure")
                        .HasColumnType("bit");

                    b.Property<string>("PingUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SHM_Services");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.TableState", b =>
                {
                    b.Property<string>("TableName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("LastProcessedTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("LastRecNum")
                        .HasColumnType("bigint");

                    b.HasKey("TableName");

                    b.ToTable("SHM_TableStates");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.AlertRule", b =>
                {
                    b.HasOne("BridgeIntelligence.iCompass.Shm.Common.DbModels.AlertRuleFailure", "LastAlertRuleFailure")
                        .WithOne("AlertRule")
                        .HasForeignKey("BridgeIntelligence.iCompass.Shm.Common.DbModels.AlertRule", "LastAlertRuleFailureId");

                    b.HasOne("BridgeIntelligence.iCompass.Shm.Common.DbModels.SensorOutput", "SensorOutput")
                        .WithMany("AlertRules")
                        .HasForeignKey("SensorOutputId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LastAlertRuleFailure");

                    b.Navigation("SensorOutput");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.AlertRuleEmailNotification", b =>
                {
                    b.HasOne("BridgeIntelligence.iCompass.Shm.Common.DbModels.AlertRule", "AlertRule")
                        .WithMany()
                        .HasForeignKey("AlertRuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AlertRule");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.AlertRuleFailure", b =>
                {
                    b.HasOne("BridgeIntelligence.iCompass.Shm.Common.DbModels.AlertRule", null)
                        .WithMany("AlertRuleFailures")
                        .HasForeignKey("AlertRuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.Daq", b =>
                {
                    b.HasOne("BridgeIntelligence.iCompass.Shm.Common.DbModels.Bridge", "Bridge")
                        .WithMany()
                        .HasForeignKey("BridgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bridge");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.Sensor", b =>
                {
                    b.HasOne("BridgeIntelligence.iCompass.Shm.Common.DbModels.Daq", "Daq")
                        .WithMany("Sensors")
                        .HasForeignKey("DaqId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Daq");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.SensorOutput", b =>
                {
                    b.HasOne("BridgeIntelligence.iCompass.Shm.Common.DbModels.Sensor", "Sensor")
                        .WithMany("SensorOutputs")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sensor");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.SensorOutputSummary", b =>
                {
                    b.HasOne("BridgeIntelligence.iCompass.Shm.Common.DbModels.Bridge", "Bridge")
                        .WithMany()
                        .HasForeignKey("BridgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BridgeIntelligence.iCompass.Shm.Common.DbModels.SensorOutput", "SensorOutput")
                        .WithMany()
                        .HasForeignKey("SensorOutputId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bridge");

                    b.Navigation("SensorOutput");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.AlertRule", b =>
                {
                    b.Navigation("AlertRuleFailures");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.AlertRuleFailure", b =>
                {
                    b.Navigation("AlertRule");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.Daq", b =>
                {
                    b.Navigation("Sensors");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.Sensor", b =>
                {
                    b.Navigation("SensorOutputs");
                });

            modelBuilder.Entity("BridgeIntelligence.iCompass.Shm.Common.DbModels.SensorOutput", b =>
                {
                    b.Navigation("AlertRules");
                });
#pragma warning restore 612, 618
        }
    }
}
