Shader "Custom/RadialGradientShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1, 1, 1, 1)
        _CenterColor ("Center Color", Color) = (1, 1, 1, 1)
        _EdgeColor ("Edge Color", Color) = (0, 0, 0, 0)
        _Offset ("Offset", Vector) = (0.5, 0.5, 0, 0) // 기본 중심점은 (0.5, 0.5)
        _Size ("Size", Vector) = (1.0, 1.0, 0, 0) // 기본 크기 배율은 (1.0, 1.0)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _CenterColor;
            fixed4 _EdgeColor;
            float4 _Offset;
            float4 _Size;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color * _Color; // Vertex color multiplied by the tint color
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = (i.uv - _Offset.xy) / _Size.xy; // 오프셋 및 크기 적용
                float dist = length(uv) * 2.0; // 거리 계산
                fixed4 gradientColor = lerp(_CenterColor, _EdgeColor, dist); // 거리 기반 색 보간
                fixed4 textureColor = tex2D(_MainTex, i.uv);
                return textureColor * gradientColor * i.color; // Gradient color multiplied by vertex color
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
