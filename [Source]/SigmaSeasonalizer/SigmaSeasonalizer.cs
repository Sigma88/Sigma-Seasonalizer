using UnityEngine;


namespace SigmaSeasonalizerPlugin
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class SigmaSeasonalizer : MonoBehaviour
    {
        void Start()
        {
            int n = FlightGlobals.Bodies.Count;

            for (int i = 0; i < n; i++)
            {
                CelestialBody body = FlightGlobals.Bodies[i];

                if (SigmaSeasonalizerLoader.scaledSeasonsDictionary.TryGetValue(body?.transform?.name, out ConfigNode[] nodes))
                {
                    ScaledSeasons seasons = body.scaledBody.AddOrGetComponent<ScaledSeasons>();
                    seasons.body = body;

                    foreach (var item in nodes)
                    {
                        if (double.TryParse(item.GetValue("meanAnomaly"), out double meanAnomaly))
                        {
                            MaterialContainer material = MaterialContainer.Load(seasons.shader, item);

                            if (material != null)
                                seasons.materials.Add(meanAnomaly, material);
                        }
                    }
                }
            }
        }
    }
}
