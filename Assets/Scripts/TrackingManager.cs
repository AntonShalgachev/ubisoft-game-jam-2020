using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace UnityPrototype
{
    public class TrackingManager : MonoBehaviour
    {
        [SerializeField] private Region m_trackingRegion = null;
        [SerializeField] private SimulatedObject m_player = null;
        [SerializeField] private GameObject m_overviewCamera = null;

        public enum State
        {
            Start,
            EnteredGravityZone,
            LeftGravityZone,
        }

        public State state { get; private set; } = State.Start;
        [ShowNativeProperty] private string m_stateString => state.ToString();

        private void Update()
        {
            var insideRegion = m_trackingRegion.IsInside(m_player.position);
            m_overviewCamera.SetActive(!insideRegion);

            switch (state)
            {
                case State.Start:
                    if (insideRegion)
                        state = State.EnteredGravityZone;
                    break;
                case State.EnteredGravityZone:
                    if (!insideRegion)
                        state = State.LeftGravityZone; // bug, but who cares?
                    break;
                case State.LeftGravityZone:
                    break;
            }
        }
    }
}
