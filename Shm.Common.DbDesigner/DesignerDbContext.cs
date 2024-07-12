using BridgeIntelligence.iCompass.Shm.Common.DbCore;
using Microsoft.EntityFrameworkCore;

namespace BridgeIntelligence.iCompass.Shm.Common.DbDesigner;

public class DesignerDbContext : ShmDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString, builder =>
        {
            builder.CommandTimeout(300);
        });
    }

    public DesignerDbContext() : base(new DbContextOptionsBuilder<ShmDbContext>().UseSqlServer(ConnectionString).Options)
    {
    }

    private const string ConnectionString =
        "Server=J-Desktop;Initial Catalog=icompass_shm_3;MultipleActiveResultSets=True;Connection Timeout=30;Integrated Security=True;TrustServerCertificate=True;";
}