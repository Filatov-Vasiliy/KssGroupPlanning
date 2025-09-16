using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using KssGroupPlanning.Configurations;
using KssGroupPlanning.Models.Help;
using KssGroupPlanning.Entities;


namespace KssGroupPlanning;

public class ProjectDbContext(DbContextOptions<ProjectDbContext> options) : DbContext(options)
{

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductTypeEntity> ProductTypes { get; set; }
    public DbSet<ProductSubTypeEntity> ProductSubTypes { get; set; }
    public DbSet<ProductSubTypeStagesSampleEntity> ProductSubTypeStagesSamples { get; set; }
    public DbSet<ProductSubTypeWorkingPeriodsSampleEntity> ProductSubTypeWorkingPeriodsSamples { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<FactoryEntity> Factories { get; set; }
    public DbSet<MaterialSampleEntity> MaterialSamples { get; set; }
    public DbSet<SrcOrderEntity> SrcOrders { get; set; }
    public DbSet<StageEntity> Stages { get; set; }
    public DbSet<StageTypeEntity> StageTypes { get; set; }
    public DbSet<WorkingPeriodEntity> WorkingPeriods { get; set; }
    public DbSet<WorkingPeriodStageEntity> WorkingPeriodStages { get; set; }
    public DbSet<WorkingPeriodsRelationsEntity> WorkingPeriodsRelations { get; set; }
    public DbSet<WorkingPeriodsStageTypesEntity> WorkingPeriodStageTypes { get; set; }
    public DbSet<BrigadeEntity> Brigades { get; set; }
    public DbSet<WorkingPeriodStageMaterialEntity> WorkingPeriodStageMaterials { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductSubTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductSubTypeStagesSampleConfiguration());
        modelBuilder.ApplyConfiguration(new ProductSubTypeWorkingPeriodsSampleConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new FactoryConfiguration());
        modelBuilder.ApplyConfiguration(new MaterialSampleConfiguration());
        modelBuilder.ApplyConfiguration(new SrcOrderConfiguration());
        modelBuilder.ApplyConfiguration(new StageConfiguration());
        modelBuilder.ApplyConfiguration(new StageTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WorkingPeriodsConfiguration());
        modelBuilder.ApplyConfiguration(new WorkingPeriodStageConfiguration());
        modelBuilder.ApplyConfiguration(new WorkingPeriodsRelationsConfiguration());
        modelBuilder.ApplyConfiguration(new WorkingPeriodsStageTypesConfiguration());
        modelBuilder.ApplyConfiguration(new BrigadeConfiguration());
        modelBuilder.ApplyConfiguration(new GroupMaterialConfiguration());
        modelBuilder.ApplyConfiguration(new WorkingPeriodStageMaterialConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
