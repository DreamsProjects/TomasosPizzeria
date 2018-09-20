using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewTomasosPizzeria.Models
{
    public partial class Kund
    {
        public Kund()
        {
            Bestallning = new HashSet<Bestallning>();
        }

        public int KundId { get; set; }
        [Required]
        public string Namn { get; set; }
        [Required]
        public string Gatuadress { get; set; }
        [Required]
        public string Postnr { get; set; }
        [Required]
        public string Postort { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefon { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Ha ett kortare användarnamn")]
        public string AnvandarNamn { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Losenord { get; set; }

        public ICollection<Bestallning> Bestallning { get; set; }
    }
}
