using System.Collections.Generic;
using Kopernicus;
using Kopernicus.Configuration;


namespace SigmaSeasonalizerPlugin
{
    [ParserTargetExternal("Body", "SigmaSeasonalizer", "Kopernicus")]
    internal class SigmaSeasonalizerLoader : BaseLoader, IParserEventSubscriber
    {
        internal static Dictionary<string, ConfigNode[]> scaledSeasonsDictionary = new Dictionary<string, ConfigNode[]>();

        public void Apply(ConfigNode node)
        {
            ConfigNode[] nodes = node.GetNodes("Material");

            if (nodes.Length > 0)
            {
                scaledSeasonsDictionary.Add(generatedBody.name, nodes);
            }
        }

        public void PostApply(ConfigNode node)
        {
        }
    }
}
