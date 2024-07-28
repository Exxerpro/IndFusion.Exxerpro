using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using Microsoft.Extensions.Logging;

namespace IndFusion.Exxerpro.Pages;

    public partial class Home : ComponentBase
    {
        [Inject] IJSRuntime JsRuntime { get; set; }
        [Inject] ILogger<Exxerpro> Logger { get; set; }
    

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Logger.LogInformation("Starting Home page and particles");
                try
                {
                    await JsRuntime.InvokeVoidAsync("particlesJS.load", "particles-js", "/particles.json");
                    Logger.LogInformation("particlesJS.load Invoked");
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Error loading particles.js");
                }
            }
        }

        private void GoTo(string page) => Snackbar.Add($"Clicked {page}");
    }
