namespace WebStore.Domain.ViewModels
{
    public class UserOrderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
