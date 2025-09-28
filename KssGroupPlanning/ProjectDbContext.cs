using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using KssGroupPlanning.Configurations;
using KssGroupPlanning.Models.Help;
using KssGroupPlanning.Entities;


namespace KssGroupPlanning;

public class ProjectDbContext(DbContextOptions<ProjectDbContext> options) : DbContext(options)
{

    public DbSet<UserEntity> User { get; set; }
    public DbSet<ProductEntity> Product { get; set; }
    public DbSet<ProductTypeEntity> ProductType { get; set; }
    public DbSet<ProductSubTypeEntity> ProductSubType { get; set; }
    public DbSet<ProductSubTypeStageSampleEntity> ProductSubTypeStageSample { get; set; }
    public DbSet<ProductSubTypeWorkingPeriodSampleEntity> ProductSubTypeWorkingPeriodSample { get; set; }
    public DbSet<OrderEntity> Order { get; set; }
    public DbSet<FactoryEntity> Factory { get; set; }
    public DbSet<SrcOrderEntity> SrcOrder { get; set; }
    public DbSet<StageEntity> Stage { get; set; }
    public DbSet<StageTypeEntity> StageType { get; set; }
    public DbSet<WorkingPeriodEntity> WorkingPeriod { get; set; }
    public DbSet<WorkingPeriodStageEntity> WorkingPeriodStage { get; set; }
    public DbSet<WorkingPeriodRelationEntity> WorkingPeriodRelation { get; set; }
    public DbSet<WorkingPeriodStageTypeRelationEntity> WorkingPeriodStageTypeRelation { get; set; }
    public DbSet<BrigadeEntity> Brigade { get; set; }
    public DbSet<WorkingPeriodStageMaterialEntity> WorkingPeriodStageMaterial { get; set; }
    public DbSet<GroupMaterialEntity> GroupMaterial { get; set; }
    public DbSet<ProductSubTypeGroupMaterialRelationEntity> ProductSubTypeGroupMaterialRelation { get; set; }
    public DbSet<WorkingPeriodStageBrigadeRelationEntity> WorkingPeriodStageBrigadeRelation { get; set; }

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
        modelBuilder.ApplyConfiguration(new WorkingPeriodStageBrigadeRelationConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
