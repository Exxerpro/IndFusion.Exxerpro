﻿using IndFusion.Components.Service;
using IndFusion.Components.Shared.Enums;
using System;
using System.Threading.Tasks;

namespace IndFusion.Components.Interfaces
{
    public interface IBlazorStrap
    {
        Func<int, Task>? OnResized { get; set; }
        BlazorStrapInterop JavaScriptInterop { get; }
        bool ShowDebugMessages { get; }
        Toaster Toaster { get; }
        public T CurrentTheme<T>() where T : Enum;
        Task SetBootstrapCss();
        Task SetBootstrapCss(string themeUrl);
        Task SetBootstrapCss<T>(T theme) where T : Enum;
        void ForwardClick(string id);
        Task InvokeEvent(string sender, string target, EventType type, object data);
        Task InvokeResize(int width);
    }
}