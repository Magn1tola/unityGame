using UnityEngine;

internal class Debug : UnityEngine.Debug
{
    public static void DrawCircle(Vector3 position, float radius, int segments, Color color, float duration)
    {
        if (radius <= 0.0f || segments <= 0) return;

        var angleStep = 360.0f / segments;
        angleStep *= Mathf.Deg2Rad;

        var lineStart = Vector3.zero;
        var lineEnd = Vector3.zero;

        for (var i = 0; i < segments; i++)
        {
            lineStart.x = Mathf.Cos(angleStep * i);
            lineStart.y = Mathf.Sin(angleStep * i);

            lineEnd.x = Mathf.Cos(angleStep * (i + 1));
            lineEnd.y = Mathf.Sin(angleStep * (i + 1));

            lineStart *= radius;
            lineEnd *= radius;

            lineStart += position;
            lineEnd += position;

            DrawLine(lineStart, lineEnd, color, duration);
        }
    }

    public static void DrawRectangle(Vector3 position, Vector2 size, Color color, float duration)
    {
        var rightOffset = Vector3.right * size.x * 0.5f;
        var upOffset = Vector3.up * size.y * 0.5f;

        var offsetA = rightOffset + upOffset;
        var offsetB = -rightOffset + upOffset;
        var offsetC = -rightOffset - upOffset;
        var offsetD = rightOffset - upOffset;

        DrawLine(position + offsetA, position + offsetB, color, duration);
        DrawLine(position + offsetB, position + offsetC, color, duration);
        DrawLine(position + offsetC, position + offsetD, color, duration);
        DrawLine(position + offsetD, position + offsetA, color, duration);
    }
}