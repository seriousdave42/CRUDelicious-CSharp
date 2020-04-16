using System.ComponentModel.DataAnnotations;
using System;

namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId {get; set;}
        [Required]
        public string Name {get; set;}
        [Required]
        public string Chef {get; set;}
        [Required]
        [Range(1,5)]
        public int Tastiness {get; set;}
        [Required]
        [Range(1,5000,ErrorMessage="Must have at least one calorie")]
        public int Calories {get; set;}
        [Required]
        public string Description {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdateAt {get; set;} = DateTime.Now;
    }
}