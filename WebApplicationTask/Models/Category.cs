using System.ComponentModel.DataAnnotations;
using WebApplicationTask.Models.Base;

namespace WebApplicationTask.Models
{
    public class Category:BaseEntity
    {
        [Required,StringLength(20, ErrorMessage = "The length can be a maximum 20 characters."), MinLength(3, ErrorMessage = "The length can be a minimum 3 characters.")]

        public string Name { get; set; }

        public List<Product>? Products { get; set; }
    }

}
