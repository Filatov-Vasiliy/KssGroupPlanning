using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;

namespace KssGroupPlanning.Configurations;

public class ProductSubTypeWorkingPeriodSampleConfiguration : IEntityTypeConfiguration<ProductSubTypeWorkingPeriodSampleEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductSubTypeWorkingPeriodSampleEntity> builder)
    {
        builder.HasKey(pstwps => pstwps.Id);
        builder.HasMany(pstwps => pstwps.WorkingPeriodStages).WithOne(wps => wps.ProductSubTypeWorkingPeriodSample);
        builder.HasOne(pstwps => pstwps.ProductSubType).WithMany(pst => pst.ProductSubTypeWorkingPeriodSamples).HasForeignKey(pstwps => pstwps.ProductSubTypeId);
        builder.HasMany(pstwps => pstwps.StageTypes).WithMany(st => st.ProductSubTypeWorkingPeriodSamples).UsingEntity<WorkingPeriodStageTypeRelationEntity>(); ;
        builder.HasMany(pstwps => pstwps.GroupMaterials).WithMany(gm => gm.ProductSubTypeWorkingPeriodSamples).UsingEntity<ProductSubTypeGroupMaterialRelationEntity>();
        builder.HasMany(cpstwps => cpstwps.ParentProductSubTypeWorkingPeriodSamples).WithMany(ppstwps => ppstwps.ChildProductSubTypeWorkingPeriodSamples).UsingEntity<WorkingPeriodRelationEntity>(
        r => r.HasOne<ProductSubTypeWorkingPeriodSampleEntity>(e => e.ChildProductSubTypeWorkingPeriodSample).WithMany(e => e.ParentWorkingPeriodRelations).HasForeignKey(e => e.ChildProductSubTypeWorkingPeriodSampleId),
            l => l.HasOne<ProductSubTypeWorkingPeriodSampleEntity>(e => e.ParentProductSubTypeWorkingPeriodSample).WithMany(e => e.ChildWorkingPeriodRelations).HasForeignKey(e => e.ParentProductSubTypeWorkingPeriodSampleId));
    }
}
