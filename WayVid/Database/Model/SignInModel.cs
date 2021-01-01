using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayVid.Database.Model
{
    public class SignInModel
    {
        [Required]
        [StringLength(15, MinimumLength = 4)]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(25, MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }
    }
}
