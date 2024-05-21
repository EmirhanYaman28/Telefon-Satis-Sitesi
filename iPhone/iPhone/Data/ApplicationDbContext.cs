	using iPhone.Models;
using Microsoft.EntityFrameworkCore;

namespace iPhone.Data
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			
		}
		public DbSet<Telefon> telefons { get; set; }
		public DbSet<User> users { get; set; }
    }
}
