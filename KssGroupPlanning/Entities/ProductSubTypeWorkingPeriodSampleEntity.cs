

namespace KssGroupPlanning.Entities;

public class ProductSubTypeWorkingPeriodSampleEntity
{
    public Guid Id { get; set; }
    public Guid ProductSubTypeId { get; set; }
    public ProductSubTypeEntity? ProductSubType { get; set; } = null;
    public int RowNumber { get; set; }
    public string WorkingPeriodName { get; set; }
    public string StandartTime { get; set; } // string??
    public int StandartEmployee { get; set; }
    public List<WorkingPeriodStageEntity> WorkingPeriodStages { get; set;  } = new List<WorkingPeriodStageEntity>();
    public List<StageTypeEntity> StageTypes { get; set; } = new List<StageTypeEntity>();
    public List<GroupMaterialEntity> GroupMaterials { get; set; } = new List<GroupMaterialEntity>();
    public List<WorkingPeriodRelationEntity> ChildWorkingPeriodRelations { get; set; } = new List<WorkingPeriodRelationEntity>();
    public List<WorkingPeriodRelationEntity> ParentWorkingPeriodRelations { get; set; } = new List<WorkingPeriodRelationEntity>();

    public List<ProductSubTypeWorkingPeriodSampleEntity> ChildProductSubTypeWorkingPeriodSamples { get; set; } = new List<ProductSubTypeWorkingPeriodSampleEntity>();
    public List<ProductSubTypeWorkingPeriodSampleEntity> ParentProductSubTypeWorkingPeriodSamples { get; set; } = new List<ProductSubTypeWorkingPeriodSampleEntity>();
    public List<WorkingPeriodStageTypeRelationEntity> WorkingPeriodStageTypeRelations { get; set; } = new List<WorkingPeriodStageTypeRelationEntity>();
    public List<ProductSubTypeGroupMaterialRelationEntity> ProductSubTypeGroupMaterialRelations { get; set; } = new List<ProductSubTypeGroupMaterialRelationEntity>();

}
