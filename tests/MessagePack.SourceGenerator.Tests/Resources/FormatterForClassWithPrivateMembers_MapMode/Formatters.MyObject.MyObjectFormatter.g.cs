﻿// <auto-generated />

#pragma warning disable 618, 612, 414, 168, CS1591, SA1129, SA1309, SA1312, SA1403, SA1649

using MsgPack = global::MessagePack;

partial class MyObject {
	internal sealed class MyObjectFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::MyObject>
	{
		// value
		private static global::System.ReadOnlySpan<byte> GetSpan_value() => new byte[1 + 5] { 165, 118, 97, 108, 117, 101 };

		public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::MyObject value, global::MessagePack.MessagePackSerializerOptions options)
		{
			if (value is null)
			{
				writer.WriteNil();
				return;
			}

			writer.WriteMapHeader(1);
			writer.WriteRaw(GetSpan_value());
			writer.Write(value.value);
		}

		public global::MyObject Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
		{
			if (reader.TryReadNil())
			{
				return null;
			}

			options.Security.DepthStep(ref reader);
			var length = reader.ReadMapHeader();
			var ____result = new global::MyObject();

			for (int i = 0; i < length; i++)
			{
				var stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
				switch (stringKey.Length)
				{
					default:
					FAIL:
					  reader.Skip();
					  continue;
					case 5:
					    if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 435761734006UL) { goto FAIL; }

					    ____result.value = reader.ReadInt32();
					    continue;

				}
			}

			reader.Depth--;
			return ____result;
		}
	}

}
