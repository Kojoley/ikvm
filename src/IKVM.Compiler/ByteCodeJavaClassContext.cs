using IKVM.ByteCode.Reading;
using IKVM.ByteCode.Syntax;

namespace IKVM.Compiler
{

    /// <summary>
    /// Provides Java class information from a source of <see cref="ClassReader"/>s
    /// </summary>
    public abstract class ByteCodeJavaClassContext : JavaClassContext
    {

        /// <summary>
        /// Implements the resolution of classes.
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        protected override JavaClassHandle ResolveCore(JavaClassName className) => new ByteCodeJavaClassHandle(Resolve(className));

        /// <summary>
        /// Override to implement resolution of class readers.
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        protected abstract new ClassReader Resolve(JavaClassName className);

    }

}
