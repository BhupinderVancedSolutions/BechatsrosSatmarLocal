using System.Collections.Generic;

namespace Application.Common.Dtos
{
    public class ServerRowsRequest
    {
        public int StartRow { get; set; }
        public int EndRow { get; set; }
        public dynamic FilterModel { get; set; }
        public string OrderBy { set; get; }
        public string SearchText { set; get; }
        public List<SortModel> SortModel { set; get; }
        public ServerRowsRequest()
        {
            SortModel = new List<SortModel>();
        }
    }
}
