namespace DTO.Response
{
    public abstract class CommonResponse
    {
        public int PageIndex { get; init; }
        public int TotalPages { get; init; }
        public int TotalCount { get; init; }
        public bool HasPreviousPage { get; init; }
        public bool HasNextPage { get; init; }
    }
}
