namespace IKVM.Compiler
{

    /// <summary>
    /// Describes a Java type.
    /// </summary>
    public abstract class JavaTypeHandle
    {

        /// <summary>
        /// Returns <c>true</c> if the type is a primitive java type.
        /// </summary>
        public abstract bool IsPrimitive { get; }

    }

}
