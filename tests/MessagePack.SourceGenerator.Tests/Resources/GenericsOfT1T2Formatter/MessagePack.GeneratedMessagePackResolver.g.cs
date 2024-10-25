﻿// <auto-generated />

#pragma warning disable 618, 612, 414, 168, CS1591, SA1129, SA1309, SA1312, SA1403, SA1649

using MsgPack = global::MessagePack;

[assembly: MsgPack::Internal.GeneratedAssemblyMessagePackResolverAttribute(typeof(MessagePack.GeneratedMessagePackResolver), 3, 0)]

namespace MessagePack {

/// <summary>A MessagePack resolver that uses generated formatters for types in this assembly.</summary>
partial class GeneratedMessagePackResolver : MsgPack::IFormatterResolver
{
	/// <summary>An instance of this resolver that only returns formatters specifically generated for types in this assembly.</summary>
	public static readonly MsgPack::IFormatterResolver Instance = new GeneratedMessagePackResolver();

	private GeneratedMessagePackResolver()
	{
	}

	public MsgPack::Formatters.IMessagePackFormatter<T> GetFormatter<T>()
	{
		return FormatterCache<T>.Formatter;
	}

	private static class FormatterCache<T>
	{
		internal static readonly MsgPack::Formatters.IMessagePackFormatter<T> Formatter;

		static FormatterCache()
		{
			var f = GeneratedMessagePackResolverGetFormatterHelper.GetFormatter(typeof(T));
			if (f != null)
			{
				Formatter = (MsgPack::Formatters.IMessagePackFormatter<T>)f;
			}
		}
	}

	private static class GeneratedMessagePackResolverGetFormatterHelper
	{
		private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, int> closedTypeLookup = new global::System.Collections.Generic.Dictionary<global::System.Type, int>(4)
		{
			{ typeof(global::TempProject.MyGenericObject<int, string>), 0 },
			{ typeof(global::TempProject.MyGenericObject<global::TempProject.MyGenericObject<int, string>, global::TempProject.MyGenericObject<int, string>>), 1 },
			{ typeof(global::TempProject.MyObject), 2 },
			{ typeof(global::TempProject.MyObjectNested), 3 },
		};
		private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, int> openTypeLookup = new global::System.Collections.Generic.Dictionary<global::System.Type, int>(1)
		{
			{ typeof(global::TempProject.MyGenericObject<,>), 0 },
		};

		internal static object GetFormatter(global::System.Type t)
		{
			if (closedTypeLookup.TryGetValue(t, out int closedKey))
			{
				switch (closedKey)
				{
					case 0: return new global::MessagePack.GeneratedMessagePackResolver.TempProject.MyGenericObjectFormatter<int, string>();
					case 1: return new global::MessagePack.GeneratedMessagePackResolver.TempProject.MyGenericObjectFormatter<global::TempProject.MyGenericObject<int, string>, global::TempProject.MyGenericObject<int, string>>();
					case 2: return new global::MessagePack.GeneratedMessagePackResolver.TempProject.MyObjectFormatter();
					case 3: return new global::MessagePack.GeneratedMessagePackResolver.TempProject.MyObjectNestedFormatter();
					default: return null; // unreachable
				};
			}
			if (t.IsGenericType && openTypeLookup.TryGetValue(t.GetGenericTypeDefinition(), out int openKey))
			{
				switch (openKey)
				{
					case 0: return global::System.Activator.CreateInstance(typeof(global::MessagePack.GeneratedMessagePackResolver.TempProject.MyGenericObjectFormatter<,>).MakeGenericType(t.GenericTypeArguments));
					default: return null; // unreachable
				};
			}

			return null;
		}
	}
}

}
