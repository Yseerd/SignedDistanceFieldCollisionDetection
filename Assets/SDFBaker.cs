using UnityEngine;
using UnityEditor;

public class SDFBaker : MonoBehaviour
{
    public Vector3 gridSize = new Vector3(64, 64, 64); // SDF网格大小
    public float cellSize = 1.0f; // 每个单元格的大小
    public string savePath = "Assets/SDFTexture.asset"; // 保存纹理的路径

    void Start()
    {
        // 生成SDF纹理
        Texture3D sdfTexture = new Texture3D((int)gridSize.x, (int)gridSize.y, (int)gridSize.z, TextureFormat.RFloat, false);
        sdfTexture.wrapMode = TextureWrapMode.Clamp;
        sdfTexture.filterMode = FilterMode.Bilinear;

        // 计算并烘焙SDF数据到纹理
        BakeSDFTexture(sdfTexture);

        // 保存纹理到本地
        SaveTexture3D(sdfTexture, savePath);
    }

    void BakeSDFTexture(Texture3D sdfTexture)
    {
        float[,,] sdfValues = new float[(int)gridSize.x, (int)gridSize.y, (int)gridSize.z];
        
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    Vector3 point = new Vector3(x * cellSize, y * cellSize, z * cellSize);
                    sdfValues[x, y, z] = CalculateDistanceToGeometry(point);
                }
            }
        }

        Color[] colors = new Color[(int)gridSize.x * (int)gridSize.y * (int)gridSize.z];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    float distance = sdfValues[x, y, z];
                    float normalizedDistance = Mathf.Clamp01((distance + 1.0f) / 2.0f); // 假设距离范围在[-1, 1]
                    colors[x + y * (int)gridSize.x + z * (int)gridSize.x * (int)gridSize.y] = new Color(normalizedDistance, 0, 0, 1);
                }
            }
        }

        sdfTexture.SetPixels(colors);
        sdfTexture.Apply();
    }

    void SaveTexture3D(Texture3D texture, string path)
    {
        AssetDatabase.CreateAsset(texture, path);
        AssetDatabase.SaveAssets();
        Debug.Log($"SDF Texture saved to {path}");
    }

    float CalculateDistanceToGeometry(Vector3 point)
    {
        float minDistance = float.MaxValue;

        foreach (var geometry in FindObjectsOfType<GeometryInfo>())
        {
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
