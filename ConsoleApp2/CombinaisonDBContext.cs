namespace ConsoleApp1
{
    using System.Data.Entity;

    /// <summary>
    /// The person db context.
    /// </summary>
    public class CombinaisonDbContext : DbContext
    {

        public CombinaisonDbContext() : base("name=CombinsaisonContext")
        {

        }
        public DbSet<Combinaison> Combinaisons { get; set; }
        public DbSet<Championnat> Championnat { get; set; }
    }

}