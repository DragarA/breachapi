using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BreachApi.Models
{
    public class BreachedEmailApiModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Not a valid e-mail.")]
        public string Email { get; set; }
    }
}
