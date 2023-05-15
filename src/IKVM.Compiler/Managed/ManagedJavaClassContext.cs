﻿using IKVM.ByteCode.Syntax;

namespace IKVM.Compiler.Managed
{

    /// <summary>
    /// Provides Java class information from a source of existing managed types.
    /// </summary>
    public abstract class ManagedJavaClassContext : JavaClassContext
    {

        /// <summary>
        /// Implements the resolution of classes.
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        protected override JavaClassHandle ResolveCore(JavaClassName className) => new ManagedJavaClassHandle(this,Resolve(className));

        /// <summary>
        /// Override to implement resolution of types.
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        protected abstract new IManagedTypeInfo Resolve(JavaClassName className);

    }

}
