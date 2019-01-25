public class RuntimeManager : FluffyBox.RuntimeManager
{
    public override void OnIgnitionCompleted()
    {
        this.ChangeRuntimeState(new RuntimeState_Main());
    }
}
