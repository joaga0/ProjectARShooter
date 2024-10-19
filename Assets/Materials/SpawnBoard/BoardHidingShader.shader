Shader "Custom/BoardHidingShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Alpha ("Alpha", Range(0, 1)) = 1    
    }
    SubShader
    {
        ZWrite On
        Tags { "Queue" = "Transparent" "Queue"="Alphatest+51"} // ������ ť ����
        Stencil
        {
            Ref 1
            Comp Always
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            /*#pragma surface surf nolight noforwardadd nolightmap noambient novertexlights noshadow

            struct Input { float4 color:COLOR; };

            void surf (Input IN, inout SurfaceOutput o){}
            float4 Lightingnolight(SurfaceOutput s, float3 lightDir, float atten)
            {
                return float4(0, 0, 0, 0);
            }
            ENDCG
            */
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
            };

            float4 _Color;
            float _Alpha;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // ���� _Alpha ���� �����Ͽ� ���� ����
                return float4(_Color.rgb, _Color.a * _Alpha);
            }
            ENDCG
        }
    }
    FallBack ""
}