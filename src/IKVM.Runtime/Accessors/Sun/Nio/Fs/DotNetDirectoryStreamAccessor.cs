﻿using System;
using System.Collections.Generic;

namespace IKVM.Runtime.Accessors.Sun.Nio.Fs
{

    internal sealed class DotNetDirectoryStreamAccessor : Accessor<object>
    {

        MethodAccessor<Func<object, IEnumerable<string>, object, object>> init;
        FieldAccessor<object, string> path;
        FieldAccessor<object, IEnumerable<string>> files;
        FieldAccessor<object, object> filter;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="resolver"></param>
        public DotNetDirectoryStreamAccessor(AccessorTypeResolver resolver) :
            base(resolver("sun.nio.fs.DotNetDirectoryStream"))
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="files"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public object Init(object path, IEnumerable<string> files, object filter) => GetConstructor(ref init, "(Lsun.nio.fs.DotNetPath;Lcli.System.Collections.IEnumerable;Ljava.nio.file.DirectoryStream$Filter;)V").Invoker(path,files, filter);

        /// <summary>
        /// Gets the value of the 'path' field.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public object GetPath(object self) => GetField(ref path, nameof(path), "Lsun.nio.fs.DotNetPath;").GetValue(self);

        /// <summary>
        /// Gets the value of the 'files' field.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public IEnumerable<string> GetFiles(object self) => GetField(ref files, nameof(files), "Lcli.System.Collections.Enumerable;").GetValue(self);

        /// <summary>
        /// Gets the value of the 'filter' field.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public object GetFilter(object self) => GetField(ref filter, nameof(filter), "Ljava.nio.file.DirectoryStream$Filter;").GetValue(self);

    }

}