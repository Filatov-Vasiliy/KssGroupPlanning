using System.ComponentModel.DataAnnotations;

namespace KssGroupPlanning.Contracts.ProductTypes;

public record CreateProductType(
    [Required] string Name);