using System;

namespace IKVM.Compiler
{

    /// <summary>
    /// Implements <see cref="ManagedTypeInfo"/> by reflecting against an existing .NET type.
    /// </summary>
    class ReflectionManagedTypeInfo : ManagedTypeInfo
    {

        readonly Type type;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="type"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected ReflectionManagedTypeInfo(Type type)
        {
            this.type = type ?? throw new ArgumentNullException(nameof(type));
        }

    }

}
