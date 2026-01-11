Shader "Toony Colors Pro 2/User/jerryToon_Transparent" {
	Properties {
		[TCP2HeaderHelp(BASE, Base Properties)] _Color ("Color", Vector) = (1,1,1,1)
		_HColor ("Highlight Color", Vector) = (0.785,0.785,0.785,1)
		_SColor ("Shadow Color", Vector) = (0.195,0.195,0.195,1)
		_HighlightMultiplier ("Highlight Multiplier", Range(0, 4)) = 1
		_ShadowMultiplier ("Shadow Multiplier", Range(0, 4)) = 1
		_WrapFactor ("Light Wrapping", Range(-1, 3)) = 1
		_MainTex ("Main Texture", 2D) = "white" {}
		[TCP2Separator] [TCP2Header(RAMP SETTINGS)] [TCP2Gradient] _Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
		[TCP2Separator] [TCP2HeaderHelp(REFLECTION, Reflection)] [NoScaleOffset] _Cube ("Cubemap", Cube) = "_Skybox" {}
		_ReflectColor ("Color (RGB) Strength (Alpha)", Vector) = (1,1,1,0.5)
		[TCP2Separator] [TCP2HeaderHelp(TRANSPARENCY)] [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlendTCP2 ("Blending Source", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlendTCP2 ("Blending Dest", Float) = 10
		[TCP2Separator] [HideInInspector] __dummy__ ("unused", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
	//CustomEditor "TCP2_MaterialInspector_SG"
}