﻿using System;
using System.Reflection.Emit;

using IKVM.Internal;

namespace IKVM.Runtime.Accessors
{

    /// <summary>
    /// Base class for accessors of class fields.
    /// </summary>
    internal abstract class FieldAccessor
    {

        readonly TypeWrapper type;
        readonly string name;
        readonly string signature;
        FieldWrapper field;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="signature"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected FieldAccessor(TypeWrapper type, string name, string signature)
        {
            this.type = type ?? throw new ArgumentNullException(nameof(type));
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.signature = signature ?? throw new ArgumentNullException(nameof(signature));
        }

        /// <summary>
        /// Gets the type which contains the field being accessed.
        /// </summary>
        protected TypeWrapper Type => type;

        /// <summary>
        /// Gets the name of the field being accessed.
        /// </summary>
        protected string Name => name;

        /// <summary>
        /// Gets the field being accessed.
        /// </summary>
        protected FieldWrapper Field => AccessorUtil.LazyGet(ref field, () => type.GetFieldWrapper(name, signature)) ?? throw new InvalidOperationException();

    }

    /// <summary>
    /// Provides fast access to a field of a given type.
    /// </summary>
    /// <typeparam name="TField"></typeparam>
    internal sealed class FieldAccessor<TField> : FieldAccessor
    {

        /// <summary>
        /// Gets a <see cref="FieldAccessor"/> for the given field on the given type.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public static FieldAccessor<TField> LazyGet(ref FieldAccessor<TField> location, TypeWrapper type, string name, string signature)
        {
            return AccessorUtil.LazyGet(ref location, () => new FieldAccessor<TField>(type, name, signature));
        }

        Func<TField> getter;
        Action<TField> setter;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="signature"></param>
        public FieldAccessor(TypeWrapper type, string name, string signature) :
            base(type, name, signature)
        {

        }

        /// <summary>
        /// Gets the getter for the field.
        /// </summary>
        Func<TField> Getter => AccessorUtil.LazyGet(ref getter, MakeGetter);

        /// <summary>
        /// Gets the setter for the field.
        /// </summary>
        Action<TField> Setter => AccessorUtil.LazyGet(ref setter, MakeSetter);

        /// <summary>
        /// Creates a new getter.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Func<TField> MakeGetter()
        {
#if FIRST_PASS || IMPORTER || EXPORTER
            throw new NotImplementedException();
#else
            var fieldType = Field.FieldTypeWrapper.EnsureLoadable(Type.GetClassLoader());
            fieldType.Finish();
            Type.Finish();
            Field.ResolveField();

            var dm = DynamicMethodUtil.Create($"__<FieldAccessorGet>__{Type.Name.Replace(".", "_")}__{Field.Name}", Type.TypeAsTBD, false, typeof(TField), Array.Empty<Type>());
            var il = CodeEmitter.Create(dm);

            Field.EmitGet(il);
            fieldType.EmitConvSignatureTypeToStackType(il);

            il.Emit(OpCodes.Ret);
            il.DoEmit();

            return (Func<TField>)dm.CreateDelegate(typeof(Func<TField>));
#endif
        }

        /// <summary>
        /// Creates a new setter.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Action<TField> MakeSetter()
        {
#if FIRST_PASS || IMPORTER || EXPORTER
            throw new NotImplementedException();
#else
            var fieldType = Field.FieldTypeWrapper.EnsureLoadable(Type.GetClassLoader());
            fieldType.Finish();
            Type.Finish();
            Field.ResolveField();

            var dm = DynamicMethodUtil.Create($"__<FieldAccessorSet>__{Type.Name.Replace(".", "_")}__{Field.Name}", Type.TypeAsTBD, false, typeof(void), new[] { typeof(TField) });
            var il = CodeEmitter.Create(dm);

            il.Emit(OpCodes.Ldarg_0);
            fieldType.EmitCheckcast(il);
            fieldType.EmitConvStackTypeToSignatureType(il, null);
            Field.EmitSet(il);

            il.Emit(OpCodes.Ret);
            il.DoEmit();

            return (Action<TField>)dm.CreateDelegate(typeof(Action<TField>));
#endif
        }

        /// <summary>
        /// Gets the value of the field.
        /// </summary>
        /// <returns></returns>
        public TField GetValue() => Getter();

        /// <summary>
        /// Sets the value of the field.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(TField value) => Setter(value);

    }

    /// <summary>
    /// Provides fast access to a field of a given type on the given object type.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TField"></typeparam>
    internal sealed class FieldAccessor<TObject, TField> : FieldAccessor
    {

        /// <summary>
        /// Gets a <see cref="FieldAccessor"/> for the given field on the given type.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public static FieldAccessor<TObject, TField> LazyGet(ref FieldAccessor<TObject, TField> location, TypeWrapper type, string name, string signature)
        {
            return AccessorUtil.LazyGet(ref location, () => new FieldAccessor<TObject, TField>(type, name, signature));
        }

        Func<TObject, TField> getter;
        Action<TObject, TField> setter;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="signature"></param>
        public FieldAccessor(TypeWrapper type, string name, string signature) :
            base(type, name, signature)
        {

        }

        /// <summary>
        /// Gets the getter for the field.
        /// </summary>
        Func<TObject, TField> Getter => AccessorUtil.LazyGet(ref getter, MakeGetter);

        /// <summary>
        /// Gets the setter for the field.
        /// </summary>
        Action<TObject, TField> Setter => AccessorUtil.LazyGet(ref setter, MakeSetter);

        /// <summary>
        /// Creates a new getter.
        /// </summary>
        /// <returns></returns>
        Func<TObject, TField> MakeGetter()
        {
#if FIRST_PASS || IMPORTER || EXPORTER
            throw new NotImplementedException();
#else
            var fieldType = Field.FieldTypeWrapper.EnsureLoadable(Type.GetClassLoader());
            fieldType.Finish();
            Type.Finish();
            Field.ResolveField();

            var dm = DynamicMethodUtil.Create($"__<FieldAccessorGet>__{Field.DeclaringType.Name.Replace(".", "_")}__{Field.Name}", Type.TypeAsTBD, false, typeof(TField), new[] { typeof(TObject) });
            var il = CodeEmitter.Create(dm);

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, Type.TypeAsBaseType);
            Field.EmitGet(il);
            fieldType.EmitConvSignatureTypeToStackType(il);

            il.Emit(OpCodes.Ret);
            il.DoEmit();

            return (Func<TObject, TField>)dm.CreateDelegate(typeof(Func<TObject, TField>));
#endif
        }

        /// <summary>
        /// Creates a new setter.
        /// </summary>
        /// <returns></returns>
        Action<TObject, TField> MakeSetter()
        {
#if FIRST_PASS || IMPORTER || EXPORTER
            throw new NotImplementedException();
#else
            var fieldType = Field.FieldTypeWrapper.EnsureLoadable(Type.GetClassLoader());
            fieldType.Finish();
            Type.Finish();
            Field.ResolveField();

            var dm = DynamicMethodUtil.Create($"__<FieldAccessorSet>__{Field.DeclaringType.Name.Replace(".", "_")}__{Field.Name}", Type.TypeAsTBD, false, typeof(void), new[] { typeof(TObject), typeof(TField) });
            var il = CodeEmitter.Create(dm);

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, Type.TypeAsBaseType);
            il.Emit(OpCodes.Ldarg_1);
            fieldType.EmitCheckcast(il);
            fieldType.EmitConvStackTypeToSignatureType(il, null);
            Field.EmitSet(il);

            il.Emit(OpCodes.Ret);
            il.DoEmit();

            return (Action<TObject, TField>)dm.CreateDelegate(typeof(Action<TObject, TField>));
#endif
        }

        /// <summary>
        /// Gets the value of the field.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public TField GetValue(TObject self) => Getter(self);

        /// <summary>
        /// Sets the value of the field.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="value"></param>
        public void SetValue(TObject self, TField value) => Setter(self, value);

    }

}