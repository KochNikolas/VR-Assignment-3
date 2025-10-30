using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MakeTriangle : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = new Mesh();

        // Define vertices (3 corners of the triangle)
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),   // bottom left
            new Vector3(1, 0, 0),   // bottom right
            new Vector3(0.5f, 1, 0) // top middle
        };

        // Define the single triangle (clockwise order)
        int[] triangles = new int[]
        {
            0, 1, 2
        };

        // Optional: UVs for texture mapping
        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0.5f, 1)
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
