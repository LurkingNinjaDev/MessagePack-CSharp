﻿// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text;
using MessagePack.SourceGenerator.Transforms;
using Microsoft.CodeAnalysis;

namespace MessagePack.SourceGenerator;

public partial class MessagePackGenerator
{
#pragma warning disable RS1035 // Do not use APIs banned for analyzers
    private static readonly string FileHeader = """
// <auto-generated />

#pragma warning disable 618, 612, 414, 168, CS1591, SA1129, SA1309, SA1312, SA1403, SA1649

""".Replace(Environment.NewLine, "\r\n");
#pragma warning restore RS1035 // Do not use APIs banned for analyzers

    /// <summary>
    /// Generates the specialized resolver and formatters for the types that require serialization in a given compilation.
    /// </summary>
    /// <param name="context">Generator context.</param>
    /// <param name="model">The full messagepack object model.</param>
    private static void Generate(IGeneratorContext context, FullModel model)
    {
        AnalyzerOptions options = model.Options;
        StringBuilder sb = new();

        foreach (EnumSerializationInfo info in model.EnumInfos)
        {
            EnumTemplate transform = new(options, info);
            AddTransform(transform.TransformText(), transform.FileName);
        }

        foreach (UnionSerializationInfo info in model.UnionInfos)
        {
            UnionTemplate transform = new(options, info);
            AddTransform(transform.TransformText(), transform.FileName);
        }

        foreach (ObjectSerializationInfo info in model.ObjectInfos)
        {
            IFormatterTemplate transform = info.IsStringKey
                ? new StringKeyFormatterTemplate(options, info)
                : new FormatterTemplate(options, info);
            AddTransform(transform.TransformText(), transform.FileName);
        }

        void AddTransform(string transformOutput, string uniqueFileName)
        {
            sb.Clear();
            sb.Append(FileHeader);
            sb.Append(transformOutput);
            context.AddSource(CodeAnalysisUtilities.GetSanitizedFileName(uniqueFileName), sb.ToString());
            sb.Clear();
        }
    }

    private static void GenerateResolver(IGeneratorContext context, FullModel model)
    {
        if (model.IsEmpty)
        {
            return;
        }

        AnalyzerOptions options = model.Options;
        StringBuilder sb = new();

        IResolverRegisterInfo[] registerInfos = model.GenericInfos
            .Where(x => !x.IsOpenGenericType)
            .Cast<IResolverRegisterInfo>()
            .Concat(model.EnumInfos)
            .Concat(model.UnionInfos)
            .Concat(model.ObjectInfos.Where(x => !x.IsOpenGenericType))
            .ToArray();
        ResolverTemplate resolverTemplate = new(options, registerInfos);
        sb.Append(FileHeader);
        sb.Append(resolverTemplate.TransformText());
        context.AddSource(resolverTemplate.FileName, sb.ToString());
    }
}
