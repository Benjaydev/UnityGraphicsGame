Shader "Hidden/VignetteShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Colour("Colour", Color) = (0,0,0,0)
        _Extent("Intensity", float) = 15
        _Intensity ("Extent", float) = 0.25
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            fixed4 _Colour;
            float _Intensity;
            float _Extent;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                i.uv *= 1.0 - i.uv.yx;
                float vig = i.uv.x * i.uv.y * _Extent;
                vig = pow(vig, _Intensity);

                float4 vigCol = lerp(float4(0, 0, 0, 0), _Colour, 1-vig);

                col = (col * vig) + vigCol;
                return col;
            }
            ENDCG
        }
    }
}
