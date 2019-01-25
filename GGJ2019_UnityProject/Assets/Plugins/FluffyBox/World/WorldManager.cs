using System.Collections;

namespace FluffyBox
{
    public partial class WorldManager : Manager, IWorldService
    {
        public override void RegisterService()
        {
            Services.AddService<IWorldService>(this);
        }

        public override IEnumerator Ignite()
        {
            yield return base.Ignite();
        }

        public override void OnIgnitionCompleted()
        {
        }
    }
}
