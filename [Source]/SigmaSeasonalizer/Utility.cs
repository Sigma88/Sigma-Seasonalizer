using System;
using System.Linq;
using UnityEngine;
using Kopernicus.Components;
using Kopernicus.OnDemand;
using Object = UnityEngine.Object;


namespace SigmaSeasonalizerPlugin
{
    internal static class Utility
    {
        // Lerp
        internal static object Lerp(object a, object b, double t)
        {
            if (a.GetType() == typeof(double))
            {
                return Lerp((double)a, (double)b, t);
            }
            if (a.GetType() == typeof(float))
            {
                return Lerp((float)a, (float)b, t);
            }
            if (a.GetType() == typeof(Color))
            {
                return Lerp((Color)a, (Color)b, t);
            }

            return null;
        }

        internal static double Lerp(double a, double b, double t)
        {
            return a * (1 - t) + b * t;
        }

        internal static float Lerp(float a, float b, double t)
        {
            return (float)(a * (1 - t) + b * t);
        }

        internal static Color Lerp(Color a, Color b, double t)
        {
            Color c = new Color
            (
                (float)Math.Pow(Lerp(Math.Pow(a.r, 2), Math.Pow(b.r, 2), t), 0.5f),
                (float)Math.Pow(Lerp(Math.Pow(a.g, 2), Math.Pow(b.g, 2), t), 0.5f),
                (float)Math.Pow(Lerp(Math.Pow(a.b, 2), Math.Pow(b.b, 2), t), 0.5f),
                Lerp(a.a, b.a, t)
            );

            return c;
        }

        // Shaders
        static Shader _shader = null;

        internal static Shader shader
        {
            get
            {
                if (_shader == null)
                {
                    ShaderLoader.LoadAssetBundle("Sigma/Seasonalizer/Shaders/", "LerpTextures");
                    _shader = ShaderLoader.GetShader("Unlit/Dissolve2Tex");
                }

                return _shader;
            }
        }

        // Material
        static Material _material;

        internal static Material material
        {
            get
            {
                if (_material == null)
                {
                    _material = new Material(shader);
                }

                return _material;
            }
        }

        // Texture Management
        internal static Texture LoadTexture(string name)
        {
            Texture output = Resources.FindObjectsOfTypeAll<Texture>().FirstOrDefault(t => t.name == name);

            if (output == null && OnDemandStorage.TextureExists(name))
            {
                Debug.Log("Utility", "Loading Texture => " + name);
                output = OnDemandStorage.LoadTexture(name, false, true, true);
            }

            return output;
        }

        internal static void UnloadTexture(Material material, string name)
        {
            Texture texture = material.GetTexture(name);
            string name_a = texture.name;
            string name_b = texture.name;

            if (texture.GetType() == typeof(RenderTexture))
            {
                string[] split = name_a.Split(';');

                if (split.Length == 2)
                {
                    name_a = split[0];
                    name_b = split[1];
                    UnloadTexture(name_b);
                }
            }

            UnloadTexture(name_a);
        }

        internal static void UnloadTexture(string name)
        {
            UnloadTexture(Resources.FindObjectsOfTypeAll<Texture>().FirstOrDefault(t => t.name == name));
        }

        internal static void UnloadTexture(Texture texture)
        {
            if (OnDemandStorage.TextureExists(texture?.name))
            {
                Debug.Log("Utility", "Unloading Texture => " + texture?.name);
                Object.DestroyImmediate(texture);
            }
        }
    }
}
