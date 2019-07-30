using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class SlicedMesh : MonoBehaviour
{
    private float border = 0.1f;
    public float Border
    {
        get
        {
            return border;
        }
        set
        {
            border = value;
            CreateSlicedMesh();
        }
    }

    private float width = 1.0f;
    public float Width
    {
        get
        {
            return width;
        }
        set
        {
            width = value;
            CreateSlicedMesh();
        }
    }

    private float height = 1.0f;
    public float Height
    {
        get
        {
            return height;
        }
        set
        {
            height = value;
            CreateSlicedMesh();
        }
    }

    private float margin = 0.4f;
    public float Margin
    {
        get
        {
            return margin;
        }
        set
        {
            margin = value;
            CreateSlicedMesh();
        }
    }

    void Start()
    {
        CreateSlicedMesh();
    }

    void CreateSlicedMesh()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = new Vector3[] {
            new Vector3(0, 0, 0),
            new Vector3(border, 0, 0),
            new Vector3(width-border, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, border, 0),
            new Vector3(border, border, 0),
            new Vector3(width-border, border, 0),
            new Vector3(width, border, 0),
            new Vector3(0, height-border, 0),
            new Vector3(border, height-border, 0),
            new Vector3(width-border, height-border, 0),
            new Vector3(width, height-border, 0),
            new Vector3(0, height, 0),
            new Vector3(border, height, 0),
            new Vector3(width-border, height, 0),
            new Vector3(width, height, 0)
        };

        mesh.uv = new Vector2[] {
            new Vector2(0, 0),
            new Vector2(margin, 0),
            new Vector2(1-margin, 0),
            new Vector2(1, 0),
            new Vector2(0, margin),
            new Vector2(margin, margin),
            new Vector2(1-margin, margin),
            new Vector2(1, margin),
            new Vector2(0, 1-margin),
            new Vector2(margin, 1-margin),
            new Vector2(1-margin, 1-margin),
            new Vector2(1, 1-margin),
            new Vector2(0, 1),
            new Vector2(margin, 1),
            new Vector2(1-margin, 1),
            new Vector2(1, 1)
        };

        mesh.triangles = new int[] {
            0, 4, 5,
            0, 5, 1,
            1, 5, 6,
            1, 6, 2,
            2, 6, 7,
            2, 7, 3,
            4, 8, 9,
            4, 9, 5,
            5, 9, 10,
            5, 10, 6,
            6, 10, 11,
            6, 11, 7,
            8, 12, 13,
            8, 13, 9,
            9, 13, 14,
            9, 14, 10,
            10, 14, 15,
            10, 15, 11
        };

        mesh.RecalculateBounds();
    }
}
