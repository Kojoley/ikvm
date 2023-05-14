using System;

using IKVM.ByteCode.Reading;

namespace IKVM.Compiler
{

    /// <summary>
    /// Provides a handle to a Java class described by a <see cref="ClassReader"/> over class data.
    /// </summary>
    sealed class ByteCodeJavaClassHandle : JavaClassHandle
    {

        readonly ClassReader reader;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="reader"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ByteCodeJavaClassHandle(ByteCodeJavaClassContext context, ClassReader reader) :
            base(context)
        {
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

    }

}
