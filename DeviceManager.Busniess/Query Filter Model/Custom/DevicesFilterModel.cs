namespace DeviceManager.Busniess.Query_Filter_Model.Custom
{
    public class DevicesFilterModel : BaseQueryModel
    {
        public string Value { get; set; }
        public string Selector { get; set; }
        public DevicesFilterModel()
        {
            this.ItemsPerPage = 10;
        }
    }
}
