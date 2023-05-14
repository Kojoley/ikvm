using System;

using IKVM.ByteCode.Syntax;

namespace IKVM.Compiler
{

    /// <summary>
    /// Provides a handle to a Java class described by a <see cref="ManagedTypeInfo"/>.
    /// </summary>
    sealed class ManagedJavaClassHandle : JavaClassHandle
    {

        readonly ManagedTypeInfo type;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="type"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ManagedJavaClassHandle(ManagedJavaClassContext context, ManagedTypeInfo type) :
            base(context)
        {
            this.type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Gets the Java class name that correlates to the specified managed type.
        /// </summary>
        public override JavaClassName ClassName => "cli." + type.Name;

    }

}
