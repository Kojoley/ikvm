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

namespace IKVM.Internal
{

    sealed partial class ClassFile
	{
        private sealed class ConstantPoolItemFloat : ConstantPoolItem
		{
			internal float v;

			internal ConstantPoolItemFloat(BigEndianBinaryReader br)
			{
				v = br.ReadSingle();
			}

			internal override ConstantType GetConstantType()
			{
				return ConstantType.Float;
			}

			internal float Value
			{
				get
				{
					return v;
				}
			}

			internal override object GetRuntimeValue()
			{
				return v;
			}
		}
	}

}