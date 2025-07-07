using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_Musician.Models;

public partial class Album
{
    [Key]
    public int Album_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Title { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Copyright_Date { get; set; }

    public int? Producer_ID { get; set; }

    [ForeignKey("Producer_ID")]
    [InverseProperty("Albums")]
    public virtual Musician Producer { get; set; }

    [InverseProperty("Album")]
    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
