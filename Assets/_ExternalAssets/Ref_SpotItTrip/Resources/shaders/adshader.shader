Shader "Adverty/AdShader" {
	Properties {
		_BaseColor ("Color", Vector) = (1,1,1,1)
		[NoScaleOffset] _BaseMap ("Albedo", 2D) = "white" {}
		[Gamma] _Metallic ("Metallic", Range(0, 1)) = 0
		_Smoothness ("Smoothness", Range(0, 1)) = 0.5
		[HideInInspector] _StencilWriteMaskID ("Stencil Write Mask ID", Float) = 0
		[HideInInspector] _WatermarkTex ("_WatermarkTex (RGB)", 2D) = "white" {}
		[HideInInspector] _WatermarkIsVisible ("Watermark is Visible", Float) = 1
		[HideInInspector] _WatermarkUvSize ("Watermark UV size", Vector) = (0,0,0,0)
		[HideInInspector] _FadeTexture ("_FadeTexture (RGB)", 2D) = "white" {}
		[HideInInspector] _TransitionProgress ("Transition Progress", Float) = 0
		[HideInInspector] _FadeTexUVFactor ("_FadeTextureUVFactor", Float) = 0
		[HideInInspector] _MainTexUVFactor ("_MainTextureUVFactor", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
}