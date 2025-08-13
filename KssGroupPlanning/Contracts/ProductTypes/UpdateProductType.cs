using System.ComponentModel.DataAnnotations;

namespace KssGroupPlanning.Contracts.ProductTypes;

public record UpdateProductType(
    [Required] Guid productTypeId,
    [Required] string Name);