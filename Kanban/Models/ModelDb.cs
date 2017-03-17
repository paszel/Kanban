namespace Kanban.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDb : DbContext
    {
        public ModelDb()
            : base("name=ModelDb")
        {
        }

        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<TaskSet> TaskSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .HasMany(e => e.Person)
                .WithMany(e => e.Card)
                .Map(m => m.ToTable("CardPerson").MapLeftKey("CardId").MapRightKey("PersonId"));

            modelBuilder.Entity<TaskSet>()
                .HasMany(e => e.Card)
                .WithRequired(e => e.TaskSet)
                .WillCascadeOnDelete(false);
        }
    }
}
