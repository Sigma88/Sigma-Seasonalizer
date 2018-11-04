using System.Collections.Generic;
using UnityEngine;


namespace SigmaSeasonalizerPlugin
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class AddSeasons : MonoBehaviour
    {
        void Start()
        {
            SortedDictionary<double, Material> materials = new SortedDictionary<double, Material>();
            int n = FlightGlobals.Bodies.Count;
            int i = 0;
            foreach (var item in FlightGlobals.Bodies)
            {
                materials.Add(Mathf.PI * 2 / n * i, Instantiate(item.scaledBody.GetComponent<Renderer>().material));
                i++;

                ScaledSeasons seasons = item.scaledBody.AddOrGetComponent<ScaledSeasons>();

                seasons.body = item;
                seasons.materials = materials;
            }
        }
    }

    internal class ScaledSeasons : MonoBehaviour
    {
        bool active = false;
        internal CelestialBody body = null;
        internal SortedDictionary<double, Material> materials = new SortedDictionary<double, Material>();

        void Update()
        {
            if (active)
            {
                GetComponent<Renderer>()?.material?.Evaluate(materials, (body.orbit.meanAnomaly + Mathf.PI * 2) % (Mathf.PI * 2));
            }
        }

        void OnBecameVisible()
        {
            active = true;
        }

        void OnBecameInvisible()
        {
            active = false;
        }
    }
}
