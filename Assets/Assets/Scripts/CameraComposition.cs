using UnityEngine;

public class CameraComposition
{
    public enum Mode
    {
        Grid,        // グリッド
        VerticallySymmetrical,   // 縦方向に対称
        HorizontallySymmetrical, // 横方向に対称
        Bisection,   // 二分割
        Thirds,      // 三分割

        Diagonal,        // 対角線
    }

    public struct Line
    {
        public Vector3 start;
        public Vector3 end;
    }

    public static Line[] Grid(Camera camera, int x, int y)
    {
        Line[] lines = new Line[x + y];

        for (int i = 0; i < x; i++)
        {
            lines[i] = new Line()
            {
                start = camera.ViewportToWorldPoint(new Vector3(1.0f / (x + 1) * (i + 1), 0, camera.nearClipPlane)),
                end   = camera.ViewportToWorldPoint(new Vector3(1.0f / (x + 1) * (i + 1), 1, camera.nearClipPlane))
            };
        }

        for (int i = 0; i < y; i++)
        {
            lines[x + i] = new Line()
            {
                start = camera.ViewportToWorldPoint(new Vector3(0, 1.0f / (y + 1) * (i + 1), camera.nearClipPlane)),
                end   = camera.ViewportToWorldPoint(new Vector3(1, 1.0f / (y + 1) * (i + 1), camera.nearClipPlane))
            };
        }

        return lines;
    }

    public static Line[] VerticallySymmetrical(Camera camera)
    {
        return Grid(camera, 0, 1);
    }

    public static Line[] HorizontallySymmetrical(Camera camera)
    {
        return Grid(camera, 1, 0);
    }

    public static Line[] Bisection(Camera camera)
    {
        return Grid(camera, 1, 1);
    }

    public static Line[] Thirds(Camera camera)
    {
        return Grid(camera, 2, 2);
    }

    public static Line[] Diagonal(Camera camera)
    {
        return new Line[]
        {
            new Line()
            {
                start = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane)),
                end   = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane))
            },
            new Line()
            {
                start = camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane)),
                end   = camera.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane))
            }
        };
    }

}
