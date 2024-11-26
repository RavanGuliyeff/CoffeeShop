using WebApplicationTask.Models.Base;

namespace WebApplicationTask.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string? ImgUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
