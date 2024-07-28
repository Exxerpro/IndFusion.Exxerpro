using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace IndFusion.Components.Utilities
{
    public interface ISvgLoader
    {
        Task<MarkupString> LoadSvg(string url);
    }
}