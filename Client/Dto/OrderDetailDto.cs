namespace WebAPI.Dto {
    public class OrderDetailDto {
        public int OrderId { get; set;}
        public DateTime OrderDate { get; set;}
        public int StaffId { get; set;}
        public string StaffName {  get; set;}


        public int OrderDetaildId {  get; set;}
        public int ProductId { get; set;}
        public string ProductName { get; set;}
        public int Quantity { get; set;}
        public decimal UnitPrice {  get; set;}
    }
}
