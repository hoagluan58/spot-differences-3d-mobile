Shader "Tutorial/UnlitMask" {
	Properties {
		_Color ("Main Color", Vector) = (0,0,0,0.7)
		_AreaCount ("Area Count", Float) = 0
		_AspectRatio ("Aspect Ratio", Float) = 1
		_FeatherDistance ("Feather Distance", Float) = 0.05
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
}