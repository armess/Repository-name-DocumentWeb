using Dapper;
using Microsoft.Data.SqlClient;
using SMDData.Models;

namespace SMDData.Services;

public class DocumentService
{
    private readonly string _connectionString;

    public DocumentService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Connection string not found");
    }

   public async Task<List<DocumentModel>> GetDocumentsAsync(int offset, int pageSize)
{
    using var connection = new SqlConnection(_connectionString);

    string sql = @"
        SELECT
            Id,
            DocumentNo,
            CustomerName,
            Remark,
            CreatedAt
        FROM Documents
        ORDER BY Id DESC
        OFFSET @Offset ROWS
        FETCH NEXT @PageSize ROWS ONLY";

    var result = await connection.QueryAsync<DocumentModel>(
        sql,
        new
        {
            Offset = offset,
            PageSize = pageSize
        });

    return result.ToList();
}
    public async Task AddDocumentAsync(DocumentModel doc)
{
    using var connection = new SqlConnection(_connectionString);

    string sql = @"
        INSERT INTO Documents
        (
            DocumentNo,
            CustomerName,
            Remark,
            CreatedAt
        )
        VALUES
        (
            @DocumentNo,
            @CustomerName,
            @Remark,
            @CreatedAt
        )";

    await connection.ExecuteAsync(sql, new
    {
        doc.DocumentNo,
        doc.CustomerName,
        doc.Remark,
        CreatedAt = DateTime.Now
    });
}
}