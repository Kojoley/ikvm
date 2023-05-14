using System;
using System.Reflection.Metadata;

namespace IKVM.Compiler
{

    /// <summary>
    /// Implements <see cref="ManagedTypeInfo"/> by accessing a <see cref="TypeDefinition"/>.
    /// </summary>
    class MetadataManagedTypeInfo : ManagedTypeInfo
    {

        readonly MetadataReader metadata;
        readonly TypeDefinition type;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="type"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected MetadataManagedTypeInfo(MetadataReader metadata, TypeDefinition type)
        {
            this.metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
            this.type = type;
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public override string Name => metadata.GetString(type.Name);

    }

}
