using System;

namespace Model.Combat.Actions
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DontCallFromSpells : Attribute
    {
        
    }
}