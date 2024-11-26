using WebApplicationTask.Models.Base;

namespace WebApplicationTask.Models
{
    public class Category:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }

}
