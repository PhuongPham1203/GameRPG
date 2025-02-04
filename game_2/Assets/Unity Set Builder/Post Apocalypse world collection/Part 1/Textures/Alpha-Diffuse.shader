Shader "Transparent/Diffuse" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Illum ("Illumin (A)", 2D) = "white" {}
	_EmissionLM ("Emission (Lightmapper)", Float) = 0
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True""IgnoreShadows"="True" "RenderType"="Transparent"}
	LOD 200

	Cull off
CGPROGRAM
#pragma surface surf Lambert alpha

sampler2D _MainTex;
sampler2D _Illum;
fixed4 _Color;

struct Input {
	float2 uv_MainTex;
	float2 uv_Illum;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	
	o.Albedo = c.rgb;
	o.Emission = c.rgb * UNITY_SAMPLE_1CHANNEL(_Illum, IN.uv_Illum);
	o.Alpha = c.a;
}
ENDCG
}

Fallback "Self-Illumin/Transparent/VertexLit"
}
