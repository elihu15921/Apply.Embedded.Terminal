﻿namespace IIoT.Infrastructure.Facilities;
internal sealed class YamlProvider : FileConfigurationProvider
{
    public YamlProvider(FileConfigurationSource source) : base(source) { }
    public override void Load(Stream stream)
    {
        YamlStream yamlStream = new();
        Stack<string> stackContext = new();
        yamlStream.Load(new StreamReader(stream));
        var data = new SortedDictionary<string, string?>(StringComparer.Ordinal);
        if (yamlStream.Documents.Count > 0) VisitYamlNode(string.Empty, yamlStream.Documents[default].RootNode);
        void VisitYamlNode(string context, YamlNode node)
        {
            string currentPath;
            if (node is YamlScalarNode scalarNode)
            {
                EnterContext(context);
                data[currentPath] = scalarNode.Value ?? string.Empty;
                ExitContext();
            }
            else if (node is YamlMappingNode mappingNode)
            {
                EnterContext(context);
                foreach (var yamlNode in mappingNode.Children)
                {
                    context = ((YamlScalarNode)yamlNode.Key).Value ?? string.Empty;
                    VisitYamlNode(context, yamlNode.Value);
                }
                ExitContext();
            }
            else if (node is YamlSequenceNode sequenceNode)
            {
                EnterContext(context);
                for (int item = default; item < sequenceNode.Children.Count; item++) VisitYamlNode(item.ToString(), sequenceNode.Children[item]);
                ExitContext();
            }
            void EnterContext(string context)
            {
                if (!string.IsNullOrEmpty(context)) stackContext.Push(context);
                currentPath = ConfigurationPath.Combine(stackContext.Reverse());
            }
            void ExitContext()
            {
                if (stackContext.Any()) stackContext.Pop();
                currentPath = ConfigurationPath.Combine(stackContext.Reverse());
            }
        }
        Data = data;
    }
}