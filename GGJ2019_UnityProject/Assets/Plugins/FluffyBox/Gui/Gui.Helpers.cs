namespace FluffyBox
{
    public class Gui
    {
        private static IGuiService privateGuiService;
        private static IKeyMappingService privateKeyMappingService;

        public static IGuiService GuiService
        {
            get
            {
                if (Gui.privateGuiService == null)
                {
                    Gui.privateGuiService = Services.GetService<IGuiService>();
                }

                return Gui.privateGuiService;
            }
        }

        public static IKeyMappingService KeyMappingService
        {
            get
            {
                if (Gui.privateKeyMappingService == null)
                {
                    Gui.privateKeyMappingService = Services.GetService<IKeyMappingService>();
                }

                return Gui.privateKeyMappingService;
            }
        }
    }
}