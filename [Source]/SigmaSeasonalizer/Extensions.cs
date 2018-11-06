using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Kopernicus.OnDemand;


namespace SigmaSeasonalizerPlugin
{
    internal static class Extensions
    {
        // Improved ElementAt
        internal static KeyValuePair<double, TValue> At<TValue>(this SortedDictionary<double, TValue> source, int index)
        {
            int n = source.Count;

            if (index == -1)
            {
                KeyValuePair<double, TValue> pair = source.ElementAt(n - 1);

                return new KeyValuePair<double, TValue>(pair.Key - Math.PI * 2, pair.Value);
            }

            if (index == n)
            {
                KeyValuePair<double, TValue> pair = source.ElementAt(0);

                return new KeyValuePair<double, TValue>(pair.Key + Math.PI * 2, pair.Value);
            }

            return source.ElementAt(index);
        }

        // Evaluate from SortedDictionary
        internal static T Evaluate<T>(this SortedDictionary<double, T> source, double key)
        {
            int? n = source?.Count;

            if (n > 1)
            {
                KeyValuePair<double, T> b;
                KeyValuePair<double, T> a;

                for (int i = 0; i <= n; i++)
                {
                    b = source.At(i);

                    if (b.Key > key)
                    {
                        a = source.At(i - 1);
                        return (T)Utility.Lerp(a.Value, b.Value, (key - a.Key) / (b.Key - a.Key));
                    }
                }
            }

            return source.ElementAt(0).Value;
        }

        internal static void Evaluate(this Renderer renderer, SortedDictionary<double, MaterialContainer> dictionary, double key)
        {
            Material material = renderer?.material;
            int? n = dictionary?.Count;

            if (material != null && n > 0)
            {
                KeyValuePair<double, MaterialContainer> a;

                if (n > 1)
                {
                    KeyValuePair<double, MaterialContainer> b;

                    for (int i = 0; i <= n; i++)
                    {
                        b = dictionary.At(i);

                        if (b.Key > key)
                        {
                            a = dictionary.At(i - 1);

                            double t = (key - a.Key) / (b.Key - a.Key);
                            renderer.Lerp(a.Value, b.Value, t);

                            return;
                        }
                    }
                }

                a = dictionary.At(0);
                renderer.Lerp(a.Value, a.Value, 0);
            }
        }

        // Evaluate from LerpMaterials
        internal static void Lerp(this Renderer renderer, MaterialContainer a, MaterialContainer b, double t)
        {
            Material material = renderer?.material;
            string shader = material?.shader?.name;

            if (!string.IsNullOrEmpty(shader) && shader == a?.shader && shader == b?.shader)
            {
                if (shader == "Terrain/Scaled Planet (Simple)")
                {
                    renderer.Lerp((ScaledPlanetSimpleContainer)a, (ScaledPlanetSimpleContainer)b, t);
                }

                else

                if (shader == "Terrain/Scaled Planet (RimLight)")
                {
                    renderer.Lerp((ScaledPlanetRimLightContainer)a, (ScaledPlanetRimLightContainer)b, t);
                }

                else

                if (shader == "Terrain/Scaled Planet (RimAerial)")
                {
                    renderer.Lerp((ScaledPlanetRimAerialContainer)a, (ScaledPlanetRimAerialContainer)b, t);
                }

                else

                if (shader == "Emissive Multi Ramp Sunspots")
                {
                    renderer.Lerp((EmissiveMultiRampSunspotsContainer)a, (EmissiveMultiRampSunspotsContainer)b, t);
                }
            }
        }

        internal static void Lerp(this Renderer renderer, ScaledPlanetSimpleContainer a, ScaledPlanetSimpleContainer b, double t)
        {
            Material material = renderer?.material;

            if (a._Color != null && b._Color != null)
                material.SetColor("_Color", Utility.Lerp(a._Color, b._Color, t));
            if (a._SpecColor != null && b._SpecColor != null)
                material.SetColor("_SpecColor", Utility.Lerp(a._Color, b._SpecColor, t));
            if (a._Shininess != null && b._Shininess != null)
                material.SetFloat("_Shininess", Utility.Lerp(a._Shininess, b._Shininess, t));
            if (!string.IsNullOrEmpty(a._MainTex) && !string.IsNullOrEmpty(b._MainTex))
                renderer.LerpTexture("_MainTex", a._MainTex, b._MainTex, t);
            if (!string.IsNullOrEmpty(a._BumpMap) && !string.IsNullOrEmpty(b._BumpMap))
                renderer.LerpTexture("_BumpMap", a._BumpMap, b._BumpMap, t);
        }

        internal static void Lerp(this Renderer renderer, ScaledPlanetRimLightContainer a, ScaledPlanetRimLightContainer b, double t)
        {
            Material material = renderer?.material;

            if (a._Color != null && b._Color != null)
                material.SetColor("_Color", Utility.Lerp(a._Color, b._Color, t));
            if (a._SpecColor != null && b._SpecColor != null)
                material.SetColor("_SpecColor", Utility.Lerp(a._Color, b._SpecColor, t));
            if (a._Shininess != null && b._Shininess != null)
                material.SetFloat("_Shininess", Utility.Lerp(a._Shininess, b._Shininess, t));
            if (!string.IsNullOrEmpty(a._MainTex) && !string.IsNullOrEmpty(b._MainTex))
                renderer.LerpTexture("_MainTex", a._MainTex, b._MainTex, t);
            if (!string.IsNullOrEmpty(a._BumpMap) && !string.IsNullOrEmpty(b._BumpMap))
                renderer.LerpTexture("_BumpMap", a._BumpMap, b._BumpMap, t);
            if (a._RimColor != null && b._RimColor != null)
                material.SetFloat("_RimColor", Utility.Lerp(a._RimColor, b._RimColor, t));
            if (a._RimPower != null && b._RimPower != null)
                material.SetFloat("_RimPower", Utility.Lerp(a._RimPower, b._RimPower, t));
        }

        internal static void Lerp(this Renderer renderer, ScaledPlanetRimAerialContainer a, ScaledPlanetRimAerialContainer b, double t)
        {
            Material material = renderer?.material;

            if (a._Color != null && b._Color != null)
                material.SetColor("_Color", Utility.Lerp(a._Color, b._Color, t));
            if (a._SpecColor != null && b._SpecColor != null)
                material.SetColor("_SpecColor", Utility.Lerp(a._Color, b._SpecColor, t));
            if (a._Shininess != null && b._Shininess != null)
                material.SetFloat("_Shininess", Utility.Lerp(a._Shininess, b._Shininess, t));
            if (!string.IsNullOrEmpty(a._MainTex) && !string.IsNullOrEmpty(b._MainTex))
                renderer.LerpTexture("_MainTex", a._MainTex, b._MainTex, t);
            if (!string.IsNullOrEmpty(a._BumpMap) && !string.IsNullOrEmpty(b._BumpMap))
                renderer.LerpTexture("_BumpMap", a._BumpMap, b._BumpMap, t);
            if (a._rimPower != null && b._rimPower != null)
                material.SetFloat("_rimPower", Utility.Lerp(a._rimPower, b._rimPower, t));
            if (a._rimBlend != null && b._rimBlend != null)
                material.SetFloat("_RimPower", Utility.Lerp(a._rimBlend, b._rimBlend, t));
            if (!string.IsNullOrEmpty(a._rimColorRamp) && !string.IsNullOrEmpty(b._rimColorRamp))
                renderer.LerpTexture("_rimColorRamp", a._rimColorRamp, b._rimColorRamp, t);
        }

        internal static void Lerp(this Renderer renderer, EmissiveMultiRampSunspotsContainer a, EmissiveMultiRampSunspotsContainer b, double t)
        {
            Material material = renderer?.material;

            if (a._RampMap != null && b._RampMap != null)
                renderer.LerpTexture("_RampMap", a._RampMap, b._RampMap, t);
            if (a._NoiseMap != null && b._NoiseMap != null)
                renderer.LerpTexture("_NoiseMap", a._NoiseMap, b._NoiseMap, t);
            if (a._EmitColor0 != null && b._EmitColor0 != null)
                material.SetColor("_EmitColor0", Utility.Lerp(a._EmitColor0, b._EmitColor0, t));
            if (a._EmitColor1 != null && b._EmitColor1 != null)
                material.SetColor("_EmitColor1", Utility.Lerp(a._EmitColor1, b._EmitColor1, t));
            if (a._SunspotTex != null && b._SunspotTex != null)
                renderer.LerpTexture("_SunspotTex", a._SunspotTex, b._SunspotTex, t);
            if (a._SunspotPower != null && b._SunspotPower != null)
                material.SetFloat("_SunspotPower", Utility.Lerp(a._SunspotPower, b._SunspotPower, t));
            if (a._SunspotColor != null && b._SunspotColor != null)
                material.SetColor("_SunspotColor", Utility.Lerp(a._SunspotColor, b._SunspotColor, t));
            if (a._RimColor != null && b._RimColor != null)
                material.SetColor("_RimColor", Utility.Lerp(a._RimColor, b._RimColor, t));
            if (a._RimPower != null && b._RimPower != null)
                material.SetFloat("_RimPower", Utility.Lerp(a._RimPower, b._RimPower, t));
            if (a._RimBlend != null && b._RimBlend != null)
                material.SetFloat("_RimBlend", Utility.Lerp(a._RimBlend, b._RimBlend, t));
        }

        // Evaluate from LerpTextures
        internal static void LerpTexture(this Renderer renderer, string name, string name_a, string name_b, double t)
        {
            Material material = renderer?.material;

            Texture old = material.GetTexture(name);
            string old_a = old.name;
            string old_b = old.name;

            if (old.GetType() == typeof(RenderTexture))
            {
                string[] split = old_a.Split(';');

                if (split.Length == 2)
                {
                    old_a = split[0];
                    old_b = split[1];
                }
            }

            if (old_a != name_a && old_a != name_b)
            {
                Utility.UnloadTexture(old_a);
            }

            if (old_b != old_a && old_b != name_a && old_b != name_b)
            {
                Utility.UnloadTexture(old_b);
            }

            Texture a = Utility.LoadTexture(name_a);
            Texture b = Utility.LoadTexture(name_b);

            renderer.LerpTexture(name, a, b, t);
        }

        internal static void LerpTexture(this Renderer renderer, string name, Texture a, Texture b, double t)
        {
            Material material = renderer?.material;

            if (a != null && b != null && a.width == b.width && a.height == b.height)
            {
                RenderTexture RT;
                Texture texture = material.GetTexture(name);

                if (texture.GetType() == typeof(RenderTexture))
                {
                    RT = (RenderTexture)texture;

                    if (RT.width != a.width || RT.height != a.height)
                        return;
                }
                else
                {
                    RT = new RenderTexture(a.width, a.height, 0);
                    RT.name = a.name + ";" + b.name;
                    material.SetTexture(name, RT);

                    if (name == "_MainTex" || name == "_BumpMap")
                    {
                        Utility.UnloadTexture(texture);
                    }

                    ScaledSpaceOnDemand OD = renderer.GetComponent<ScaledSpaceOnDemand>();
                    if (OD != null)
                    {
                        if (name == "_MainTex")
                        {
                            Utility.UnloadTexture(OD.texture);
                            OD.texture = "";
                        }

                        if (name == "_BumpMap")
                        {
                            Utility.UnloadTexture(OD.normals);
                            OD.normals = "";
                        }
                    }
                }

                Utility.material.SetFloat("_Blend", (float)t);
                Utility.material.SetTexture("_SecondTex", b);

                Graphics.Blit(a, RT, Utility.material);
            }
        }
    }
}
