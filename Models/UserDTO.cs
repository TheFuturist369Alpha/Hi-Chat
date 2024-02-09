using System.ComponentModel.DataAnnotations;
namespace Models
{
    public class UserDTO
    {

        [Required(ErrorMessage="This field can't be blank.")]
        [StringLength(9,MinimumLength =3, ErrorMessage ="{0} must be at least {2} letters long and at most {1} letters long")]
        public string Name { get; set; }

    }
}