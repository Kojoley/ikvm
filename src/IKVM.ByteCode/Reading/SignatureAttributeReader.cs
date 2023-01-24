﻿using IKVM.ByteCode.Parsing;

namespace IKVM.ByteCode.Reading
{

    internal class SignatureAttributeReader : AttributeReader<SignatureAttributeRecord>
    {

        string value;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="declaringClass"></param>
        /// <param name="info"></param>
        /// <param name="data"></param>
        internal SignatureAttributeReader(ClassReader declaringClass, AttributeInfoReader info, SignatureAttributeRecord data) :
            base(declaringClass, info, data)
        {

        }

        /// <summary>
        /// Gets the signature value.
        /// </summary>
        public string Value => LazyGet(ref value, () => DeclaringClass.ResolveConstant<Utf8ConstantReader>(Record.SignatureIndex).Value);

    }

}
