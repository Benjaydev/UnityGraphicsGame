Shader "Hidden/StretchShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
                float2 screenPos : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            sampler2D _MainTex;

            float2x2 RotateMat(float angle)
            {
                float si = sin(angle);
                float co = cos(angle);
                return float2x2(co, si, -si, co);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float time = _Time.y;

                // Rough panning...
                float2 pixel = (i.uv.xy - _ScreenParams.xy * 0.5) / _ScreenParams.xy
                + float2(0.0, 0.1 - smoothstep(9.0, 12.0, time) * 0.35
                    + smoothstep(18.0, 20.0, time) * 0.15);


                float3 col;
                float n;
                float inc = (smoothstep(17.35, 18.5, time) - smoothstep(18.5, 21.0, time)) * (time - 16.0) * 0.1;

                float2x2 rotMat = RotateMat(inc);
                for (int j = 1; j < 50; j++)
                {
                    float depth = 40.0 + float(j) + smoothstep(18.0, 21.0, time) * 65;

                    float2 uv = pixel * depth / 210;

                    // Shifting the pan to the text near the end...

                    // And shifts to the right again for the last line of text at 23:00!
                    col = tex2D(_MainTex, frac(i.screenPos + float2(0.5 + smoothstep(20, 21, time) * 0.11
                        + smoothstep(23, 23.5, time) * 0.04
                        , 0.7 - smoothstep(20, 21, time) * 0.2))).rgb;

                    if ((1 - (col.y * col.y)) < float(j + 1) / 50)
                    {
                        break;
                    }

                }

                return fixed4(col, 1.0);
            }
            ENDCG
        }
    }
}
