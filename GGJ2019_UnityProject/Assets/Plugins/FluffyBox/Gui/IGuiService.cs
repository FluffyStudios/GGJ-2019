namespace FluffyBox
{
    public interface IGuiService : IService
    {
        void ShowWindow<T>(bool animated = true);

        void ShowWindow(GuiWindow guiWindow, bool animated = true);

        void HideWindow<T>(bool animated = true);

        void HideWindow(GuiWindow guiWindow, bool animated = true);

        T GetWindow<T>() where T : GuiWindow;
    }
}
