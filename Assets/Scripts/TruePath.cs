using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace UnityPrototype
{
    [CreateAssetMenu(fileName = "TruePath", menuName = "Game/TruePath")]
    public class TruePath : ScriptableObject
    {
        private struct PathPoint
        {
            public PathPoint(Vector2 position, float time)
            {
                this.position = position;
                this.time = time;
            }

            public Vector2 position;
            public float time;
        }

        private List<PathPoint> m_points = new List<PathPoint>();

        public void AddPoint(Vector2 position, float time)
        {
            m_points.Add(new PathPoint(position, time));
        }

        public void Clear()
        {
            m_points.Clear();
        }

        public Vector2 GetPosition(float time)
        {
            Debug.Assert(m_points.Count > 1);

            var prevPoint = m_points[0];

            for (var i = 1; i < m_points.Count; i++)
            {
                var point = m_points[i];

                if (time >= prevPoint.time && time < point.time)
                {
                    var t = Mathf.InverseLerp(prevPoint.time, point.time, time);
                    return Vector2.Lerp(prevPoint.position, point.position, t);
                }

                prevPoint = point;
            }

            return prevPoint.position;
        }

        public void DrawGizmos()
        {
            GizmosHelper.DrawCurve(m_points, (PathPoint point) => point.position);
        }
    }
}
