using DemoVerify.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoVerify.Database
{
    public class VerifyDataContext : DbContext
    {
        public VerifyDataContext(DbContextOptions<VerifyDataContext> options) : base(options)
        {
        }

        public DbSet<Verifier> Verifiers { get; set; }
    }
}