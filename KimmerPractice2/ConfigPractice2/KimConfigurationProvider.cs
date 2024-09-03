using Microsoft.Extensions.Configuration;
using System.Xml;

namespace ConfigPractice2;

internal class KimConfigurationProvider : FileConfigurationProvider
{
    public KimConfigurationProvider(KimConfihurationSource source) : base(source)
    {
    }

    public override void Load(Stream stream)
    {
        Dictionary<string, string> data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.Load(stream);
        XmlNodeList nodes = xmlDoc.SelectNodes(@"/configuration/ConnectionStrings/add");
        //["connextionStr1":{"connectionString":"","provider":""},"connextionStr2":{"connectionString":"","provider":""}]
        foreach (XmlNode node in nodes.Cast<XmlNode>())
        {
            string name = node.Attributes["name"].Value;
            string connectionString = node.Attributes["connectionString"].Value;
            string? provider = node.Attributes["provider"]?.Value;

            data[$"{name}:connectionString"] = connectionString;
            if (!string.IsNullOrWhiteSpace(provider))
            {
                data[$"{name}:provider"] = provider;
            }
        }

        XmlNodeList otherNodes = xmlDoc.SelectNodes(@"/configuration/appSettings/add");
        foreach (XmlNode node in otherNodes.Cast<XmlNode>())
        {
            string name = node.Attributes["name"].Value;
            string value = node.Attributes["value"].Value;

            name = name.Replace(",", ":");
            name = name.Replace(".", ":");

            data.Add(name, value);
        }

        this.Data = data;
    }
}
