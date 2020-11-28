using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class SoundFxManager : MonoBehaviour
    {
        [SerializeField] private AudioSource m_sourcePrefab = null;
        [SerializeField] private AudioClip m_warningFx = null;
        [SerializeField] private float m_warningDesyncThreshold = 0.7f;

        private AudioSource m_warningSource = null;

        private void Update()
        {
            var desyncManager = GameComponentsLocator.Get<DesynchronizationManager>();
            var playWarning = desyncManager.desynchronization > m_warningDesyncThreshold;
            UpdateWarning(playWarning);
        }

        private void UpdateWarning(bool play)
        {
            if (m_warningSource == null)
                m_warningSource = Instantiate(m_sourcePrefab, transform.position, transform.rotation, transform);

            if (play && !m_warningSource.isPlaying)
                StartWarning();
            if (!play && m_warningSource.isPlaying)
                StopWarning();
        }

        private void StartWarning()
        {
            m_warningSource.clip = m_warningFx;
            m_warningSource.loop = true;
            m_warningSource.Play();
        }

        private void StopWarning()
        {
            m_warningSource.Stop();
        }
    }
}
