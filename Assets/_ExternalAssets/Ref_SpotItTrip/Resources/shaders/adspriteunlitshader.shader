Shader "Adverty/AdSpriteUnlitShader" {
	Properties {
		[PerRendererData] _BaseMap ("Texture", 2D) = "white" {}
		_BaseColor ("Color", Vector) = (1,1,1,1)
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