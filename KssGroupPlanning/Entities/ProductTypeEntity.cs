using System.Net.Quic;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Globalization;
using System.Linq;

namespace KssGroupPlanning.Entities;

public class ProductTypeEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ProductSubTypeEntity> ProductSubTypeEntities { get; set; } = [];
}
