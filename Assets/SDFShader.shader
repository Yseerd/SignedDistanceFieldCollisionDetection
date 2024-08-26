Shader "Custom/SDFShader"
{
    Properties
    {
        _SDFTex("SDF Texture", 3D) = "" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 uvw : TEXCOORD0;  // 将uvw作为纹理坐标
            };

            sampler3D _SDFTex;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                // 将世界坐标转化为标准化的纹理坐标
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uvw = (worldPos + 0.5) / 1.0; // 假设对象在[0, 1]范围内，适当缩放和偏移

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 查询SDF纹理
                float sdfValue = tex3D(_SDFTex, i.uvw).r;

                // 基于SDF值调整颜色
                if (sdfValue < 0.5)
                {
                    return fixed4(1, 0, 0, 1);  // 红色：几何体内
                }
                else
                {
                    return fixed4(0, 1, 0, 1);  // 绿色：几何体外
                }
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
