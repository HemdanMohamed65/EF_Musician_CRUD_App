using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_Musician.Models;

public partial class Musician
{
    [Key]
    public int Musician_ID { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Street { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string City { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string phone_Number { get; set; }

    [InverseProperty("Producer")]
    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    [ForeignKey("Musician_ID")]
    [InverseProperty("Musicians")]
    public virtual ICollection<Instrument> Instrument_Names { get; set; } = new List<Instrument>();

    [ForeignKey("Musician_ID")]
    [InverseProperty("Musicians")]
    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
