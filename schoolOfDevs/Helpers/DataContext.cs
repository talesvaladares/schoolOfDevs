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

        // este metodo serve para converter um enum para um string
        // por exemplo para cada tipo do enum student ou teachar no banco ficaria salvo 0 para student ou 1 para teacher
        // com esta conversão o banco ficara salvo o proprio texto do enum teachar ou student
        // este enum é para definir o tipo de usuario
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .Property(x => x.TypeUser)
                .HasConversion(
                    v => v.ToString(),
                    v => (TypeUser)Enum.Parse(typeof(TypeUser), v)
                );
        }

        //metodo que sobrescreve o saveChangesAsync
        //para que de forma automatica ele passe o creted_at e o updated_at para o objeto
        //primeiro ele verifica se o objeto herda do baseEntity que criamos com o id e update e create_at
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToke = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                DateTime datetime = DateTime.Now;
                ((BaseEntity)entityEntry.Entity).UpdatedAt = datetime;

                if (entityEntry.State == EntityState.Added)
                    ((BaseEntity)entityEntry.Entity).CreatedAt = datetime;

            }
             return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToke);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Course> Courses { get; set; }


    }
}
