using UnityEngine;
public class SDFCalculator : MonoBehaviour
{
    public Vector3 gridSize = new Vector3(10, 10, 10);
    public float cellSize = 0.5f;
    private float[,,] sdfGrid;

    void Start()
    {
        // 初始化SDF网格
        sdfGrid = new float[(int)gridSize.x, (int)gridSize.y, (int)gridSize.z];

        // 计算每个网格点的SDF值
        CalculateSDF();
    }

   public void CalculateSDF()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    Vector3 point = new Vector3(x * cellSize, y * cellSize, z * cellSize);
                    sdfGrid[x, y, z] = CalculateDistanceToGeometry(point);
                }
            }
        }
    }
   public float GetSDFValue(Vector3 worldPosition)
   {
       // 将世界坐标转换为相对于SDFCalculator的局部坐标
       Vector3 localPosition = worldPosition - transform.position;

       // 计算局部坐标在网格中的索引
       int x = Mathf.Clamp((int)(localPosition.x / cellSize), 0, (int)gridSize.x - 1);
       int y = Mathf.Clamp((int)(localPosition.y / cellSize), 0, (int)gridSize.y - 1);
       int z = Mathf.Clamp((int)(localPosition.z / cellSize), 0, (int)gridSize.z - 1);

       // 输出调试信息
       Debug.Log($"Local Position: {localPosition}, Grid Position: ({x}, {y}, {z}), SDF Value: {sdfGrid[x, y, z]}");

       // 返回相应的SDF值
       return sdfGrid[x, y, z];
   }
   public  float CalculateDistanceToGeometry(Vector3 point)
    {
        float minDistance = float.MaxValue;

        // 遍历场景中的所有几何体
        foreach (var geometry in FindObjectsOfType<GeometryInfo>())
        {
            // 计算点到几何体表面的最短距离
            foreach (var vertex in geometry.GetComponent<MeshFilter>().mesh.vertices)
            {
                float distance = Vector3.Distance(point, vertex);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
        }
        
        return minDistance;
    }
}