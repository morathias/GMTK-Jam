Shader "Unlit/Bullet"
{
    Properties
    {
        _MainColor("Main Color", Color) = (1,1,1,1)
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimPow("Rim Pow", float) = 1
        _Emission("Emission", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal: NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal: NORMAL;
                float3 viewDir : TEXCOORD0;
            };

            half4 _MainColor, _RimColor;
            half _RimPow, _Emission;

            UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_INSTANCING_BUFFER_END(Props)

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = WorldSpaceViewDir(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = _MainColor;
                half rim = 1.0 - saturate(dot(normalize(i.viewDir), i.normal));
                half3 rimColor = _RimColor.rgb * pow(rim, _RimPow);
                col.rgb += _RimColor.rgb * pow(rim, _RimPow);
                col.rgb += rimColor * _Emission;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
