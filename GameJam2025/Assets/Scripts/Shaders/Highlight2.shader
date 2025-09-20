Shader "Custom/Highlight2"
{
     Properties
    {
        _HighlightColor ("Highlight Color", Color) = (1,1,0,0.5)
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay+1" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        ZTest LEqual
        Cull Back

        Pass
        {
            CGPROGRAM
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

            fixed4 _HighlightColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _HighlightColor;
            }
            ENDCG
        }
    }
}
