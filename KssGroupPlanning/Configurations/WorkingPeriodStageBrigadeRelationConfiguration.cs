using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Configurations;

public class WorkingPeriodStageBrigadeRelationConfiguration : IEntityTypeConfiguration<WorkingPeriodStageBrigadeRelationEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkingPeriodStageBrigadeRelationEntity> builder)
    {
        builder.HasKey(wpsbr => wpsbr.Id);
        builder.HasOne(wpsbr => wpsbr.Brigade).WithMany(st => st.WorkingPeriodStageBrigadeRelations).HasForeignKey(wpst => wpst.BrigadeId);
        builder.HasOne(wpsbr => wpsbr.WorkingPeriodStage).WithMany(st => st.WorkingPeriodStageBrigadeRelations).HasForeignKey(wpst => wpst.WorkingPeriodStageId);
    }
}
