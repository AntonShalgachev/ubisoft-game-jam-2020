using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class DesyncPitch : MonoBehaviour
    {
        [SerializeField] private float m_minPitch = 0.5f;

        private void Update()
        {
            var flowManager = GameComponentsLocator.Get<GameFlowManager>();

            var source = GetComponent<AudioSource>();
            source.pitch = Mathf.Lerp(1.0f, m_minPitch, flowManager.slowdownRatio);
        }
    }
}
