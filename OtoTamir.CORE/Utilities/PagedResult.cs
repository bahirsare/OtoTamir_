namespace OtoTamir.CORE.Utilities
{
    /// <summary>
    /// Sayfalama bilgilerini ve sonuçları taşıyan generic model.
    /// </summary>
    public class PagedResult<T>
    {
        public List<T> Results { get; set; } = new();
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        // View'larda güvenli erişim için yardımcı property'ler
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < PageCount;
        public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;
        public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
    }

    /// <summary>
    /// View'larda tip güvenli kullanım için type-erased wrapper.
    /// ViewBag yerine bu sınıfı kullanın.
    /// </summary>
    public class PagedResultMeta
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < PageCount;

        public static PagedResultMeta From<T>(PagedResult<T> source) => new()
        {
            CurrentPage = source.CurrentPage,
            PageCount = source.PageCount,
            PageSize = source.PageSize,
            RowCount = source.RowCount
        };
    }
}
