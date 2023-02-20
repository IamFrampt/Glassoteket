using System.ComponentModel.DataAnnotations;

namespace mikaeleriksson.Shared;

[Serializable]
public class Favorite
{
	[Key]
	public Guid Id { get; set; }
	[Required]
	public string? Name { get; set; }
	[Required]
	public string? Info { get; set; }
	[Required]
	public string? Img { get; set; }
	[Required]
	public string? Type { get; set; }
	[Required]
	public string? Company { get; set; }
	[Required]
	public string? Calorie { get; set; }
	[Required]
	public string? Kilojoules { get; set; }
	[Required]
	public string? Fat { get; set; }
	[Required]
	public string? Salt { get; set; }
	[Required]
	public string? Carbohydrates { get; set; }
	[Required]
	public string? Protein { get; set; }

}
