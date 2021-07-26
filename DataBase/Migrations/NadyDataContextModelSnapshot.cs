﻿// <auto-generated />
using System;
using DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataBase.Migrations
{
    [DbContext(typeof(NadyDataContext))]
    partial class NadyDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.17");

            modelBuilder.Entity("DataBase.Models.Member", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsBasic")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<string>("RelationShip")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("DataBase.Models.MemberDetails", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NickName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MemberId")
                        .IsUnique();

                    b.ToTable("MemberDetails");
                });

            modelBuilder.Entity("DataBase.Models.MemberHistory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Added")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Ended")
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Preserved")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Resumed")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Separated")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MemberId")
                        .IsUnique();

                    b.ToTable("MemberHistories");
                });

            modelBuilder.Entity("DataBase.Models.MemberPayment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<double>("DiscountPercentage")
                        .HasColumnType("REAL");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("PaymentAmount")
                        .HasColumnType("REAL");

                    b.Property<double>("PaymentTotal")
                        .HasColumnType("REAL");

                    b.Property<int>("PaymentType")
                        .HasColumnType("INTEGER");

                    b.Property<double>("TaxPercentage")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("MemberPayments");
                });

            modelBuilder.Entity("DataBase.Models.ScheduledPayment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberPaymentId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("PaymentAmount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("PaymentDueDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MemberPaymentId")
                        .IsUnique();

                    b.ToTable("ScheduledPayments");
                });

            modelBuilder.Entity("DataBase.Models.MemberDetails", b =>
                {
                    b.HasOne("DataBase.Models.Member", null)
                        .WithOne("MemberDetails")
                        .HasForeignKey("DataBase.Models.MemberDetails", "MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataBase.Models.MemberHistory", b =>
                {
                    b.HasOne("DataBase.Models.Member", null)
                        .WithOne("MemberHistory")
                        .HasForeignKey("DataBase.Models.MemberHistory", "MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataBase.Models.MemberPayment", b =>
                {
                    b.HasOne("DataBase.Models.Member", null)
                        .WithMany("MemberPayments")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataBase.Models.ScheduledPayment", b =>
                {
                    b.HasOne("DataBase.Models.MemberPayment", null)
                        .WithOne("ScheduledPayment")
                        .HasForeignKey("DataBase.Models.ScheduledPayment", "MemberPaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
