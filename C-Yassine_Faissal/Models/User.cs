using System.ComponentModel.DataAnnotations;

namespace C_Yassine_Faissal.Models
{
    public class User
    {
        // Properties voor het gebruikersmodel.
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        // We gebruiken één enkele User-klasse met een UserRole property om onderscheid te maken tussen
        // Admin, Employee en Guest in plaats van afzonderlijke klassen voor elk type gebruiker te hebben
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password cannot be longer than 100 characters.")]
        public string Password { get; set; }

        public bool IsGuest { get; set; }
        // UserRole property om de rol van de gebruiker te bepalen (Admin, Employee of Guest)
        public UserRole Role { get; set; }
    }
}
