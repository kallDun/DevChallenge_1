using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SC.DevChallenge.Api.Models
{
    public class Portfolio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string InstrumentOwner { get; set; }

        [Required]
        public string Instrument { get; set; }

        [Required]
        public TimeSlot Date { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        public double Price { get; set; }

    }
}
