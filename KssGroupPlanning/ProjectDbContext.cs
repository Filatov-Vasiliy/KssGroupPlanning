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
    public DbSet<ProductSubTypeStageSampleEntity> ProductSubTypeStageSamples { get; set; }
    public DbSet<ProductSubTypeWorkingPeriodSampleEntity> ProductSubTypeWorkingPeriodSamples { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<FactoryEntity> Factories { get; set; }
    public DbSet<MaterialSampleEntity> MaterialSamples { get; set; }
    public DbSet<SrcOrderEntity> SrcOrders { get; set; }
    public DbSet<StageEntity> Stages { get; set; }
    public DbSet<StageTypeEntity> StageTypes { get; set; }
    public DbSet<WorkingPeriodEntity> WorkingPeriods { get; set; }
    public DbSet<WorkingPeriodStageEntity> WorkingPeriodStages { get; set; }
    public DbSet<WorkingPeriodRelationEntity> WorkingPeriodRelations { get; set; }
    public DbSet<WorkingPeriodStageTypeRelationEntity> WorkingPeriodStageTypeRelations { get; set; }
    public DbSet<BrigadeEntity> Brigades { get; set; }
    public DbSet<WorkingPeriodStageMaterialEntity> WorkingPeriodStageMaterials { get; set; }
    public DbSet<GroupMaterialEntity> GroupMaterials { get; set; }
    public DbSet<ProductSubTypeGroupMaterialRelationEntity> ProductSubTypeGroupMaterialRelations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductSubTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductSubTypeStageSampleConfiguration());
        modelBuilder.ApplyConfiguration(new ProductSubTypeWorkingPeriodSampleConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new FactoryConfiguration());
        modelBuilder.ApplyConfiguration(new MaterialSampleConfiguration());
        modelBuilder.ApplyConfiguration(new SrcOrderConfiguration());
        modelBuilder.ApplyConfiguration(new StageConfiguration());
        modelBuilder.ApplyConfiguration(new StageTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WorkingPeriodConfiguration());
        modelBuilder.ApplyConfiguration(new WorkingPeriodStageConfiguration());
        modelBuilder.ApplyConfiguration(new WorkingPeriodRelationConfiguration());
        modelBuilder.ApplyConfiguration(new WorkingPeriodStageTypeRelationConfiguration());
        modelBuilder.ApplyConfiguration(new BrigadeConfiguration());
        modelBuilder.ApplyConfiguration(new GroupMaterialConfiguration());
        modelBuilder.ApplyConfiguration(new ProductSubTypeGroupMaterialRelationConfiguration());
        modelBuilder.ApplyConfiguration(new WorkingPeriodStageMaterialConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
