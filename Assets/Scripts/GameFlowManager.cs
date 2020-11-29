using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace UnityPrototype
{
    public class GameFlowManager : MonoBehaviour
    {
        [SerializeField] private float m_singularityDuration = 1.0f;
        [SerializeField] private float m_scaleDuration = 1.0f;

        [ShowNativeProperty] public float singularityProgress { get; private set; } = 0.0f;
        [ShowNativeProperty] public float scale { get; private set; } = 0.0f;

        private enum State
        {
            GameplayLoop,
            Desynchronized,
        }

        private State m_state = State.GameplayLoop;

        private void Update()
        {
            var desyncManager = GameComponentsLocator.Get<DesynchronizationManager>();

            if (m_state != State.Desynchronized && desyncManager.desynchronized)
            {
                m_state = State.Desynchronized;
                OnDesync();
            }
        }

        [Button("Desync")]
        private void OnDesync()
        {
            StartCoroutine(CollapseToSingularity());
        }

        private IEnumerator CollapseToSingularity()
        {
            singularityProgress = 0.0f;
            scale = 0.0f;

            var singularityTime = 0.0f;
            while (singularityTime < m_singularityDuration)
            {
                singularityProgress = Mathf.InverseLerp(0.0f, m_singularityDuration, singularityTime);

                singularityTime += Time.unscaledDeltaTime;
                yield return null;
            }

            var scaleTime = 0.0f;
            while (scaleTime < m_scaleDuration)
            {
                scale = Mathf.InverseLerp(0.0f, m_scaleDuration, scaleTime);

                scaleTime += Time.unscaledDeltaTime;
                yield return null;
            }
        }
    }
}
