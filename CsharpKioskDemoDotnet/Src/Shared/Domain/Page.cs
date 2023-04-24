// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Shared.Domain;

public class Page<T>
{
    public IReadOnlyList<T> Content { get; }
    public int CurrentPageNumber { get; }
    public long TotalElements { get; }

    public int LastPageItemIndex
    {
        get
        {
            return (_maxElementsPerPage * CurrentPageNumber) + Content.Count;
        }
    }

    public int FirstPageItemIndex
    {
        get
        {
            return (_maxElementsPerPage * CurrentPageNumber) + 1;
        }
    }

    public int PreviousPageNumber
    {
        get
        {
            return Math.Max(CurrentPageNumber, 1);
        }
    }

    public int NexPageNumber
    {
        get
        {
            return Math.Min(CurrentPageNumber + 2, _totalPages);
        }
    }

    private readonly int _totalPages;
    private readonly int _maxElementsPerPage;

    public Page(
        IReadOnlyList<T> content,
        int currentPageNumber,
        int maxElementsPerPage,
        long totalElements,
        int totalPages
    )
    {
        Content = content;
        CurrentPageNumber = currentPageNumber;
        _maxElementsPerPage = maxElementsPerPage;
        TotalElements = totalElements;
        _totalPages = totalPages;
    }

    public Page<TU> MapElementsToNewType<TU>(IConverter<TU, T> converter)
    {
        ArgumentNullException.ThrowIfNull(converter);
        return new Page<TU>(
            content: Content.Select(converter.Execute).ToList(),
            currentPageNumber: CurrentPageNumber,
            maxElementsPerPage: _maxElementsPerPage,
            totalElements: TotalElements,
            totalPages: _totalPages
        );
    }
}