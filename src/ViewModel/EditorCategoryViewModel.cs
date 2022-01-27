using Blog.Models;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must contain between 3 to 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Slug is required")]
        public string Slug { get; set; }

        public static implicit operator Category(EditorCategoryViewModel model)
        {
            return new Category
            {
                Name = model.Name,
                Slug = model.Slug.ToLower()
            };
        }
    }
}
