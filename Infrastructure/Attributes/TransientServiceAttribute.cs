using System;

namespace Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TransientServiceAttribute : Attribute
    {
    }
}
