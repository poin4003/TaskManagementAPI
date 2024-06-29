namespace api.Helpers;

public class TodoQueryObject
{
    public string? Name { get; set; } = null;
    public string? Description { get; set; } = null;
    public string? SortBy { get; set; } = null;
    public bool IsDecsending { get; set; } = false;
}