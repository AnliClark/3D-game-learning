Shader "Custom/SkyBoxTrans"
{
    Properties
    {
        // Ä£Äâ¹ý¶É
        _SkyTex1("SkyTex1", CUBE) = "white" {}
        _SkyTex2("SkyTex2", CUBE) = "white" {}
        _SkyTex3("SkyTex3", CUBE) = "white" {}
        _SkyTex4("SkyTex4", CUBE) = "white" {}
        _SkyTex5("SkyTex5", CUBE) = "white" {}
        _SkyRange("SkyRange", Range(0, 5)) = 0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "BackGround" }
        LOD 100

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
                float3 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
            };

            samplerCUBE _SkyTex1;
            samplerCUBE _SkyTex2;
            samplerCUBE _SkyTex3;
            samplerCUBE _SkyTex4;
            samplerCUBE _SkyTex5;

            float _SkyRange;

            v2f vert(appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 worldViewDir = UnityWorldSpaceViewDir(i.worldPos);
                worldViewDir = normalize(worldViewDir) * -1;
                float3 worldReflect = reflect(worldViewDir, i.worldNormal);

                float relcycleValue = _SkyRange < 1 ? _SkyRange : (_SkyRange - 1);
                relcycleValue = _SkyRange < 2 ? relcycleValue : (_SkyRange - 2);
                relcycleValue = _SkyRange < 3 ? relcycleValue : (_SkyRange - 3);
                relcycleValue = _SkyRange < 4 ? relcycleValue : (_SkyRange - 4);

                float4 col = _SkyRange < 1 ? texCUBE(_SkyTex1, worldReflect) * (1 - relcycleValue) + texCUBE(_SkyTex2, worldReflect) * relcycleValue :
                             _SkyRange < 2 ? texCUBE(_SkyTex2, worldReflect) * (1 - relcycleValue) + texCUBE(_SkyTex3, worldReflect) * relcycleValue :
                             _SkyRange < 3 ? texCUBE(_SkyTex3, worldReflect) * (1 - relcycleValue) + texCUBE(_SkyTex4, worldReflect) * relcycleValue :
                             _SkyRange < 4 ? texCUBE(_SkyTex4, worldReflect) * (1 - relcycleValue) + texCUBE(_SkyTex5, worldReflect) * relcycleValue :
                                             texCUBE(_SkyTex5, worldReflect) * (1 - relcycleValue) + texCUBE(_SkyTex1, worldReflect) * relcycleValue;

                return col;
            }
            ENDCG
        }
    }
}
