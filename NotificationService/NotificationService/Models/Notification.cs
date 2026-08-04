namespace NotificationService.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int CustomerId { get; set; }
        public int? OrderId { get; set; }
        public required string  NotificationType { get; set; }
        public string? Message { get; set; }
        public string? EmailStatus { get; set; }
        public string? SMSStatus { get; set; }
        public DateTime? SentDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
