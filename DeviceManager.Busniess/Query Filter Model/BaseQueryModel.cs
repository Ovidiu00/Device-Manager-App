namespace DeviceManager.Busniess.Query_Filter_Model
{
    public class BaseQueryModel
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public BaseQueryModel()
        {
            Page = 1;
            ItemsPerPage = 25;
        }
    }
}
