using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_Musician.Models;

public partial class Song
{
    [Key]
    public int Song_ID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Title { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Author { get; set; }

    public int? Album_ID { get; set; }

    [ForeignKey("Album_ID")]
    [InverseProperty("Songs")]
    public virtual Album Album { get; set; }

    [ForeignKey("Song_ID")]
    [InverseProperty("Songs")]
    public virtual ICollection<Musician> Musicians { get; set; } = new List<Musician>();
}
