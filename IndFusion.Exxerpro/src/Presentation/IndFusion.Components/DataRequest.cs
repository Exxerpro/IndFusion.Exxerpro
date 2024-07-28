using System.ComponentModel;

namespace IndFusion.Components
{
    public class DataRequest
    {
        public int Page { get; set; } = 1;
        public string? SortColumn { get; set; }
        public PropertyDescriptor? SortColumnProperty { get; set; }
        public string? FilterColumn { get; set; }
        public PropertyDescriptor? FilterColumnProperty { get; set; }
        public string? Filter { get; set; }
        public bool Descending { get; set; } = false;
    }
}
