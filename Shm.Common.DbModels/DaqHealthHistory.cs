using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels
{
    public class DaqHealthHistory
    {
        [Key]
        public int Id { get; set; }

        public int DaqId { get; set; }

        [ForeignKey("DaqId")]
        public virtual Daq Daq { get; set; } = default!;

        public DateTimeOffset? UnhealthyStartTime { get; set; }

        public DateTimeOffset? UnhealthyEndTime { get; set; }
 
        public bool NotifiedForFailure { get; set; }
    }
}