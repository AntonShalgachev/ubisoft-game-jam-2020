using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class TrackingManager : MonoBehaviour
    {
        [SerializeField] private Region m_trackingRegion = null;
        [SerializeField] private SimulatedObject m_player = null;
        [SerializeField] private GameObject m_overviewCamera = null;

        private void Update()
        {
            var insideRegion = m_trackingRegion.IsInside(m_player.position);
            m_overviewCamera.SetActive(!insideRegion);
        }
    }
}
