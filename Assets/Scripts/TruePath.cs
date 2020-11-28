using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace UnityPrototype
{
    [CreateAssetMenu(fileName = "TruePath", menuName = "Game/TruePath")]
    public class TruePath : ScriptableObject
    {
        public struct PathPoint
        {
            public PathPoint(Vector2 position, Vector2 vecloity, float time)
            {
                this.position = position;
                this.velocity = vecloity;
                this.time = time;
            }

            public Vector2 position;
            public Vector2 velocity;
            public float time;
        }

        private List<PathPoint> m_points = new List<PathPoint>();

        public void AddPoint(SimulatedObject target, float time)
        {
            m_points.Add(new PathPoint(target.position, target.velocity, time));
        }

        public void Clear()
        {
            m_points.Clear();
        }

        public PathPoint GetPoint(float time)
        {
            Debug.Assert(m_points.Count > 0);

            var prevPoint = m_points[0];

            for (var i = 1; i < m_points.Count; i++)
            {
                var point = m_points[i];

                if (time >= prevPoint.time && time < point.time)
                {
                    var t = Mathf.InverseLerp(prevPoint.time, point.time, time);

                    PathPoint lerpedPoint;
                    lerpedPoint.time = time;
                    lerpedPoint.position = Vector2.Lerp(prevPoint.position, point.position, t);
                    lerpedPoint.velocity = Vector2.Lerp(prevPoint.velocity, point.velocity, t);

                    return lerpedPoint;
                }

                prevPoint = point;
            }

            return prevPoint;
        }

        // public Vector2 GetPosition(float time)
        // {

        // }

        // public Vector2 GetTangent(float time)
        // {

        // }

        public void DrawGizmos()
        {
            GizmosHelper.DrawCurve(m_points, (PathPoint point) => point.position);
        }
    }
}
