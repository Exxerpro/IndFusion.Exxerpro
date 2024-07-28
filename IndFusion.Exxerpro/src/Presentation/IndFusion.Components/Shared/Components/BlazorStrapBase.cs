﻿using IndFusion.Components.Extensions;
using IndFusion.Components.Interfaces;
using IndFusion.Components.Service;
using IndFusion.Components.Shared.Enums;
using IndFusion.Components.Utilities;

namespace IndFusion.Components.Shared.Components
{
    public abstract class BlazorStrapBase : ComponentBase, IBlazorStrapBase
    {
        protected BlazorStrapCore BlazorStrapService => (BlazorStrapCore)BlazorStrapSrc;
        // ReSharper disable once NullableWarningSuppressionIsUsed
        [Inject] public IBlazorStrap BlazorStrapSrc { get; set; } = null!;
        // ReSharper disable once NullableWarningSuppressionIsUsed

        // [Inject] public IJSRuntime Js { get; set; } = null!;

        /// <summary>
        /// Add [JSInvokable] above your override
        /// </summary>
        public virtual Task InteropEventCallback(string id, CallerName name, EventType type, Dictionary<string, string>? classList, JavascriptEvent? e)
            => Task.CompletedTask;

        /// <summary>
        /// Add [JSInvokable] above your override
        /// </summary>
        public virtual Task InteropEventCallback(string id, CallerName name, EventType type)
            => Task.CompletedTask;

        /// <summary>
        /// New Event Callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="targer"></param>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual Task OnEventAsync(string sender, string target, EventType type, object? data) => Task.CompletedTask;

        /// <summary>
        /// Add [JSInvokable] above your override
        /// </summary>
        public virtual Task InteropResizeComplete(int width)
            => Task.CompletedTask;

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

        [Parameter] public RenderFragment? ChildContent { get; set; }

        /// <inheritdoc/>
        [Parameter] public string Class { get; set; } = "";

        /// <inheritdoc/>
        [Parameter] public string DataId { get; set; } = Guid.NewGuid().ToString();

        /// <inheritdoc/>
        [Parameter]
        public Margins Margin { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Margins MarginBottom { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Margins MarginEnd { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Margins MarginLeftAndRight { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Margins MarginStart { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Margins MarginTop { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Margins MarginTopAndBottom { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Padding Padding { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Padding PaddingBottom { get; set; }

        /// <inheritdoc/>

        [Parameter]
        public Padding PaddingEnd { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Padding PaddingLeftAndRight { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Padding PaddingStart { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Padding PaddingTop { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Padding PaddingTopAndBottom { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Position Position { get; set; } = Position.Default;

        protected bool EventsSet;

    }
}