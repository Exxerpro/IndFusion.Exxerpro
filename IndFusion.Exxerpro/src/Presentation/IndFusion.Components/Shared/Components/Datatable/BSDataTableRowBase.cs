using IndFusion.Components.Shared.Components.Content;

namespace IndFusion.Components.Shared.Components.Datatable
{
    public abstract class BSDataTableRowBase : BSTRBase
    {
        /// <summary>
        /// Hides the row.
        /// </summary>
        [Parameter]
        public bool IsHidden { get; set; } = false;
    }
}
