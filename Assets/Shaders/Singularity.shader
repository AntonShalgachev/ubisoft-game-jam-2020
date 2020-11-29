Shader "Hidden/Custom/Singularity"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        float _Radius;
        float _TimePassed;
        float4 _BackgroundColor;

        float Sphere(float2 uv, float2 center, float r, float blur)
        {
            float d = length(center - uv);
            return smoothstep(r, r-blur, d);
        }

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            // float2 uv = i.texcoord / _ScreenParams.xy;
            // uv-=0.5;
            // uv.x*=_ScreenParams.x/_ScreenParams.y;

            // uv = i.texcoord;
            // // uv -= 0.5;

            // float time=fmod(_Time.y*0.2, 2.0);

            // // // time = _Time.y;
            
            // float s = Sphere(uv, float2(0.5, 0.5), 1.0f - _Radius, 0.2);
            // float4 distortion = float4(s, s, s, 1.0);
            
            // float4 table=lerp(SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv*distortion.r), 
            //             _BackgroundColor, 
            //             1.0-distortion);

            // return table;
            
            // Output to screen
            // fragColor=table*vec4(1.0, 1.0, 1.0, 1.0);

            // float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
            // float luminance = dot(color.rgb, float3(0.2126729, 0.7151522, 0.0721750));
            // color.rgb = lerp(color.rgb, luminance.xxx, _Radius.xxx);
            // return color;

            float c = float2(0.5, 0.5);
            float2 offset = i.texcoord - c;
            float d = length(offset);
            float newD = pow(d * 2.0, 1.0 / _Radius) / 2.0;
            // float2 newOffset = sign(offset) * pow(abs(offset) * 2.0, 1.0 / _Radius) / 2.0;
            float2 newOffset = normalize(offset) * newD;
            float2 uv = c + newOffset;

            return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
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