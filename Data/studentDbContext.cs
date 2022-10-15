using Microsoft.EntityFrameworkCore;
using TE_CodeFirst_Dotnet.Models;

namespace TE_CodeFirst_Dotnet.Data
{
    public class studentDbContext:DbContext
    {
        public studentDbContext(DbContextOptions<studentDbContext>options):base(options)
        {

        }

        public DbSet<students> student { get; set; }
    }
}
