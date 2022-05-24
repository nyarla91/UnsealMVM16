using System;

namespace Model.Combat.Effects
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DontCallFromSpells : Attribute
    {
        
    }
}