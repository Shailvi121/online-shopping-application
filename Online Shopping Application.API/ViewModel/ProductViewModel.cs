namespace Online_Shopping_Application.API.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public int? CategoryId { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public bool? IsActive { get; set; }
    }
}
