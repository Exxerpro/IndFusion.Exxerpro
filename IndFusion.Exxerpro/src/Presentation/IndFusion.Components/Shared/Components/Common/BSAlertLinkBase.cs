namespace IndFusion.Components.Shared.Components.Common
{
    public abstract class BSAlertLinkBase : BlazorStrapBase
    {
        /// <summary>
        /// Url to link to.
        /// </summary>
        [Parameter] public string? Url { get; set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
