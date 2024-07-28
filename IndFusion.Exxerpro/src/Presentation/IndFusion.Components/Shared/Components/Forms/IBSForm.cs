namespace IndFusion.Components.Shared.Components.Forms
{
    public interface IBSForm
    {
        event Action? OnResetEventHandler;
        void Refresh();
        public void Reset();
    }
}