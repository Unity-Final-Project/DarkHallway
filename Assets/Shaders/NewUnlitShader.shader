Shader "Custom/BlurShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Size("Blur Size", Float) = 1.0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_TexelSize;
                float _Size;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.texcoord;
                    return o;
                }

                float4 frag(v2f i) : SV_Target
                {
                    float4 col = float4(0, 0, 0, 0);
                    float2 offset = _MainTex_TexelSize.xy * _Size;

                    col += tex2D(_MainTex, i.uv + offset * -2.0);
                    col += tex2D(_MainTex, i.uv + offset * -1.0);
                    col += tex2D(_MainTex, i.uv);
                    col += tex2D(_MainTex, i.uv + offset * 1.0);
                    col += tex2D(_MainTex, i.uv + offset * 2.0);

                    return col / 5.0;
                }
                ENDCG
            }
        }
}
