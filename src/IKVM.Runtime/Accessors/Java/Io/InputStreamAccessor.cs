﻿namespace IKVM.Runtime.Accessors.Java.Io
{

    /// <summary>
    /// Provides runtime access to the 'java.io.InputStream' type.
    /// </summary>
    internal sealed class InputStreamAccessor : Accessor
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="resolver"></param>
        public InputStreamAccessor(AccessorTypeResolver resolver) :
            base(resolver("java.io.InputStream"))
        {

        }

    }

}