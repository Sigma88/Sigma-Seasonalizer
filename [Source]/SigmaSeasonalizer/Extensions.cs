using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


namespace SigmaSeasonalizerPlugin
{
    internal static class Extensions
    {
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

        internal static void Evaluate(this Material material, SortedDictionary<double, Material> dictionary, double key)
        {
            int? n = dictionary?.Count;

            if (material != null && n > 0)
            {
                if (n > 1)
                {
                    KeyValuePair<double, Material> b;
                    KeyValuePair<double, Material> a;

                    for (int i = 0; i <= n; i++)
                    {
                        b = dictionary.At(i);

                        if (b.Key > key)
                        {
                            a = dictionary.At(i - 1);

                            double t = (key - a.Key) / (b.Key - a.Key);

                            return;
                        }
                    }
                }
            }
        }

        internal static void Lerp(this Material material, Material a, Material b, double t)
        {
            if (material.shader == a.shader && a.shader == b.shader)
            {
                if (material.shader.name == "Terrain/Scaled Planet (Simple)")
                {
                    material.SetColor("_Color", Utility.Lerp(a.GetColor("_Color"), b.GetColor("_Color"), t));
                    material.SetColor("_SpecColor", Utility.Lerp(a.GetColor("_SpecColor"), b.GetColor("_SpecColor"), t));
                    material.SetFloat("_Shininess", Utility.Lerp(a.GetFloat("_Shininess"), b.GetFloat("_Shininess"), t));
                    material.LerpTexture("_MainTex", a.GetTexture("_MainTex"), b.GetTexture("_MainTex"), t);
                    material.LerpTexture("_BumpMap", a.GetTexture("_BumpMap"), b.GetTexture("_BumpMap"), t);
                    material.SetFloat("_Opacity", Utility.Lerp(a.GetFloat("_Opacity"), b.GetFloat("_Opacity"), t));
                    material.LerpTexture("_ResourceMap", a.GetTexture("_ResourceMap"), b.GetTexture("_ResourceMap"), t);
                }

                else

                if (material.shader.name == "Terrain/Scaled Planet (RimLight)")
                {
                    material.SetColor("_Color", Utility.Lerp(a.GetColor("_Color"), b.GetColor("_Color"), t));
                    material.SetColor("_SpecColor", Utility.Lerp(a.GetColor("_SpecColor"), b.GetColor("_SpecColor"), t));
                    material.SetFloat("_Shininess", Utility.Lerp(a.GetFloat("_Shininess"), b.GetFloat("_Shininess"), t));
                    material.LerpTexture("_MainTex", a.GetTexture("_MainTex"), b.GetTexture("_MainTex"), t);
                    material.LerpTexture("_BumpMap", a.GetTexture("_BumpMap"), b.GetTexture("_BumpMap"), t);
                    material.SetColor("_RimColor", Utility.Lerp(a.GetColor("_RimColor"), b.GetColor("_RimColor"), t));
                    material.SetFloat("_RimPower", Utility.Lerp(a.GetFloat("_RimPower"), b.GetFloat("_RimPower"), t));
                    material.LerpTexture("_ResourceMap", a.GetTexture("_ResourceMap"), b.GetTexture("_ResourceMap"), t);
                }

                else

                if (material.shader.name == "Terrain/Scaled Planet (RimAerial)")
                {
                    material.SetColor("_Color", Utility.Lerp(a.GetColor("_Color"), b.GetColor("_Color"), t));
                    material.SetColor("_SpecColor", Utility.Lerp(a.GetColor("_SpecColor"), b.GetColor("_SpecColor"), t));
                    material.SetFloat("_Shininess", Utility.Lerp(a.GetFloat("_Shininess"), b.GetFloat("_Shininess"), t));
                    material.LerpTexture("_MainTex", a.GetTexture("_MainTex"), b.GetTexture("_MainTex"), t);
                    material.LerpTexture("_BumpMap", a.GetTexture("_BumpMap"), b.GetTexture("_BumpMap"), t);
                    material.SetFloat("_rimPower", Utility.Lerp(a.GetFloat("_rimPower"), b.GetFloat("_rimPower"), t));
                    material.SetFloat("_rimBlend", Utility.Lerp(a.GetFloat("_rimBlend"), b.GetFloat("_rimBlend"), t));
                    material.LerpTexture("_rimColorRamp", a.GetTexture("_rimColorRamp"), b.GetTexture("_rimColorRamp"), t);
                    material.LerpTexture("_ResourceMap", a.GetTexture("_ResourceMap"), b.GetTexture("_ResourceMap"), t);
                }

                else

                if (material.shader.name == "Terrain/Scaled Planet (RimAerial)")
                {
                    material.LerpTexture("_RampMap", a.GetTexture("_RampMap"), b.GetTexture("_RampMap"), t);
                    material.LerpTexture("_NoiseMap", a.GetTexture("_NoiseMap"), b.GetTexture("_NoiseMap"), t);
                    material.SetColor("_EmitColor0", Utility.Lerp(a.GetColor("_EmitColor0"), b.GetColor("_EmitColor0"), t));
                    material.SetColor("_EmitColor1", Utility.Lerp(a.GetColor("_EmitColor1"), b.GetColor("_EmitColor1"), t));
                    material.LerpTexture("_SunspotTex", a.GetTexture("_SunspotTex"), b.GetTexture("_SunspotTex"), t);
                    material.SetFloat("_SunspotPower", Utility.Lerp(a.GetFloat("_SunspotPower"), b.GetFloat("_SunspotPower"), t));
                    material.SetColor("_SunspotColor", Utility.Lerp(a.GetColor("_SunspotColor"), b.GetColor("_SunspotColor"), t));
                    material.SetColor("_RimColor", Utility.Lerp(a.GetColor("_RimColor"), b.GetColor("_RimColor"), t));
                    material.SetFloat("_RimPower", Utility.Lerp(a.GetFloat("_RimPower"), b.GetFloat("_RimPower"), t));
                    material.SetFloat("_RimBlend", Utility.Lerp(a.GetFloat("_RimBlend"), b.GetFloat("_RimBlend"), t));
                }
            }
        }

        internal static void LerpTexture(this Material material, string name, Texture a, Texture b, double t)
        {
            RenderTexture RT;
            Texture texture = material.GetTexture(name);

            if (texture.GetType() == typeof(RenderTexture))
            {
                RT = (RenderTexture)texture;
            }
            else
            {
                RT = new RenderTexture(a.width, a.height, 0);
                RT.name = "LERP: [" + a.name + "] >>> [" + b.name + "]";
                material.SetTexture(name, RT);
            }

            Utility.material.SetFloat("_Blend", (float)t);
            Utility.material.SetTexture("_SecondTex", b);

            Graphics.Blit(a, RT, Utility.material);
        }
    }
}
