Shader "Hidden/SecurityCamera"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PixelSize("Pixel Size", float) = 1
        _Distortion("Distortion", float) = 1
        _Movement("Movement", float) = 1
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
            float _PixelSize;
            float _Distortion;
            float _Movement;

            fixed4 frag(v2f i) : SV_Target
            {
                float2 cor;

                cor.x = i.uv.x / _PixelSize;
                cor.y = (i.uv.y + _PixelSize * (cos(_Time.y) * _Movement) * fmod(floor(cor.x), sin(_Time.y)* _Distortion)) / (_PixelSize * 3.0);

                float2 ico = floor(cor);
                float2 fco = frac(cor);

                float3 pix = step(1.5, fmod(float3(0.0,1.0,2.0) + ico.x, 3.0));
                float3 ima = tex2D(_MainTex, _PixelSize * ico * float2(1.0,3.0)).xyz;

                float3 col = pix * dot(pix, ima);

                col *= step(abs(fco.x - 0.5), 0.4);
                col *= step(abs(fco.y - 0.5), 0.4);

                col *= 1.2;
                return fixed4(col, 1);
            }
            ENDCG
        }
    }
}
