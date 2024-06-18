Shader "Custom/DistortionShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DistortionStrength ("Distortion Strength", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Overlay" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float _DistortionStrength;

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

            fixed4 frag (v2f i) : SV_Target
            {
                float distortion = sin(i.uv.y * 10 + _Time.y * 20) * _DistortionStrength;
                i.uv.x += distortion;
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
