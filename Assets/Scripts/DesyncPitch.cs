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
            var desyncManager = GameComponentsLocator.Get<DesynchronizationManager>();

            if (desyncManager.desynchronized)
            {
                var source = GetComponent<AudioSource>();
                source.pitch = Mathf.Lerp(1.0f, m_minPitch, desyncManager.slowdownRatio);
            }
        }
    }
}
