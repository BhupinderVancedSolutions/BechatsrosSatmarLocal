using System;

namespace Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonServiceAttribute : Attribute
    {
    }
}
