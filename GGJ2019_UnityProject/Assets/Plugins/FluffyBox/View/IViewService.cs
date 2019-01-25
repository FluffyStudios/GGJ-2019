using System;
using System.Collections;

namespace FluffyBox
{
    public interface IViewService : IService
    {
        event EventHandlerOnLoadView OnLoadView;

        View MainView { get; set; }

        IEnumerator LoadMainView(string viewName);
    }
}
