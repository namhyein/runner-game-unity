Shader "Custom/VerticalGradientShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1, 1, 1, 1)
        _TopColor("Top Color", Color) = (1,1,1,1)
        _BottomColor("Bottom Color", Color) = (0,0,0,1)
        _Offset ("Offset", Float) = 0.0 // 기본 오프셋 값
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        LOD 200

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
            fixed4 _TopColor;
            fixed4 _BottomColor;
            float _Offset;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color; // Vertex color
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float lerpFactor = clamp(i.uv.y + _Offset, 0.0, 1.0); // Offset 적용 및 클램프
                fixed4 gradientColor = lerp(_BottomColor, _TopColor, lerpFactor);
                fixed4 textureColor = tex2D(_MainTex, i.uv) * i.color * _Color; // Tint color applied to texture color
                return textureColor * gradientColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
