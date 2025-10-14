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
    public DbSet<MaterialStageEntity> MaterialStage { get; set; }
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
        modelBuilder.ApplyConfiguration(new MaterialStageConfiguration());


        modelBuilder.Entity<ProductTypeEntity>().HasData(
            new ProductTypeEntity[]
            {
                new ProductTypeEntity { Id = Guid.Parse("898d6acb-7ad6-4ca6-982c-d297af815a64"),Name = "ПНС"},
                new ProductTypeEntity { Id = Guid.Parse("c871d38b-21c7-4429-bdce-3c70c379ae4b"),Name = "КНС"},
                new ProductTypeEntity { Id = Guid.Parse("993548ea-fde7-4614-ac09-6c12bbfbfd9d"),Name = "ВНС в корпусе"},
                new ProductTypeEntity { Id = Guid.Parse("56938081-7b16-4bb4-a002-38bea806f87c"),Name = "ПНС в корпусе"},
                new ProductTypeEntity { Id = Guid.Parse("e02387d3-03c9-4acf-acca-fad34ac47038"),Name = "КНС в корпусе"},
                new ProductTypeEntity { Id = Guid.Parse("42a52c7e-1fcd-46bf-bf78-34a89f642cd3"),Name = "Водомерный узел"},
                new ProductTypeEntity { Id = Guid.Parse("d64ac138-b11f-44ef-b75a-48c338bc8157"),Name = "ЛОС"},
                new ProductTypeEntity { Id = Guid.Parse("e7d8c946-0a0e-4556-9cbb-2e1e3e6d1561"),Name = "ЕН"},
                new ProductTypeEntity { Id = Guid.Parse("11f6dadc-53d7-4ba3-90d4-8e9bfc0e4bca"),Name = "ЖУВ"},
                new ProductTypeEntity { Id = Guid.Parse("0cb6cef3-948e-426e-b655-a481772a84ca"),Name = "Ш/У"},
                new ProductTypeEntity { Id = Guid.Parse("def39874-75ce-4a53-8c1e-6a6bc88f92b9"),Name = "Автомойка"},
                new ProductTypeEntity { Id = Guid.Parse("aef1f8a9-9fa9-4696-abcf-228129da8ad1"),Name = "Блок-контейнер"},
                new ProductTypeEntity { Id = Guid.Parse("b19d093d-2deb-4b33-a69b-f1583aa27ab6"),Name = "ПНС в блок-контейнере"},
                new ProductTypeEntity { Id = Guid.Parse("96197298-d396-424a-8ed8-0f97bd038ba0"),Name = "ВНС в блок-контейнере"},
                new ProductTypeEntity { Id = Guid.Parse("8b12a5b4-5905-4eb9-8927-1b31eb9e960d"),Name = "КР"},
                new ProductTypeEntity { Id = Guid.Parse("3d747667-d70c-436c-8856-4f56a38a19ea"),Name = "КГН"},
                new ProductTypeEntity { Id = Guid.Parse("edf5834b-a1a0-42a7-8b55-84de08280fd7"),Name = "КП"},
                new ProductTypeEntity { Id = Guid.Parse("bf5381ed-4534-4ec5-87f7-8b28eaa22f9d"),Name = "КГН"},
                new ProductTypeEntity { Id = Guid.Parse("f102fb2a-b3af-462f-9989-9d1f3169d44b"),Name = "ВНС"}
            });
        modelBuilder.Entity<ProductSubTypeEntity>().HasData(
            new ProductSubTypeEntity[]
            { 
                new ProductSubTypeEntity { Id = Guid.Parse("41bfadaa-08ee-4a09-ab66-4c53d72c9959"),Name = "Подтип А", ProductTypeId = Guid.Parse("0cb6cef3-948e-426e-b655-a481772a84ca") },
                new ProductSubTypeEntity { Id = Guid.Parse("eaeda842-4989-4a5d-9755-0090f45db54f"),Name = "Подтип Б", ProductTypeId = Guid.Parse("0cb6cef3-948e-426e-b655-a481772a84ca") },
                new ProductSubTypeEntity { Id = Guid.Parse("0a283d26-f089-410e-8fdd-08135ba9b999"),Name = "Подтип В", ProductTypeId = Guid.Parse("0cb6cef3-948e-426e-b655-a481772a84ca") },
                new ProductSubTypeEntity { Id = Guid.Parse("3762494e-93a2-458b-8030-10397640cf4b"),Name = "Подтип А", ProductTypeId = Guid.Parse("11f6dadc-53d7-4ba3-90d4-8e9bfc0e4bca") },
                new ProductSubTypeEntity { Id = Guid.Parse("212d7ed0-547a-4611-a514-b47f5eb5e7ac"),Name = "Подтип Б", ProductTypeId = Guid.Parse("11f6dadc-53d7-4ba3-90d4-8e9bfc0e4bca") },
                new ProductSubTypeEntity { Id = Guid.Parse("af05f00d-070e-494c-a734-84cf1ae27c84"),Name = "Подтип А", ProductTypeId = Guid.Parse("3d747667-d70c-436c-8856-4f56a38a19ea") },
                new ProductSubTypeEntity { Id = Guid.Parse("c04349f0-de53-4c71-a700-a74acb81fae2"),Name = "Подтип Б", ProductTypeId = Guid.Parse("3d747667-d70c-436c-8856-4f56a38a19ea") },
                new ProductSubTypeEntity { Id = Guid.Parse("37f914f9-3463-4a46-919d-850ab0438593"),Name = "Подтип А", ProductTypeId = Guid.Parse("42a52c7e-1fcd-46bf-bf78-34a89f642cd3") },
                new ProductSubTypeEntity { Id = Guid.Parse("b574504f-1e86-42da-b016-48a92df3a22f"),Name = "Подтип Б", ProductTypeId = Guid.Parse("42a52c7e-1fcd-46bf-bf78-34a89f642cd3") },
                new ProductSubTypeEntity { Id = Guid.Parse("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"),Name = "Подтип А", ProductTypeId = Guid.Parse("56938081-7b16-4bb4-a002-38bea806f87c") },
                new ProductSubTypeEntity { Id = Guid.Parse("65310518-d75f-44f7-bc26-20ec63245b19"),Name = "Подтип Б", ProductTypeId = Guid.Parse("56938081-7b16-4bb4-a002-38bea806f87c") },
                new ProductSubTypeEntity { Id = Guid.Parse("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"),Name = "Подтип А", ProductTypeId = Guid.Parse("898d6acb-7ad6-4ca6-982c-d297af815a64") },
                new ProductSubTypeEntity { Id = Guid.Parse("dd235869-87fa-492a-9896-810b2d69261b"),Name = "Подтип Б", ProductTypeId = Guid.Parse("898d6acb-7ad6-4ca6-982c-d297af815a64") },
                new ProductSubTypeEntity { Id = Guid.Parse("b7ef5558-ba1c-4a3d-bd53-ebb961d189c4"),Name = "Подтип А", ProductTypeId = Guid.Parse("8b12a5b4-5905-4eb9-8927-1b31eb9e960d") },
                new ProductSubTypeEntity { Id = Guid.Parse("f8402906-63fa-4781-9240-2cf192133da0"),Name = "Подтип Б", ProductTypeId = Guid.Parse("8b12a5b4-5905-4eb9-8927-1b31eb9e960d") },
                new ProductSubTypeEntity { Id = Guid.Parse("3fbc3814-6bce-4046-a0b2-1bd279b1d295"),Name = "Подтип А", ProductTypeId = Guid.Parse("96197298-d396-424a-8ed8-0f97bd038ba0") },
                new ProductSubTypeEntity { Id = Guid.Parse("4e4c686b-1698-4718-af84-2ccd154b7475"),Name = "Подтип Б", ProductTypeId = Guid.Parse("96197298-d396-424a-8ed8-0f97bd038ba0") },
                new ProductSubTypeEntity { Id = Guid.Parse("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"),Name = "Подтип А", ProductTypeId = Guid.Parse("993548ea-fde7-4614-ac09-6c12bbfbfd9d") },
                new ProductSubTypeEntity { Id = Guid.Parse("fba229fd-dd29-4e80-bb79-09991987c4b4"),Name = "Подтип Б", ProductTypeId = Guid.Parse("993548ea-fde7-4614-ac09-6c12bbfbfd9d") },
                new ProductSubTypeEntity { Id = Guid.Parse("7865463d-4417-4adc-910a-9f14ba709c7b"),Name = "Подтип А", ProductTypeId = Guid.Parse("aef1f8a9-9fa9-4696-abcf-228129da8ad1") },
                new ProductSubTypeEntity { Id = Guid.Parse("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"),Name = "Подтип Б", ProductTypeId = Guid.Parse("aef1f8a9-9fa9-4696-abcf-228129da8ad1") },
                new ProductSubTypeEntity { Id = Guid.Parse("b853406e-78d8-4d2c-bfb6-1827da7e301b"),Name = "Подтип А", ProductTypeId = Guid.Parse("b19d093d-2deb-4b33-a69b-f1583aa27ab6") },
                new ProductSubTypeEntity { Id = Guid.Parse("dcb0b6e8-1a86-435a-b2a9-28dfd2deb699"),Name = "Подтип Б", ProductTypeId = Guid.Parse("b19d093d-2deb-4b33-a69b-f1583aa27ab6") },
                new ProductSubTypeEntity { Id = Guid.Parse("86a6d038-d6f0-41b5-b372-37cd66e0ff0a"),Name = "Подтип А", ProductTypeId = Guid.Parse("bf5381ed-4534-4ec5-87f7-8b28eaa22f9d") },
                new ProductSubTypeEntity { Id = Guid.Parse("b63d4462-6380-4f35-a746-56702ae9e1e8"),Name = "Подтип Б", ProductTypeId = Guid.Parse("bf5381ed-4534-4ec5-87f7-8b28eaa22f9d") },
                new ProductSubTypeEntity { Id = Guid.Parse("d8fe2c6f-d3ad-428e-8536-79d8f3298da8"),Name = "Подтип А", ProductTypeId = Guid.Parse("c871d38b-21c7-4429-bdce-3c70c379ae4b") },
                new ProductSubTypeEntity { Id = Guid.Parse("5daed2ae-ca02-4ea7-a7fa-f2ad68ba5f16"),Name = "Подтип Б", ProductTypeId = Guid.Parse("c871d38b-21c7-4429-bdce-3c70c379ae4b") },
                new ProductSubTypeEntity { Id = Guid.Parse("91b34819-6e7a-45dd-9b6f-3e35665fded3"),Name = "Подтип А", ProductTypeId = Guid.Parse("d64ac138-b11f-44ef-b75a-48c338bc8157") },
                new ProductSubTypeEntity { Id = Guid.Parse("1d0fca15-a4b0-4878-85b8-40ce384a7777"),Name = "Подтип Б", ProductTypeId = Guid.Parse("d64ac138-b11f-44ef-b75a-48c338bc8157") },
                new ProductSubTypeEntity { Id = Guid.Parse("57f44c4b-8bb9-41d6-9199-1bd8b52bef34"),Name = "Подтип А", ProductTypeId = Guid.Parse("def39874-75ce-4a53-8c1e-6a6bc88f92b9") },
                new ProductSubTypeEntity { Id = Guid.Parse("e86fec9c-55b8-4740-b396-1aea0e964e67"),Name = "Подтип Б", ProductTypeId = Guid.Parse("def39874-75ce-4a53-8c1e-6a6bc88f92b9") },
                new ProductSubTypeEntity { Id = Guid.Parse("2c891976-9006-4f0a-b493-52f1b8366946"),Name = "Подтип А", ProductTypeId = Guid.Parse("e02387d3-03c9-4acf-acca-fad34ac47038") },
                new ProductSubTypeEntity { Id = Guid.Parse("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"),Name = "Подтип Б", ProductTypeId = Guid.Parse("e02387d3-03c9-4acf-acca-fad34ac47038") },
                new ProductSubTypeEntity { Id = Guid.Parse("e7a65ae3-8290-499c-9226-fde4efc80914"),Name = "Подтип А", ProductTypeId = Guid.Parse("e7d8c946-0a0e-4556-9cbb-2e1e3e6d1561") },
                new ProductSubTypeEntity { Id = Guid.Parse("dee59352-bf5e-4901-8912-88a7afa0aea8"),Name = "Подтип Б", ProductTypeId = Guid.Parse("e7d8c946-0a0e-4556-9cbb-2e1e3e6d1561") },
                new ProductSubTypeEntity { Id = Guid.Parse("e0a9c3e6-3102-4892-a44b-8bec8bfcdf1c"),Name = "Подтип А", ProductTypeId = Guid.Parse("edf5834b-a1a0-42a7-8b55-84de08280fd7") },
                new ProductSubTypeEntity { Id = Guid.Parse("32fed513-1b3c-49cb-9e06-ddfaaa82a488"),Name = "Подтип Б", ProductTypeId = Guid.Parse("edf5834b-a1a0-42a7-8b55-84de08280fd7") },
                new ProductSubTypeEntity { Id = Guid.Parse("f2a48405-4728-4e50-b99e-54ee9f9235ac"),Name = "Подтип Б", ProductTypeId = Guid.Parse("f102fb2a-b3af-462f-9989-9d1f3169d44b") },
            });

        base.OnModelCreating(modelBuilder);
    }
}
