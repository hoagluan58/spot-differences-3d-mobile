Shader "Adverty/AdUIShader" {
	Properties {
		[NoScaleOffset] _MainTex ("Base (RGB)", 2D) = "white" {}
		_BaseColor ("Color", Vector) = (1,1,1,1)
		[HideInInspector] _StencilWriteMaskID ("Stencil Write Mask ID", Float) = 0
		[HideInInspector] _WatermarkTex ("_WatermarkTex (RGB)", 2D) = "white" {}
		[HideInInspector] _WatermarkIsVisible ("Watermark is Visible", Float) = 1
		[HideInInspector] _WatermarkUvSize ("Watermark UV size", Vector) = (0,0,0,0)
		[HideInInspector] _FadeTexture ("_FadeTex (RGB)", 2D) = "white" {}
		[HideInInspector] _TransitionProgress ("Transition Progress", Float) = 0
		[HideInInspector] _FadeTexUVFactor ("_FadeTextureUVFactor", Float) = 0
		[HideInInspector] _MainTexUVFactor ("_MainTextureUVFactor", Float) = 0
		[HideInInspector] _StencilComp ("Stencil Comparison", Float) = 8
		[HideInInspector] _Stencil ("Stencil ID", Float) = 0
		[HideInInspector] _StencilOp ("Stencil Operation", Float) = 0
		[HideInInspector] _StencilWriteMask ("Stencil Write Mask", Float) = 255
		[HideInInspector] _StencilReadMask ("Stencil Read Mask", Float) = 255
		[HideInInspector] _ColorMask ("Color Mask", Float) = 15
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}