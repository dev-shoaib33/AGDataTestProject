using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AGDataTestProject.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name must not exceed 50 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(255, ErrorMessage = "Address must not exceed 255 characters.")]
        public string? Address { get; set; }
    }

}
