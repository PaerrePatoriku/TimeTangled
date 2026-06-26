using System;

namespace Game.UI.EventBus.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class UISignal : Attribute
    {
        public Type EventType { get; set; }
        public UISignal()
        {

        }


    }
}
