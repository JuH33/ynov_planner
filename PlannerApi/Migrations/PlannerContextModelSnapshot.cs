﻿// <auto-generated />
using events_planner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace events_planner.Migrations
{
    [DbContext(typeof(PlannerContext))]
    partial class PlannerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("events_planner.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("booking_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventId")
                        .HasColumnName("event_id");

                    b.Property<bool>("Present")
                        .HasColumnName("present");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("booking");
                });

            modelBuilder.Entity("events_planner.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasMaxLength(20);

                    b.Property<int?>("SubCategoryId")
                        .HasColumnName("sub_category_id");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("SubCategoryId");

                    b.ToTable("category");
                });

            modelBuilder.Entity("events_planner.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("event_id");

                    b.Property<DateTime?>("CloseAt")
                        .IsRequired()
                        .HasColumnName("close_at");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description");

                    b.Property<DateTime?>("EndAt")
                        .HasColumnName("end_at");

                    b.Property<string>("Image")
                        .HasColumnName("image_url");

                    b.Property<string>("Location")
                        .HasColumnName("location");

                    b.Property<DateTime?>("OpenAt")
                        .HasColumnName("open_at");

                    b.Property<DateTime?>("StartAt")
                        .IsRequired()
                        .HasColumnName("start_at");

                    b.Property<int>("Status")
                        .HasColumnName("status");

                    b.Property<int>("SubscribeNumber")
                        .HasColumnName("subscribe_number");

                    b.Property<string>("Title")
                        .HasColumnName("title")
                        .HasMaxLength(255);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

                    b.HasKey("Id");

                    b.ToTable("event");
                });

            modelBuilder.Entity("events_planner.Models.EventCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnName("category_id");

                    b.Property<int>("EventId")
                        .HasColumnName("event_id");

                    b.HasKey("CategoryId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("eventcategory");
                });

            modelBuilder.Entity("events_planner.Models.EventPromotion", b =>
                {
                    b.Property<int>("PromotionId")
                        .HasColumnName("promotion_id");

                    b.Property<int>("EventId")
                        .HasColumnName("event_id");

                    b.HasKey("PromotionId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("eventpromotion");
                });

            modelBuilder.Entity("events_planner.Models.EventUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.Property<int>("EventId")
                        .HasColumnName("event_id");

                    b.HasKey("UserId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("eventuser");
                });

            modelBuilder.Entity("events_planner.Models.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("price_id");

                    b.Property<int>("Amount")
                        .HasColumnName("price");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventId")
                        .HasColumnName("event_id");

                    b.Property<int>("RoleId")
                        .HasColumnName("role_id");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("RoleId");

                    b.ToTable("price");
                });

            modelBuilder.Entity("events_planner.Models.Promotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("promotion_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(40);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnName("year");

                    b.HasKey("Id");

                    b.ToTable("promotion");
                });

            modelBuilder.Entity("events_planner.Models.Recovery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("recovery_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Token")
                        .HasColumnName("token")
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

                    b.HasKey("Id");

                    b.ToTable("recovery");
                });

            modelBuilder.Entity("events_planner.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("role_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("role");
                });

            modelBuilder.Entity("events_planner.Models.Subscribe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("subcribe_id");

                    b.Property<int>("CategoryId")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("subscribe");
                });

            modelBuilder.Entity("events_planner.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnName("email")
                        .HasMaxLength(30);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password");

                    b.Property<int>("PhoneNumber")
                        .HasColumnName("phone_number");

                    b.Property<int>("PromotionId")
                        .HasColumnName("promotion_id");

                    b.Property<int>("RoleId")
                        .HasColumnName("role_id");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

                    b.Property<string>("Username")
                        .HasColumnName("username")
                        .HasMaxLength(20);

                    b.Property<int?>("recovery_id");

                    b.HasKey("Id");

                    b.HasIndex("PromotionId");

                    b.HasIndex("RoleId");

                    b.HasIndex("recovery_id")
                        .IsUnique();

                    b.HasIndex("Username", "Email")
                        .IsUnique();

                    b.ToTable("user");
                });

            modelBuilder.Entity("events_planner.Models.Booking", b =>
                {
                    b.HasOne("events_planner.Models.Event", "Event")
                        .WithMany("Bookings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("events_planner.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("events_planner.Models.Category", b =>
                {
                    b.HasOne("events_planner.Models.Category", "SubCategory")
                        .WithMany()
                        .HasForeignKey("SubCategoryId");
                });

            modelBuilder.Entity("events_planner.Models.EventCategory", b =>
                {
                    b.HasOne("events_planner.Models.Category", "Category")
                        .WithMany("EventCategory")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("events_planner.Models.Event", "Event")
                        .WithMany("EventCategory")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("events_planner.Models.EventPromotion", b =>
                {
                    b.HasOne("events_planner.Models.Event", "Event")
                        .WithMany("EventPromotion")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("events_planner.Models.Promotion", "Promotion")
                        .WithMany("EventPromotion")
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("events_planner.Models.EventUser", b =>
                {
                    b.HasOne("events_planner.Models.Event", "Event")
                        .WithMany("EventUser")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("events_planner.Models.User", "User")
                        .WithMany("EventUser")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("events_planner.Models.Price", b =>
                {
                    b.HasOne("events_planner.Models.Event", "Event")
                        .WithMany("Prices")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("events_planner.Models.Role", "Role")
                        .WithMany("Prices")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("events_planner.Models.Subscribe", b =>
                {
                    b.HasOne("events_planner.Models.Category", "Category")
                        .WithMany("Subscribers")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("events_planner.Models.User", "User")
                        .WithMany("SubscribeTo")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("events_planner.Models.User", b =>
                {
                    b.HasOne("events_planner.Models.Promotion", "Promotion")
                        .WithMany("Users")
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("events_planner.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("events_planner.Models.Recovery", "Recovery")
                        .WithOne("User")
                        .HasForeignKey("events_planner.Models.User", "recovery_id");
                });
#pragma warning restore 612, 618
        }
    }
}
