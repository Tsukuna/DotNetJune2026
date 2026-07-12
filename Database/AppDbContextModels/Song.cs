using System;
using System.Collections.Generic;

namespace Database.AppDbContextModels;

public partial class Song
{
    public int SongId { get; set; }

    public string? SongName { get; set; }

    public string? SongType { get; set; }

    public string? ArtistName { get; set; }

    public string? Genre { get; set; }
}
