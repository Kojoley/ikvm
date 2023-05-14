using IKVM.ByteCode.Syntax;

namespace IKVM.Compiler
{

    /// <summary>
    /// Represents a handle to a primitive Java type.
    /// </summary>
    class PrimitiveJavaTypeHandle : JavaTypeHandle
    {

        public static PrimitiveJavaTypeHandle Boolean = new PrimitiveJavaTypeHandle(JavaBaseTypeName.Boolean);
        public static PrimitiveJavaTypeHandle Byte = new PrimitiveJavaTypeHandle(JavaBaseTypeName.Byte);
        public static PrimitiveJavaTypeHandle Char = new PrimitiveJavaTypeHandle(JavaBaseTypeName.Char);
        public static PrimitiveJavaTypeHandle Short = new PrimitiveJavaTypeHandle(JavaBaseTypeName.Short);
        public static PrimitiveJavaTypeHandle Int = new PrimitiveJavaTypeHandle(JavaBaseTypeName.Int);
        public static PrimitiveJavaTypeHandle Long = new PrimitiveJavaTypeHandle(JavaBaseTypeName.Long);
        public static PrimitiveJavaTypeHandle Float = new PrimitiveJavaTypeHandle(JavaBaseTypeName.Float);
        public static PrimitiveJavaTypeHandle Double = new PrimitiveJavaTypeHandle(JavaBaseTypeName.Double);

        readonly JavaBaseTypeName typeName;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="typeName"></param>
        PrimitiveJavaTypeHandle(JavaBaseTypeName typeName)
        {
            this.typeName = typeName;
        }

        /// <summary>
        /// Returns <c>true</c>.
        /// </summary>
        public override bool IsPrimitive => true;

    }

}
