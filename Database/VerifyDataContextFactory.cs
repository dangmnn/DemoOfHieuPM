using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DemoVerify.Database
{
    public class VerifyDataContextFactory : IDesignTimeDbContextFactory<VerifyDataContext>
    {
        public VerifyDataContext CreateDbContext(string[] args)
        {
            var optionsbuilder = new DbContextOptionsBuilder<VerifyDataContext>();
            optionsbuilder.UseSqlServer("Server=localhost;Database=demoverify;user id=sa;password=1");
            return new VerifyDataContext(optionsbuilder.Options);
        }
    }
}