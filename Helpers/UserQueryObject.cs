namespace api.Helpers;

public class UserQueryObject
{
    public string? Name { get; set; } = null;
    public DateTime? Birthday { get; set; }
    public string? Email { get; set; } = null;
    public string? SortBy { get; set; } = null;
    public bool IsDecsending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set;} = 10;
}