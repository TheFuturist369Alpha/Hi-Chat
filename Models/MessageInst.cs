using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MessageInst
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public string From { get; set; }    
        public string? To { get; set; }
    }
}
