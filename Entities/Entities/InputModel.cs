using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class InputModel
    {
        #region Propriedades

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        #endregion
    }
}