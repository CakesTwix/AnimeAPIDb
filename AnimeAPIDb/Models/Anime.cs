using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnimeAPIDb.Models;

public class Anime
{
    [Key]
    public int Id { get; set; }
    public string Codename { get; set; }
    [Required]
    public string NameUa { get; set; }
    [Required]
    public string NameEn { get; set; }
    [Required]
    public string Desc { get; set; }
    public string Type { get; set; }
    public string Poster { get; set; } = "https://static.displate.com/857x1200/displate/2022-04-15/7422bfe15b3ea7b5933dffd896e9c7f9_46003a1b7353dc7b5a02949bd074432a.jpg";
    public int AnilistId { get; set; }
    public int KitsuId { get; set; }
    public int MalId { get; set; }
    public int ImdbId { get; set; }
    public int Year { get; set; }
    
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();
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

public class Episode
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Poster { get; set; }
    public string Desc { get; set; }
    public int seasonNum { get; set; }
    public int episodeNum { get; set; }
    public ICollection<Links> Links { get; set; }
}

public class Links
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string SourceName { get; set; }
    [Required]
    public string url { get; set; }
    [Required]
    public bool isDub { get; set; }
}