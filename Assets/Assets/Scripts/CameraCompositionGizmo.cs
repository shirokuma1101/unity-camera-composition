using UnityEngine;

public class CameraCompositionGizmo : CameraCompositionBase
{
    [SerializeField]
    private bool drawGizmos = true;
    [SerializeField]
    private bool drawGizmosSelected = true;


    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        DrawCameraComposition();
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmosSelected) return;

        DrawCameraComposition();
    }

    protected override void DrawLines(CameraComposition.Line[] lines)
    {
        Gizmos.color = LineColor;

        foreach (var line in lines)
        {
            Gizmos.DrawLine(line.start, line.end);
        }
    }
}
