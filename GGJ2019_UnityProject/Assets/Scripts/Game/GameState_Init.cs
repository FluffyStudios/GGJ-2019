public class GameState_Init : FluffyBox.GameState
{
    private IGameService GameService
    {
        get;
        set;
    }
    
    public override void Begin()
    {
        base.Begin();

        Gui.GuiService.ShowWindow<MainGameScreen>();
    }

    public override void End()
    {
        base.End();
    }
}