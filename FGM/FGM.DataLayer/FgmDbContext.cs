using FGM.DataLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FGM.Data;

public class FgmDbContext(DbContextOptions<FgmDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Survey> Surveys { get; set; }
}