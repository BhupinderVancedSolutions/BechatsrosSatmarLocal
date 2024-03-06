namespace Application.Common.Models.Request
{
    public class CommonRequest
    {
        public int StartRow { get; set; } = 1;
        public int EndRow { get; set; } = -1;
        public string FilterQuery { get; set; } = null;
        public string OrderBy { get; set; }=null;
        public string SearchText { get; set; }=null;

    }
}
