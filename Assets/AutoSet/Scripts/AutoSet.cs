using System;
using UnityEngine;

namespace AutoSet.Utils
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AutoSetAttribute : PropertyAttribute
    {
        public AutoSetAttribute(Type type = default) { }
    }

    public abstract class AutoSetFromAttribute : AutoSetAttribute
    {
        public bool IncludeInactive { get; set; } = false;
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class AutoSetFromParentAttribute : AutoSetFromAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class AutoSetFromChildrenAttribute : AutoSetFromAttribute
    {
    }
}
