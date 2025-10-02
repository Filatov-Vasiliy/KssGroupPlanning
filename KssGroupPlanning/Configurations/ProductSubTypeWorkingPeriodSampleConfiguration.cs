using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        builder.HasMany(ppstwps => ppstwps.ChildProductSubTypeWorkingPeriodSamples).WithMany(cpstwps => cpstwps.ParentProductSubTypeWorkingPeriodSamples).UsingEntity<WorkingPeriodRelationEntity>(
                r => r.HasOne<ProductSubTypeWorkingPeriodSampleEntity>().WithMany().HasForeignKey(e => e.ChildProductSubTypeWorkingPeriodSampleId),
                l => l.HasOne<ProductSubTypeWorkingPeriodSampleEntity>().WithMany().HasForeignKey(e => e.ParentProductSubTypeWorkingPeriodSampleId)            
            );
    }
}
