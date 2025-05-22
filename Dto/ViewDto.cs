namespace VisitorManagement.Dto
{
    public class ViewDto
    {
        public int VisitLogId { get; set; }
        public int VisitorId { get; set; }
        public string VisitorName { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public int HostEmployeeId { get; set; }
        public string HostEmployeeName { get; set; }
        public string Purpose { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}
