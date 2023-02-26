using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
    public virtual ICollection<Season> Seasons { get; set; }

    public Anime()
    {
        Tags = new List<Tag>();
    }
}

public class Tag
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [JsonIgnore]
    public ICollection<Anime> Animes { get; set; }
    
    public Tag()
    {
        Animes = new List<Anime>();
    }
}

public class Season
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public ICollection<Episodes> Episodes { get; set; }
}

public class Episodes
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Poster { get; set; }
    public string Desc { get; set; }
    public int Number { get; set; }
    public ICollection<Links> Links { get; set; }
}

public class Links
{
    [Key]
    public int Id { get; set; }
    public string SourceName { get; set; }
    public string url { get; set; }
    public bool isDub { get; set; }
}