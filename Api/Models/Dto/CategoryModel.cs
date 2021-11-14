using Data.Enums;

namespace Api.Models.Dto;

public class CategoryModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryType Type { get; set; }
}
