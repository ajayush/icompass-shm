using BridgeIntelligence.iCompass.Shm.Common.DbModels;
using Microsoft.EntityFrameworkCore;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore;

public class ShmDbContext : DbContext
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="options">Database context options</param>
    public ShmDbContext(DbContextOptions<ShmDbContext> options) : base(options)
    {
    }

    public virtual DbSet<TableState> TableStates { get; set; } = default!;

    public virtual DbSet<AlertRuleFailure> AlertRuleFailures { get; set; } = default!;
    
    public virtual DbSet<AlertRule> AlertRules { get; set; } = default!;

    public virtual DbSet<Bridge> Bridges { get; set; } = default!;
    
    public virtual DbSet<DaqFailure> DaqFailures { get; set; } = default!;

    public virtual DbSet<Daq> Daqs { get; set; } = default!;

    public virtual DbSet<AlertRuleEmailNotification> AlertRuleEmailNotifications { get; set; } = default!;

    public virtual DbSet<SensorFailure> SensorFailures { get; set; } = default!;
    
    public virtual DbSet<Sensor> Sensors { get; set; } = default!;

    public virtual DbSet<SensorOutput> SensorOutputs { get; set; } = default!;
    
    public virtual DbSet<SensorOutputSummary> SensorOutputSummary { get; set; } = default!;

    public virtual DbSet<Service> Services { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (string.IsNullOrWhiteSpace(tableName))
            {
                continue;
            }

            entity.SetTableName($"SHM_{tableName}");
        }

        modelBuilder.Entity<AlertRule>(entity => entity.HasOne(b => b.LastAlertRuleFailure).WithOne(b => b.AlertRule));
        modelBuilder.Entity<AlertRule>(entity => entity.HasMany(b => b.AlertRuleFailures));

        //modelBuilder.Entity<Daq>(entity => entity.HasOne(b => b.LastDaqFailure).WithOne(b => b.Daq));
        //modelBuilder.Entity<Daq>(entity => entity.HasMany(b => b.DaqFailures));

        //modelBuilder.Entity<Sensor>(entity => entity.HasOne(b => b.LastSensorFailure).WithOne(b => b.Sensor));
        //modelBuilder.Entity<Sensor>(entity => entity.HasMany(b => b.SensorFailures));

        base.OnModelCreating(modelBuilder);
    }
}