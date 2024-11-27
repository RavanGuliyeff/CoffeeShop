using System.ComponentModel.DataAnnotations;
using WebApplicationTask.Models.Base;

namespace WebApplicationTask.Models
{
    public class Category:BaseEntity
    {
        [Required,StringLength(10, ErrorMessage = "The length can be a maximum 10 characters."), MinLength(3, ErrorMessage = "The length can be a minimum 3 characters.")]

        public string Name { get; set; }

        public List<Product>? Products { get; set; }
    }

}
