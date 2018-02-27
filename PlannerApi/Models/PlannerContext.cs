using Microsoft.EntityFrameworkCore;

namespace events_planner.Models {
    public class PlannerContext : DbContext {
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Price> Price { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<Recovery> Recovery { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Subscribe> Subscribe { get; set; }
        public DbSet<User> User { get; set; }

        //JOINTS
        public DbSet<EventCategory> EventCategory { get; set; }
        public DbSet<EventPromotion> EventPromotion { get; set; }
        public DbSet<EventUser> EventUser { get; set; }
        
        
        /// <summary>
        /// This line is added to sync the database with migration file 
        /// </summary>
        public PlannerContext(DbContextOptions options) : base(options) {}
        public PlannerContext() {}

        /// <summary>
        /// Build the many to many relations
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //EVENT PROMOTION
            modelBuilder.Entity<EventPromotion>().HasKey(ep => new { ep.PromotionId, ep.EventId });

            modelBuilder.Entity<EventPromotion>()
                .HasOne<Event>(ep => ep.Event)
                .WithMany(e => e.EventPromotion)
                .HasForeignKey(ep => ep.EventId);


            modelBuilder.Entity<EventPromotion>()
                .HasOne<Promotion>(ep => ep.Promotion)
                .WithMany(p => p.EventPromotion)
                .HasForeignKey(ep => ep.PromotionId);

            //EVENT USER
            modelBuilder.Entity<EventUser>().HasKey(eu => new { eu.UserId, eu.EventId });

            modelBuilder.Entity<EventUser>()
                .HasOne<Event>(eu => eu.Event)
                .WithMany(e => e.EventUser)
                .HasForeignKey(eu => eu.EventId);


            modelBuilder.Entity<EventUser>()
                .HasOne<User>(eu => eu.User)
                .WithMany(u => u.EventUser)
                .HasForeignKey(eu => eu.UserId);

            //EVENT CATEGORY
            modelBuilder.Entity<EventCategory>().HasKey(ec => new { ec.CategoryId, ec.EventId });

            modelBuilder.Entity<EventCategory>()
                .HasOne<Event>(ec => ec.Event)
                .WithMany(e => e.EventCategory)
                .HasForeignKey(ec => ec.EventId);


            modelBuilder.Entity<EventCategory>()
                .HasOne<Category>(ec => ec.Category)
                .WithMany(c => c.EventCategory)
                .HasForeignKey(ec => ec.CategoryId);

            //==================================
            //          DEFAULT VALUE
            //==================================

            modelBuilder.Entity<User>((obj) =>
            {
                obj.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                obj.Property(p => p.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                obj.HasIndex(i => new {i.Username, i.Email}).IsUnique(true);
            });

            modelBuilder.Entity<Promotion>((obj) =>
            {
                obj.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                obj.Property(p => p.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Event>((obj) => 
            {
                obj.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                obj.Property(p => p.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}