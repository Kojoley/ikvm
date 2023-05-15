using IKVM.ByteCode;
using IKVM.ByteCode.Syntax;

namespace IKVM.Compiler
{

    /// <summary>
    /// Describes a Java type.
    /// </summary>
    public abstract class JavaTypeHandle
    {

        /// <summary>
        /// Gets the Java class name.
        /// </summary>
        public abstract JavaTypeSignature? Signature { get; }

        /// <summary>
        /// Returns <c>true</c> if the type is a primitive java type.
        /// </summary>
        public abstract bool IsPrimitive { get; }

        /// <summary>
        /// Gets whether or not the primitive is a double or a long.
        /// </summary>
        public virtual bool IsWidePrimitive => false;

        /// <summary>
        /// Gets whether or not this Java type is primitive and appears on the stack as an integer value.
        /// </summary>
        internal bool IsIntOnStackPrimitive => IsPrimitive && (this == PrimitiveJavaTypeHandle.Boolean || this == PrimitiveJavaTypeHandle.Byte || this == PrimitiveJavaTypeHandle.Char || this == PrimitiveJavaTypeHandle.Short || this == PrimitiveJavaTypeHandle.Int);

        /// <summary>
        /// Returns <c>true</c> if this Java class represents an array.
        /// </summary>
        public bool IsArray => Signature != null && Signature.Value.IsArray;

        /// <summary>
        /// Gets the array rank of the type.
        /// </summary>
        public int ArrayRank => Signature != null ? Signature.Value.ArrayRank : 0;

        /// <summary>
        /// A ghost is an interface that appears to be implemented by a .NET type.
        /// </summary>
        public virtual bool IsGhost => false;

        /// <summary>
        /// Gets the access flag of the type.
        /// </summary>
        public virtual AccessFlag AccessFlag => 0;



        internal bool IsInternal => (AccessFlag & AccessFlag.InternalAccess) != 0;

        internal bool IsPublic
        {
            get
            {
                return (modifiers & Modifiers.Public) != 0;
            }
        }

        internal bool IsAbstract
        {
            get
            {
                // interfaces don't need to marked abstract explicitly (and javac 1.1 didn't do it)
                return (modifiers & (Modifiers.Abstract | Modifiers.Interface)) != 0;
            }
        }

        internal bool IsFinal
        {
            get
            {
                return (modifiers & Modifiers.Final) != 0;
            }
        }

    }

}
