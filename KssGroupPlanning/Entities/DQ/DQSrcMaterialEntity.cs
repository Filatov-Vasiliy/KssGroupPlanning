using CsvHelper.Configuration.Attributes;
using KssGroupPlanning.Entities.Src;

namespace KssGroupPlanning.Entities.DQ;

public class DQSrcMaterialEntity
{
    public Guid Id { get; set; }
    public string? ProductOrderName { get; set; }

    public DateOnly? ProductOrderDate { get; set; }
    public string? MaterialName { get; set; }
    public string? MaterialGroup { get; set; }
    public decimal? Qty { get; set; }
    public decimal? CurrentQty { get; set; }
    public DateOnly? PostedDate { get; set; }

    public DateOnly? ForAdmissionDate { get; set; }

    public DateOnly? CreateDate { get; set; }
    public string? ProductOrderNameChild { get; set; }
    public DateOnly? ProductOrderDateChild { get; set; }
    public bool LoadStatus { get; set; }
    public string? Reason { get; set; }
    public Guid? GroupMaterialId { get; set; } = Guid.Empty;
    public Guid? ProductId { get; set; } = Guid.Empty;
    public DateTime? CreateTime { get; set; } = DateTime.Now;
    public DQSrcMaterialEntity SrcToDQ(SrcMaterialEntity material, string reason, bool loadStatus, Guid? groupMaterialId, Guid? productId)
    {
        DQSrcMaterialEntity newEntity = new DQSrcMaterialEntity();
        newEntity.Id = material.Id;
        newEntity.ProductOrderName = material.ProductOrderName;
        newEntity.ProductOrderDate = material.ProductOrderDate;
        newEntity.MaterialName = material.MaterialName;
        newEntity.MaterialGroup = material.MaterialGroup;
        newEntity.Qty = material.Qty;
        newEntity.CurrentQty = material.CurrentQty;
        newEntity.PostedDate = material.PostedDate;
        newEntity.ForAdmissionDate = material.ForAdmissionDate;
        newEntity.CreateDate = material.CreateDate;
        newEntity.ProductOrderNameChild = material.ProductOrderNameChild;
        newEntity.ProductOrderDateChild = material.ProductOrderDateChild;
        newEntity.LoadStatus = loadStatus;
        newEntity.Reason = reason;
        newEntity.GroupMaterialId = groupMaterialId;
        newEntity.ProductId = productId;
        return newEntity;
    }
}
