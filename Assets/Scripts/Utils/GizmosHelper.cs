using UnityEngine;
using Gamelogic.Extensions;
using System.Collections.Generic;

namespace UnityPrototype
{
    public static class GizmosHelper
    {
        public static void DrawVector(Vector2 pos, Vector2 dir, float scale = 1.0f)
        {
            Gizmos.DrawLine(pos, pos + dir * scale);
        }

        public static void DrawCircle(Vector2 origin, float radius, int segments = 16)
        {
            DrawEllipse(origin, Vector2.one * radius, segments);
        }

        public static void DrawEllipse(Vector2 origin, Vector2 axesLength, int segments = 16)
        {
            segments = Mathf.Max(segments, 2);

            var prevDir = Vector2.up;
            var prevPoint = prevDir * axesLength;
            var deltaAngle = 360.0f / segments;

            for (var i = 0; i < segments; i++)
            {
                var dir = prevDir.Rotate(deltaAngle);
                var pos = dir * axesLength;

                Gizmos.DrawLine(origin + prevPoint, origin + pos);

                prevPoint = pos;
                prevDir = dir;
            }
        }

        public static void DrawCurve(IEnumerable<Vector2> points, float pointSize = -1.0f, bool wireframePoint = false)
        {
            DrawCurve(points, (Vector2 point) => point, pointSize, wireframePoint);
        }

        public static void DrawCurve<PointType>(IEnumerable<PointType> points, System.Func<PointType, Vector2> getPosition, float pointSize = -1.0f, bool wireframePoint = false)
        {
            Vector2? prevPosition = null;

            foreach (var point in points)
            {
                var position = getPosition(point);

                if (prevPosition.HasValue)
                    Gizmos.DrawLine(prevPosition.Value, position);

                if (pointSize > 0.0f)
                {
                    if (wireframePoint)
                        GizmosHelper.DrawCircle(position, pointSize);
                    else
                        Gizmos.DrawSphere(position, pointSize);
                }

                prevPosition = position;
            }
        }
    }
}
