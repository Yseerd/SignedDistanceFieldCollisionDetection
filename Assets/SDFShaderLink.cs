using UnityEditor;
using UnityEngine;


    public class SDFShaderLink : MonoBehaviour
    {
   
        public Material material; // 需要绑定的材质
        public string sdfTexturePath = "Assets/SDFTexture.asset"; // 3D纹理的路径

        void Start()
        {
            // 加载保存的SDF纹理
            Texture3D sdfTexture = AssetDatabase.LoadAssetAtPath<Texture3D>(sdfTexturePath);

            if (sdfTexture != null)
            {
                // 将纹理绑定到材质的Shader上
                material.SetTexture("_SDFTex", sdfTexture);
                Debug.Log("SDF Texture loaded and applied to material.");
            }
            else
            {
                Debug.LogError("Failed to load SDF Texture.");
            }
        }
    }
 