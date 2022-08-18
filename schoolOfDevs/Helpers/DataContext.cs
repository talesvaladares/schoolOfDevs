using Microsoft.EntityFrameworkCore;
using schoolOfDevs.Entities;
using schoolOfDevs.Enuns;

namespace schoolOfDevs.Helpers
{
    // herdo de dbContext que é um pacote
    public class DataContext : DbContext
    {   
        // configuração basica do dataContext com o DbContextOptions
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .Property(x => x.TypeUser)
                .HasConversion(
                    v => v.ToString(),
                    v => (TypeUser)Enum.Parse(typeof(TypeUser), v)
                );
        }


    }
}
