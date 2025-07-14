using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.UI.EventBus.Attributes
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
