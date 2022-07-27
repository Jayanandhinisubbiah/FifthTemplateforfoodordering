namespace FifthTemplateforfoodordering.Models
{
    public class ReportViewModel
    {
        public Food food { get; set; }
        public User users { get; set; }
        public OrderMaster orderMaster { get; set; }
        public OrderDetails orderDetails { get; set; }
    }
}
