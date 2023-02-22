using System.ComponentModel.DataAnnotations;

namespace AnimeAPIDb.Models;

public class Anime
{
    [Key]
    public int id { get; set; }
    public string codename { get; set; }
    [Required]
    public string name_ua { get; set; }
    public string name_en { get; set; }
    [Required]
    public string desc { get; set; }
    [Required]
    public string type { get; set; }
    public string poster { get; set; }
    public int anilist_id { get; set; }
    public int kitsu_id { get; set; }
    public int mal_id { get; set; }
    public int imdb_rating { get; set; }
    public int year { get; set; }
    // public List<String> genres { get; set; }
}