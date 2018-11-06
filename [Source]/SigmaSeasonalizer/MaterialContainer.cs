using Kopernicus;
using Kopernicus.Configuration;


namespace SigmaSeasonalizerPlugin
{
    internal class MaterialContainer
    {
        internal virtual string shader
        {
            get { return ""; }
        }

        internal ColorParser newColorParser(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                ColorParser parser = new ColorParser();
                parser.SetFromString(str);
                return parser;
            }

            return null;
        }

        internal Texture2DParser newTexture2DParser(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Texture2DParser parser = new Texture2DParser();
                parser.SetFromString(str);
                return parser;
            }

            return null;
        }

        internal NumericParser<T> newNumericParser<T>(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                NumericParser<T> parser = new NumericParser<T>();
                parser.SetFromString(str);
                return parser;
            }

            return null;
        }

        internal static MaterialContainer Load(string shaderName, ConfigNode node)
        {
            switch (shaderName)
            {
                case "Terrain/Scaled Planet (Simple)":
                    return new ScaledPlanetSimpleContainer(node);
                case "Terrain/Scaled Planet (RimLight)":
                    return new ScaledPlanetRimLightContainer(node);
                case "Terrain/Scaled Planet (RimAerial)":
                    return new ScaledPlanetRimAerialContainer(node);
                case "Emissive Multi Ramp Sunspots":
                    return new EmissiveMultiRampSunspotsContainer(node);
                default: return null;
            }
        }
    }

    internal class ScaledPlanetSimpleContainer : MaterialContainer
    {
        internal override string shader
        {
            get { return "Terrain/Scaled Planet (Simple)"; }
        }
        internal ColorParser _Color;
        internal ColorParser _SpecColor;
        internal NumericParser<float> _Shininess;
        internal string _MainTex;
        internal string _BumpMap;

        internal ScaledPlanetSimpleContainer(ConfigNode node)
        {
            _Color = newColorParser(node.GetValue("_Color"));
            _SpecColor = newColorParser(node.GetValue("_SpecColor"));
            _Shininess = newNumericParser<float>(node.GetValue("_Shininess"));
            _MainTex = node.GetValue("_MainTex");
            _BumpMap = node.GetValue("_BumpMap");
        }
    }

    internal class ScaledPlanetRimLightContainer : MaterialContainer
    {
        internal override string shader
        {
            get { return "Terrain/Scaled Planet (RimLight)"; }
        }
        internal ColorParser _Color;
        internal ColorParser _SpecColor;
        internal NumericParser<float> _Shininess;
        internal string _MainTex;
        internal string _BumpMap;
        internal NumericParser<float> _RimColor;
        internal NumericParser<float> _RimPower;

        internal ScaledPlanetRimLightContainer(ConfigNode node)
        {
            _Color = newColorParser(node.GetValue("_Color"));
            _SpecColor = newColorParser(node.GetValue("_SpecColor"));
            _Shininess = newNumericParser<float>(node.GetValue("_Shininess"));
            _MainTex = node.GetValue("_MainTex");
            _BumpMap = node.GetValue("_BumpMap");
            _RimColor = newNumericParser<float>(node.GetValue("_RimColor"));
            _RimPower = newNumericParser<float>(node.GetValue("_RimPower"));
        }
    }

    internal class ScaledPlanetRimAerialContainer : MaterialContainer
    {
        internal override string shader
        {
            get { return "Terrain/Scaled Planet (RimAerial)"; }
        }
        internal ColorParser _Color;
        internal ColorParser _SpecColor;
        internal NumericParser<float> _Shininess;
        internal string _MainTex;
        internal string _BumpMap;
        internal NumericParser<float> _rimPower;
        internal NumericParser<float> _rimBlend;
        internal string _rimColorRamp;

        internal ScaledPlanetRimAerialContainer(ConfigNode node)
        {
            _Color = newColorParser(node.GetValue("_Color"));
            _SpecColor = newColorParser(node.GetValue("_SpecColor"));
            _Shininess = newNumericParser<float>(node.GetValue("_Shininess"));
            _MainTex = node.GetValue("_MainTex");
            _BumpMap = node.GetValue("_BumpMap");
            _rimPower = newNumericParser<float>(node.GetValue("_rimPower"));
            _rimBlend = newNumericParser<float>(node.GetValue("_rimBlend"));
            _rimColorRamp = node.GetValue("_rimColorRamp");
        }
    }

    internal class EmissiveMultiRampSunspotsContainer : MaterialContainer
    {
        internal override string shader
        {
            get { return "Emissive Multi Ramp Sunspots"; }
        }
        internal Texture2DParser _RampMap;
        internal Texture2DParser _NoiseMap;
        internal ColorParser _EmitColor0;
        internal ColorParser _EmitColor1;
        internal Texture2DParser _SunspotTex;
        internal NumericParser<float> _SunspotPower;
        internal ColorParser _SunspotColor;
        internal ColorParser _RimColor;
        internal NumericParser<float> _RimPower;
        internal NumericParser<float> _RimBlend;

        internal EmissiveMultiRampSunspotsContainer(ConfigNode node)
        {
            _RampMap = newTexture2DParser(node.GetValue("_RampMap"));
            _NoiseMap = newTexture2DParser(node.GetValue("_NoiseMap"));
            _EmitColor0 = newColorParser(node.GetValue("_EmitColor0"));
            _EmitColor1 = newColorParser(node.GetValue("_EmitColor1"));
            _SunspotTex = newTexture2DParser(node.GetValue("_SunspotTex"));
            _SunspotPower = newNumericParser<float>(node.GetValue("_SunspotPower"));
            _SunspotColor = newColorParser(node.GetValue("_SunspotColor"));
            _RimColor = newColorParser(node.GetValue("_RimColor"));
            _RimPower = newNumericParser<float>(node.GetValue("_RimPower"));
            _RimBlend = newNumericParser<float>(node.GetValue("_RimBlend"));
        }
    }
}
