
namespace KssGroupPlanning.DTO.Orders;

public class OrderDetailed
{
    public Guid Id { get; set; }
    public string Number { get; set; }
    public string Manager{ get; set; }
    public string Contragent{ get; set; }
    public decimal PaymentAmount{ get; set; }
    public decimal PaymentCurrent{ get; set; }
    public string Status{ get; set; }
    public DateTime CreateTime{ get; set; }
    public DateTime UpdateTime{ get; set; }
    public List<ProductDetailed?> Product { get; set; }
    }


public class ProductDetailed{
    public Guid Id { get; set; }
    public string Number { get; set; }
    public ProductSubTypeDetailed ProductSubType { get; set; }
    public Factory Factory { get; set; }
    public Guid ParentProductId { get; set; }
    public string Status { get; set; }
    public List<Stage> Stages { get; set; }
    public List<WorkingPeriod> WorkingPeriods { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
}

public class Factory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class ProductSubTypeDetailed{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ProductType ProductType { get; set; }
}

public class ProductType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class Stage
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
}

public class WorkingPeriod
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public List<WorkingPeriodStage> WorkingPeriodStages { get; set; }
}

public class WorkingPeriodStage
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public TimeOnly? Recycling { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
}