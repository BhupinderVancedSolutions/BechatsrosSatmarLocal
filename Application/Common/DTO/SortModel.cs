namespace Application.Common.Dtos
{
    public class SortModel
    {
        public string ColId { set; get; }
        public string Sort { set; get; }

        public SortModel() { }

        public SortModel(string colId, string sort)
        {
            this.ColId = colId;
            this.Sort = sort;
        }
    }
}
