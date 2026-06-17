namespace SMDData.Helpers;

public class PaginationState
{
    public int CurrentPage { get; private set; } = 1;

    public int PageSize { get; set; } = 50;

    public int Offset => (CurrentPage - 1) * PageSize;

    public void Next()
    {
        CurrentPage++;
    }

    public void Prev()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
        }
    }

    public void Reset()
    {
        CurrentPage = 1;
    }
}