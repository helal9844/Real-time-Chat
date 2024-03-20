using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_BL
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(16,MinimumLength =4)]
        public string Password { get; set; }
    }
}
