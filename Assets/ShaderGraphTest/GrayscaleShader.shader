// Unity URP Unlit Shader for greyscale texture

Shader "Unlit/GrayScaleShader"
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
            HLSLPROGRAM

            // Include required URP libraries
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Define the texture and sampler
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            // Define the vertex and fragment shader functions
            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float2 uv           : TEXCOORD0;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                float grey = dot(color.rgb, float3(0.299, 0.587, 0.114));
                return float4(grey, grey, grey, 1.0);
            }

            ENDHLSL
        }
    }
}
