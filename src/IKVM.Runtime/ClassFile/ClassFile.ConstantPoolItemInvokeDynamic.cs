﻿/*
  Copyright (C) 2002-2015 Jeroen Frijters

  This software is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this software.

  Permission is granted to anyone to use this software for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this software must not be misrepresented; you must not
     claim that you wrote the original software. If you use this software
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original software.
  3. This notice may not be removed or altered from any source distribution.

  Jeroen Frijters
  jeroen@frijters.net
  
*/
using IKVM.ByteCode.Reading;

namespace IKVM.Internal
{

    sealed partial class ClassFile
    {
        internal sealed class ConstantPoolItemInvokeDynamic : ConstantPoolItem
        {

            readonly ushort bootstrap_specifier_index;
            readonly ushort name_and_type_index;

            string name;
            string descriptor;
            TypeWrapper[] argTypeWrappers;
            TypeWrapper retTypeWrapper;

            /// <summary>
            /// Initializes a new instance.
            /// </summary>
            /// <param name="reader"></param>
            internal ConstantPoolItemInvokeDynamic(InvokeDynamicConstantReader reader)
            {
                bootstrap_specifier_index = reader.Record.BootstrapMethodAttributeIndex;
                name_and_type_index = reader.Record.NameAndTypeIndex;
            }

            internal override void Resolve(ClassFile classFile, string[] utf8_cp, ClassFileParseOptions options)
            {
                var name_and_type = (ConstantPoolItemNameAndType)classFile.GetConstantPoolItem(name_and_type_index);
                // if the constant pool items referred to were strings, GetConstantPoolItem returns null
                if (name_and_type == null)
                    throw new ClassFormatError("Bad index in constant pool");

                name = string.Intern(classFile.GetConstantPoolUtf8String(utf8_cp, name_and_type.nameIndex));
                descriptor = string.Intern(classFile.GetConstantPoolUtf8String(utf8_cp, name_and_type.descriptorIndex).Replace('/', '.'));
            }

            internal override void Link(TypeWrapper thisType, LoadMode mode)
            {
                lock (this)
                {
                    if (argTypeWrappers != null)
                    {
                        return;
                    }
                }

                var classLoader = thisType.GetClassLoader();
                var args = classLoader.ArgTypeWrapperListFromSig(descriptor, mode);
                var ret = classLoader.RetTypeWrapperFromSig(descriptor, mode);

                lock (this)
                {
                    if (argTypeWrappers == null)
                    {
                        argTypeWrappers = args;
                        retTypeWrapper = ret;
                    }
                }
            }

            internal TypeWrapper[] GetArgTypes()
            {
                return argTypeWrappers;
            }

            internal TypeWrapper GetRetType()
            {
                return retTypeWrapper;
            }

            internal string Name
            {
                get { return name; }
            }

            internal string Signature
            {
                get { return descriptor; }
            }

            internal ushort BootstrapMethod
            {
                get { return bootstrap_specifier_index; }
            }

        }

    }

}
