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
        [SerializeField] private float m_slowdownDuration = 1.0f;
        [SerializeField] private float m_collapseStartThreshold = 1.0f;
        [SerializeField] private float m_farewellDelay = 1.0f;

        [ShowNativeProperty] public float singularityProgress { get; private set; } = 0.0f;
        [ShowNativeProperty] public float scale { get; private set; } = 0.0f;

        private float m_slowdownRatio = 0.0f;
        public float slowdownRatio => m_slowdownRatio;

        public enum State
        {
            Intro,
            GameplayLoop,
            Desynchronized,
            Success,
            Farewell,
            PostFarewell,
        }

        public State state { get; private set; } = State.Intro;
        [ShowNativeProperty] private string m_stateString => state.ToString();

        private void Update()
        {
            var isGameplay = state == State.Intro || state == State.Farewell;
            Time.timeScale = isGameplay ? 0.0f : 1.0f;

            var desyncManager = GameComponentsLocator.Get<DesynchronizationManager>();
            var trackingManager = GameComponentsLocator.Get<TrackingManager>();

            if (state == State.GameplayLoop && desyncManager.desynchronized)
            {
                state = State.Desynchronized;
                OnDesync();
            }

            if (state == State.GameplayLoop && trackingManager.state == TrackingManager.State.LeftGravityZone)
            {
                state = State.Success;
                OnSuccess();
            }
        }

        public void OnIntroComplete()
        {
            state = State.GameplayLoop;
        }

        public void OnFarewellCompleted()
        {
            Debug.Log("Farewell done");
        }

        [Button("Desync")]
        private void OnDesync()
        {
            Debug.Log("Desync");
            StartCoroutine(Slowdown());
            StartCoroutine(CollapseToSingularity());
        }

        [Button("Success")]
        private void OnSuccess()
        {
            Debug.Log("Success");
            StartCoroutine(ShowFarewell());
        }

        private IEnumerator ShowFarewell()
        {
            yield return new WaitForSecondsRealtime(m_farewellDelay);
            state = State.Farewell;

            var successDialogues = GameComponentsLocator.Get<SuccessDialogues>();
            successDialogues.ShowFarewell();
        }

        private IEnumerator CollapseToSingularity()
        {
            singularityProgress = 0.0f;
            scale = 0.0f;

            var singularityTime = 0.0f;
            while (singularityTime < m_singularityDuration)
            {
                singularityProgress = Mathf.InverseLerp(0.0f, m_singularityDuration, singularityTime);

                if (m_slowdownRatio >= m_collapseStartThreshold)
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

        private IEnumerator Slowdown()
        {
            float initialTimescale = Time.timeScale;
            float time = 0.0f;

            while (time < m_slowdownDuration)
            {
                m_slowdownRatio = Mathf.InverseLerp(0.0f, m_slowdownDuration, time);
                Time.timeScale = Mathf.Lerp(initialTimescale, 0.0f, m_slowdownRatio);

                time += Time.unscaledDeltaTime;
                yield return null;
            }
        }
    }
}
