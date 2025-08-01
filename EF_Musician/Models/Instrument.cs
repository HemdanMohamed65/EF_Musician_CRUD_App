using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_Musician.Models;

public partial class Instrument
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Instrument_Name { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Musical_Key { get; set; }

    [ForeignKey("Instrument_Name")]
    [InverseProperty("Instrument_Names")]
    public virtual ICollection<Musician> Musicians { get; set; } = new List<Musician>();
}
