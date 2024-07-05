namespace TrybeHotel.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    // 1. Implemente as models da aplicação
    public class City {
        public int CityId {get; set;}
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name must have a maximum of 60 characters")]
        public string? Name {get; set;}

        [Required(ErrorMessage = "State is required")]
        [StringLength(3, ErrorMessage = "State must have a maximum of 3 characters")]
        public string? State {get; set;}

        public virtual List<Hotel>? Hotels {get; set;}
    }
}