using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace UnityPrototype
{
    [CreateAssetMenu(fileName = "TruePath", menuName = "Game/TruePath")]
    public class TruePath : ScriptableObject
    {
        public List<Vector2> points = new List<Vector2>();

        public void AddPoint(Vector2 point)
        {
            points.Add(point);
        }

        public void Clear()
        {
            points.Clear();
        }

        public void DrawGizmos()
        {
            GizmosHelper.DrawCurve(points);
        }
    }
}
