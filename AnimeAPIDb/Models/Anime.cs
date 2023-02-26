using System.ComponentModel.DataAnnotations;

namespace AnimeAPIDb.Models;

public class Anime
{
    [Key]
    public int Id { get; set; }
    public string Codename { get; set; }
    [Required]
    public string Name_ua { get; set; }
    public string Name_en { get; set; }
    [Required]
    public string Desc { get; set; }
    [Required]
    public string Type { get; set; }
    public string Poster { get; set; }
    public int Anilist_id { get; set; }
    public int Kitsu_id { get; set; }
    public int Mal_id { get; set; }
    public int Imdb_rating { get; set; }
    public int Year { get; set; }
    
    public virtual ICollection<Tag> Tags { get; set; }
}

public class Tag
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public ICollection<Anime> Animes { get; set; }
}