// Simplified Bumped shader. Differences from regular Bumped one:
// - no Main Color
// - Normalmap uses Tiling/Offset of the Base texture
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Character/Double/Alpha Bumped Diffuse" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	//_BumpMap ("Normalmap", 2D) = "bump" {}
}

SubShader {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Opaque"}//"Queue"="Transparent+5"  渲染顺序在 transparent之后 不然会有透明叠加问题
	LOD 250
	
	Cull front
CGPROGRAM
#pragma surface surf Lambert alpha:fade

sampler2D _MainTex;
//sampler2D _BumpMap;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
	o.Albedo = c.rgb;
	o.Alpha = c.a;
	//o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
}
ENDCG  


	Cull back
CGPROGRAM
#pragma surface surf Lambert alpha:fade

sampler2D _MainTex;
//sampler2D _BumpMap;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
	o.Albedo = c.rgb;
	o.Alpha = c.a;
	//o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
}
ENDCG  
}

FallBack "Mobile/Diffuse"
}
