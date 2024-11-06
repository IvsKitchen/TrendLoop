using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TrendLoop.Data
{
    public class TrendLoopDbContext : IdentityDbContext
    {
        public TrendLoopDbContext(DbContextOptions<TrendLoopDbContext> options) : base(options)
        {
        }

        // TODO add DbSets
    }

}
