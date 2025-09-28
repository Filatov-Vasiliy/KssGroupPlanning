using System.Xml.Linq;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Entities
{
    public class Brigade
    {
        private Brigade(Guid id, Guid stageTypeId, Guid factoryId, int countEmployee)
        {
            Id = id;
            StageTypeId = stageTypeId;
            FactoryId = factoryId;
            CountEmployee = countEmployee;
        }

        public Guid Id { get; set; }
        public StageType? StageType { get; set; }
        public Guid StageTypeId { get; set; }
        public Factory? Factory { get; set; }
        public Guid FactoryId { get; set; }
        public int CountEmployee { get; set; }
        public List<WorkingPeriodStage>? WorkingPeriodStages { get; set; } = new List<WorkingPeriodStage>();
        public static Brigade Create(Guid id, Guid stageTypeId, Guid factoryId, int countEmployee)
        { 
            return new Brigade(id,stageTypeId,factoryId,countEmployee);
        }
    }
}
