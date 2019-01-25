public class RuntimeState_Main : FluffyBox.RuntimeState
{
    public override void Begin()
    {
        base.Begin();

        Game.GameService.CreateGame();
    }
}
 