namespace IndFusion.UI.Models
{

    public class BreadcrumbItemWithIcon
    {
        public string Href { get; set; }
        public string Icon { get; set; }

        public BreadcrumbItemWithIcon(string href, string icon)
        {
            Href = href;
            Icon = icon;
        }
    }
}
