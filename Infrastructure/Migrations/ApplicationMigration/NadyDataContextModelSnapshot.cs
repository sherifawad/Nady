﻿// <auto-generated />
using System;
using DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations.ApplicationMigration
{
    [DbContext(typeof(NadyDataContext))]
    partial class NadyDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.17");

            modelBuilder.Entity("Core.Models.Member", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasMaxLength(66);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("TEXT");

                    b.Property<long>("CreatedDate")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsOwner")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MemberStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ModifiedByUser")
                        .HasColumnType("TEXT");

                    b.Property<long>("ModifiedDate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Note")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("RelationShip")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Core.Models.MemberDetails", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasMaxLength(66);

                    b.Property<string>("Address")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("TEXT");

                    b.Property<long>("CreatedDate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("ModifiedByUser")
                        .HasColumnType("TEXT");

                    b.Property<long>("ModifiedDate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("MemberDetails");
                });

            modelBuilder.Entity("Core.Models.MemberHistory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasMaxLength(66);

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("TEXT");

                    b.Property<long>("CreatedDate")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Date")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Detail")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(66);

                    b.Property<string>("ModifiedByUser")
                        .HasColumnType("TEXT");

                    b.Property<long>("ModifiedDate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("MemberHistories");
                });

            modelBuilder.Entity("Core.Models.MemberPayment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasMaxLength(66);

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("TEXT");

                    b.Property<long>("CreatedDate")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Date")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("DiscountPercentage")
                        .HasColumnType("REAL");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(66);

                    b.Property<string>("ModifiedByUser")
                        .HasColumnType("TEXT");

                    b.Property<long>("ModifiedDate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Note")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<int?>("PaymentMethod")
                        .HasColumnType("INTEGER");

                    b.Property<double>("PaymentTotal")
                        .HasColumnType("REAL");

                    b.Property<int>("PaymentType")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("TaxPercentage")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("MemberPayments");
                });

            modelBuilder.Entity("Core.Models.MemberVisitor", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasMaxLength(66);

                    b.Property<long?>("AccessesDate")
                        .HasColumnType("INTEGER");

                    b.Property<long>("AddedDate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("TEXT");

                    b.Property<long>("CreatedDate")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Gate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(66);

                    b.Property<string>("ModifiedByUser")
                        .HasColumnType("TEXT");

                    b.Property<long>("ModifiedDate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<int>("VisitorStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VisitorType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("MemberVisitors");
                });

            modelBuilder.Entity("Core.Models.ScheduledPayment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasMaxLength(66);

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("TEXT");

                    b.Property<long>("CreatedDate")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Fulfiled")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("FulfiledDate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MemberPaymentId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(66);

                    b.Property<string>("ModifiedByUser")
                        .HasColumnType("TEXT");

                    b.Property<long>("ModifiedDate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<double>("PaymentAmount")
                        .HasColumnType("REAL");

                    b.Property<long>("PaymentDueDate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MemberPaymentId");

                    b.ToTable("ScheduledPayments");
                });

            modelBuilder.Entity("DataBase.Models.Audit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditType")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditUser")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("KeyValues")
                        .HasColumnType("TEXT");

                    b.Property<string>("NewValues")
                        .HasColumnType("TEXT");

                    b.Property<string>("OldValues")
                        .HasColumnType("TEXT");

                    b.Property<string>("TableName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("Core.Models.MemberDetails", b =>
                {
                    b.HasOne("Core.Models.Member", "Member")
                        .WithOne("MemberDetails")
                        .HasForeignKey("Core.Models.MemberDetails", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Models.MemberHistory", b =>
                {
                    b.HasOne("Core.Models.Member", "Member")
                        .WithMany("MemberHistoriesList")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Models.MemberPayment", b =>
                {
                    b.HasOne("Core.Models.Member", "Member")
                        .WithMany("MemberPayments")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Models.MemberVisitor", b =>
                {
                    b.HasOne("Core.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Models.ScheduledPayment", b =>
                {
                    b.HasOne("Core.Models.MemberPayment", "MemberPayment")
                        .WithMany("ScheduledPayments")
                        .HasForeignKey("MemberPaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}