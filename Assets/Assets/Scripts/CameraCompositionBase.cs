using UnityEngine;

public abstract class CameraCompositionBase : MonoBehaviour
{
    [field: SerializeField]
    public CameraComposition.Mode Mode { get; set; }
    [field: SerializeField]
    public Camera PhotoCamera { get; private set; }
    [field: SerializeField]
    public Color LineColor { get; private set; } = Color.white;
    [field: SerializeField]
    public Vector2Int GridSize { get; private set; } = new(3, 3);

    private CameraComposition.Mode beforeMode;
    private Vector2Int beforeGridSize;


    protected abstract void DrawLines(CameraComposition.Line[] lines);

    protected virtual void OnValueChanged(int lines) { }

    protected void Update()
    {
        if (beforeMode != Mode || beforeGridSize != GridSize)
        {
            OnValueChanged(GetLinesCount());
            beforeMode = Mode;
            beforeGridSize = GridSize;
        }
    }

    protected void DrawCameraComposition()
    {
        if (PhotoCamera == null) return;

        switch (Mode)
        {
            case CameraComposition.Mode.Grid:
                DrawLines(CameraComposition.Grid(PhotoCamera, GridSize.x, GridSize.y));
                break;

            case CameraComposition.Mode.VerticallySymmetrical:
                DrawLines(CameraComposition.VerticallySymmetrical(PhotoCamera));
                break;

            case CameraComposition.Mode.HorizontallySymmetrical:
                DrawLines(CameraComposition.HorizontallySymmetrical(PhotoCamera));
                break;

            case CameraComposition.Mode.Bisection:
                DrawLines(CameraComposition.Bisection(PhotoCamera));
                break;

            case CameraComposition.Mode.Thirds:
                DrawLines(CameraComposition.Thirds(PhotoCamera));
                break;

            case CameraComposition.Mode.Diagonal:
                DrawLines(CameraComposition.Diagonal(PhotoCamera));
                break;

        }
    }

    protected int GetLinesCount()
    {
        return Mode switch
        {
            CameraComposition.Mode.Grid                    => CameraComposition.Grid(PhotoCamera, GridSize.x, GridSize.y).Length,
            CameraComposition.Mode.VerticallySymmetrical   => CameraComposition.VerticallySymmetrical(PhotoCamera).Length,
            CameraComposition.Mode.HorizontallySymmetrical => CameraComposition.HorizontallySymmetrical(PhotoCamera).Length,
            CameraComposition.Mode.Bisection               => CameraComposition.Bisection(PhotoCamera).Length,
            CameraComposition.Mode.Thirds                  => CameraComposition.Thirds(PhotoCamera).Length,
            CameraComposition.Mode.Diagonal                => CameraComposition.Diagonal(PhotoCamera).Length,
            _ => 0,
        };
    }

}
