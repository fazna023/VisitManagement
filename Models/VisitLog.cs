using System.ComponentModel.DataAnnotations;

namespace VisitorManagement.Models
{
    public class VisitLog
    {
        public int Id { get; set; }

        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; }

        public DateTime VisitDate { get; set; }

        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        public int HostEmployeeId { get; set; }
        public Employee HostEmployee { get; set; }

        public string Purpose { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; } = " ";

        public DateTime? ScheduledTime { get; set; }
    }
}
