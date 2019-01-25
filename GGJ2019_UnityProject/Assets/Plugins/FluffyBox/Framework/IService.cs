using System.Collections;

namespace FluffyBox
{
    public interface IService
    {
        UnityEngine.Coroutine StartCoroutine(IEnumerator routine);
    }
}
