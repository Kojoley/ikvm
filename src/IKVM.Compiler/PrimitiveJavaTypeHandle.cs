using IKVM.ByteCode.Syntax;

namespace IKVM.Compiler
{

    /// <summary>
    /// Represents a handle to a primitive Java type.
    /// </summary>
    class PrimitiveJavaTypeHandle : JavaTypeHandle
    {

        public static PrimitiveJavaTypeHandle Boolean = new PrimitiveJavaTypeHandle(JavaPrimitiveTypeName.Boolean);
        public static PrimitiveJavaTypeHandle Byte = new PrimitiveJavaTypeHandle(JavaPrimitiveTypeName.Byte);
        public static PrimitiveJavaTypeHandle Char = new PrimitiveJavaTypeHandle(JavaPrimitiveTypeName.Char);
        public static PrimitiveJavaTypeHandle Short = new PrimitiveJavaTypeHandle(JavaPrimitiveTypeName.Short);
        public static PrimitiveJavaTypeHandle Int = new PrimitiveJavaTypeHandle(JavaPrimitiveTypeName.Int);
        public static PrimitiveJavaTypeHandle Long = new PrimitiveJavaTypeHandle(JavaPrimitiveTypeName.Long);
        public static PrimitiveJavaTypeHandle Float = new PrimitiveJavaTypeHandle(JavaPrimitiveTypeName.Float);
        public static PrimitiveJavaTypeHandle Double = new PrimitiveJavaTypeHandle(JavaPrimitiveTypeName.Double);

        readonly JavaPrimitiveTypeName typeName;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="typeName"></param>
        PrimitiveJavaTypeHandle(JavaPrimitiveTypeName typeName)
        {
            this.typeName = typeName;
        }

        /// <summary>
        /// Returns <c>true</c>.
        /// </summary>
        public override bool IsPrimitive => true;

    }

}
