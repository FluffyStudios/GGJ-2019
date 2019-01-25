using System;

namespace FluffyBox
{
    public delegate void EventHandlerOnLoadView(object sender, EventArgsOnLoadView e);

    public class EventArgsOnLoadView : EventArgs
    {
        public EventArgsOnLoadView(View view)
        {
            this.View = view;
        }

        public View View
        {
            get;
            set;
        }
    }
}