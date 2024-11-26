using Microsoft.EntityFrameworkCore;

namespace WebApplicationTask.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}