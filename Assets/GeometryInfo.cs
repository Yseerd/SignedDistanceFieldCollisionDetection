using UnityEngine;

public class GeometryInfo : MonoBehaviour
{
    private Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        
        // 获取顶点信息
        Vector3[] vertices = mesh.vertices;
        
        // 获取三角形面信息
        int[] triangles = mesh.triangles;
        
        // 处理边缘、面信息
        // 此处我们可以进一步处理这些数据，用于后续SDF计算
    }
}
