using System;
using System.Collections.Generic;
using UnityEngine;
using Kopernicus.OnDemand;


namespace SigmaSeasonalizerPlugin
{
    internal class ScaledSeasons : MonoBehaviour
    {
        bool active = false;
        bool unload = false;
        double unloadIn = 0;

        internal string shader
        {
            get { return GetComponent<Renderer>()?.material?.shader?.name; }
        }
        internal CelestialBody body = null;
        internal SortedDictionary<double, MaterialContainer> materials = new SortedDictionary<double, MaterialContainer>();

        void Update()
        {
            if (active)
            {
                GetComponent<Renderer>()?.Evaluate(materials, (body.orbit.meanAnomaly + Math.PI * 2) % (Math.PI * 2));
            }

            if (unload)
            {
                unloadIn = unloadIn - Time.deltaTime;

                if (unloadIn < 0)
                {
                    unloadIn = 0;
                    unload = false;

                    Utility.UnloadTexture(GetComponent<Renderer>()?.material, "_MainTex");
                    Utility.UnloadTexture(GetComponent<Renderer>()?.material, "_BumpMap");
                }
            }
        }

        void OnBecameVisible()
        {
            active = true;
            unload = false;
        }

        void OnBecameInvisible()
        {
            active = false;
            unload = true;
            unloadIn = OnDemandStorage.onDemandUnloadDelay;
        }
    }
}
