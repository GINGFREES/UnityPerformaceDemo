Shader "CustomShader/WaterLineMove"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed("Speed", Float) = 1.0
        _Amp("Amp", Float) = 0.3
    }
    SubShader
    {
        Tags {"RenderType"="Opaque"}
        LOD 100

        Pass
        {
            Cull Off
            ZWrite Off
            Lighting Off
            Fog { Mode off }
            Offset -1, -1
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _Speed;
            float _Amp;
            float4 _MainTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float t = _SinTime.y;
                o.vertex.z += sin(o.vertex.z *  _Speed * _Amp);
                o.vertex.y += sin(o.vertex.y + 0.1*t);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.x += sin(o.uv.x + 0.1*t);
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
