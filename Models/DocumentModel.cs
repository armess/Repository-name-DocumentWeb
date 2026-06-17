namespace SMDData.Models;

public class DocumentModel
{
    public int Id { get; set; }
    public string DocumentNo { get; set; } = "";
    public string? CustomerName { get; set; }
    public string? Remark { get; set; }
    public DateTime CreatedAt { get; set; }
}