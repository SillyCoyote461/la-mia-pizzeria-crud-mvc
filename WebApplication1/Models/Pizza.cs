using System.ComponentModel.DataAnnotations;
using WebApplication1.Attributes;

namespace WebApplication1.Models
{
    public class Pizza
    {
        public Pizza()
        {

        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a pizza name.")]
        [StringLength(25)]
        public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please provide a pizza price.")]
        [GreaterThanZero]
		public float Price { get; set; } = 0.0f;

		[Required(ErrorMessage = "Please provide a pizza description.")]
        [StringLength(255)]

		public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please provide an image.")]
        public string Image { get; set; } = string.Empty;

    }
}
