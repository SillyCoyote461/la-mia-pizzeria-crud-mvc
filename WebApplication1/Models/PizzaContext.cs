using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace WebApplication1.Models
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pizzadb;Integrated Security=True;TrustServerCertificate=True");
        }
        public void Seed()
        {
            if (!Pizzas.Any())
            {
                var seed = new Pizza[]
                {
                    new Pizza
                    {
                        Image = "https://picsum.photos/200",
                        Name = "Margherita",
                        Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                        Price = 4.5f
                    },
                    new Pizza
                    {
                        Image = "https://picsum.photos/200",
                        Name = "Diavola",
                        Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                        Price = 5.5f
                    },
                    new Pizza
                    {
                        Image = "https://picsum.photos/200",
                        Name = "San Daniele",
                        Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                        Price = 6f
                    },
                    new Pizza
                    {
                        Image = "https://picsum.photos/200",
                        Name = "Quattro stagioni",
                        Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                        Price = 6f
                    }

                };

                Pizzas.AddRange(seed);
                SaveChanges();
            }

        }

    }
}