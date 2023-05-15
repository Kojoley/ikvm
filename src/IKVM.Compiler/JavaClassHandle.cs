using System;

using IKVM.ByteCode.Syntax;

namespace IKVM.Compiler
{

    /// <summary>
    /// Describes a Java class.
    /// </summary>
    public abstract class JavaClassHandle : JavaTypeHandle
    {

        readonly JavaClassContext context;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected JavaClassHandle(JavaClassContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the <see cref="JavaClassContext"/> that owns this class handle.
        /// </summary>
        public JavaClassContext Context => context;

        /// <summary>
        /// Returns <c>false</c>.
        /// </summary>
        public sealed override bool IsPrimitive => false;

        /// <summary>
        /// Gets the Java class name.
        /// </summary>
        public abstract JavaTypeSignature? Signature { get; }

        /// <summary>
        /// Returns <c>true</c> if this Java class represents an array.
        /// </summary>
        public bool IsArray => Signature != null && Signature.Value.IsArray;

        /// <summary>
        /// Gets the rank of the class if it is an array.
        /// </summary>
        public int ArrayRank => Signature != null ? Signature.Value.ArrayRank ?? 0;

        /// <summary>
        /// A ghost is an interface that appears to be implemented by a .NET type.
        /// </summary>
        public virtual bool IsGhost => false;

    }

}
