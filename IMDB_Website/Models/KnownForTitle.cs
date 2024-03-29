﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IMDB_Website.Models;

public partial class KnownForTitle
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("nconst")]
    [StringLength(10)]
    [Unicode(false)]
    public string Nconst { get; set; }

    [Required]
    [Column("tconst")]
    [StringLength(10)]
    [Unicode(false)]
    public string Tconst { get; set; }

    [ForeignKey("Nconst")]
    [InverseProperty("KnownForTitles")]
    public virtual Name NconstNavigation { get; set; }

    [ForeignKey("Tconst")]
    [InverseProperty("KnownForTitles")]
    public virtual Title TconstNavigation { get; set; }
}