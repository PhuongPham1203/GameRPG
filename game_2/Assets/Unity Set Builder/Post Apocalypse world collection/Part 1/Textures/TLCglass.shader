Shader "FX/TLCindie/Glass" {
Properties {
	_Color ("Color(RGB) Distortion(A)", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_Parallax ("Height", Range (0.000, 0.08)) = 0.02
	_ReflectColor ("Clarity(RGB) Reflect Strengh(A)", Color) = (1,1,1,0.5)
	_MainTex ("Main Texture(RGB) & Hight(A)", 2D) = "white" {}
	_Cube ("Reflection Cubemap", Cube) = "" { TexGen CubeReflect }
	_BumpMap ("Normalmap", 2D) = "bump" {}
	
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 400

Cull Off
 
CGPROGRAM
#pragma surface surf BlinnPhong alpha
#pragma target 3.0
//input limit (8) exceeded, shader uses 9
#pragma exclude_renderers d3d11_9x

sampler2D _MainTex;
sampler2D _BumpMap;
samplerCUBE _Cube;
fixed4 _Color;
fixed4 _ReflectColor;
half _Shininess;
float _Parallax;


struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
	float3 worldRefl;
	float3 viewDir;
	INTERNAL_DATA
};

void surf (Input IN, inout SurfaceOutput o) {

	half h = tex2D (_MainTex, IN.uv_BumpMap).w;
	float2 offset = ParallaxOffset (h, _Parallax, IN.viewDir);
	IN.uv_MainTex += offset;
	IN.uv_BumpMap += offset;

	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	o.Albedo = tex.rgb * _Color.rgb;
	o.Gloss = tex.a;
	o.Alpha = tex.a * _Color.a;
	o.Specular = _Shininess;
	
	{
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	float3 worldRefl = WorldReflectionVector (IN, o.Alpha);
	fixed4 reflcol = texCUBE (_Cube, worldRefl);
	reflcol *= tex.a;
	o.Emission = reflcol.rgb * _ReflectColor.a;
	o.Alpha = reflcol.a * _ReflectColor.rgb;
	
}
}
ENDCG
}

FallBack "Transparent/VertexLit"
}