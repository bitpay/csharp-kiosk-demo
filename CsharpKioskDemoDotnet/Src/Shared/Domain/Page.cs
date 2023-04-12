namespace CsharpKioskDemoDotnet.Shared.Domain;

public class Page<T>
{
    public List<T> Content { get; }
    public int CurrentPageNumber { get; }
    public int MaxElementsPerPage { get; }
    public long TotalElements { get; }
    public int TotalPages { get; }

    public Page(
        List<T> content,
        int currentPageNumber,
        int maxElementsPerPage,
        long totalElements,
        int totalPages
    )
    {
        Content = content;
        CurrentPageNumber = currentPageNumber;
        MaxElementsPerPage = maxElementsPerPage;
        TotalElements = totalElements;
        TotalPages = totalPages;
    }

    public Page<TU> MapElementsToNewType<TU>(IConverter<TU, T> converter)
    {
        return new Page<TU>(
            content: Content.Select(converter.Execute).ToList(),
            currentPageNumber: CurrentPageNumber,
            maxElementsPerPage: MaxElementsPerPage,
            totalElements: TotalElements,
            totalPages: TotalPages
        );
    }

    public int GetFirstPageItemIndex()
    {
        return (MaxElementsPerPage * CurrentPageNumber) + 1;
    }
    
    public int GetLastPageItemIndex()
    {
        return (MaxElementsPerPage * CurrentPageNumber) + Content.Count;
    }
    
    public int GetPreviousPageNumber()
    {
        return Math.Max(CurrentPageNumber, 1);
    }
    
    public int GetNexPageNumber()
    {
        return Math.Min(CurrentPageNumber + 2, TotalPages);
    }
}