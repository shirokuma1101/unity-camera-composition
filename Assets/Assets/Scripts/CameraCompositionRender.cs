using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraCompositionRender : CameraCompositionBase
{
    private readonly List<LineRenderer> lineRendererList = new();


    private void Start()
    {
        OnValueChanged(GetLinesCount());
    }

    private new void Update()
    {
        base.Update();
        DrawCameraComposition();
    }

    protected override void DrawLines(CameraComposition.Line[] lines)
    {
        foreach (var (line, i) in lines.Select((line, i) => (line, i)))
        {
            lineRendererList[i].startColor = LineColor;
            lineRendererList[i].endColor = LineColor;

            lineRendererList[i].SetPosition(0, line.start);
            lineRendererList[i].SetPosition(1, line.end);
        }
    }

    protected override void OnValueChanged(int lines)
    {
        foreach (var lineRenderer in lineRendererList)
        {
            Destroy(lineRenderer.gameObject);
        }
        lineRendererList.Clear();

        for (var i = 0; i < lines; i++)
        {
            var lineRenderer = new GameObject($"Line {i}").AddComponent<LineRenderer>();
            lineRenderer.transform.SetParent(transform);
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRendererList.Add(lineRenderer);
        }
    }
}
