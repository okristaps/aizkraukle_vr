Shader "Custom/Panorama360"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Front

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 viewDir : TEXCOORD0;
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.viewDir = v.vertex.xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 dir = normalize(i.viewDir);

                // Convert direction to equirectangular UV
                float u = 0.5 + atan2(dir.z, dir.x) / (2.0 * 3.14159265);
                float v = 0.5 + asin(dir.y) / 3.14159265;

                fixed4 col = tex2D(_MainTex, float2(u, v));
                return col;
            }
            ENDCG
        }
    }
}
