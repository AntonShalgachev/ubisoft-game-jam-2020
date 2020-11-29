Shader "Hidden/Custom/Singularity"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        float _Radius;
        float _TimePassed;
        float4 _BackgroundColor;
        float _SingularityScale;

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            float c = float2(0.5, 0.5);
            float2 offset = i.texcoord - c;
            float d = length(offset);
            float newD = pow(d * 2.0, 1.0 / _Radius) / 2.0;
            float2 newOffset = normalize(offset) * newD / (1.0 - _SingularityScale);
            float2 uv = c + newOffset;

            float4 collapsedColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);

            return lerp(collapsedColor, _BackgroundColor, _SingularityScale);
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}