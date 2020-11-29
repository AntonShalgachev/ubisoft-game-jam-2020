using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace UnityPrototype
{
    [CreateAssetMenu(fileName = "PlanetContainer", menuName = "Game/CelestialBodyContainer")]
    public class CelestialBodyContainer : ScriptableObject
    {
        private List<CelestialBody> m_bodies = new List<CelestialBody>();
        public IEnumerable<CelestialBody> bodies => m_bodies;

        public void RegisterBody(CelestialBody body)
        {
            Debug.Log($"Registered body {body.name}");
            m_bodies.Add(body);
        }

        public void UnregisterBody(CelestialBody body)
        {
            m_bodies.Remove(body);
        }
    }
}
