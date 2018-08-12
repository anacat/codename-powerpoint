Shader "Camera/Recolor" {
 Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _ColorA ("Color A", Color) = (1,1,1,1)
 _ColorB ("Color B", Color) = (0,0,0,1)
 //_Glitch ("Glitch", Float) = 1

 }
 SubShader {
 Pass {
 CGPROGRAM
 #pragma vertex vert_img
 #pragma fragment frag
 
 #include "UnityCG.cginc"
 
 uniform sampler2D _MainTex;
 //uniform float _Glitch;
 uniform float4 _ColorA;
 uniform float4 _ColorB;
 
 float4 frag(v2f_img i) : COLOR {
 float4 c = tex2D(_MainTex, i.uv);
 //c = float4((c.r*_Glitch)-floor(c.r*_Glitch),(c.g*_Glitch)-floor(c.g*_Glitch),(c.b*_Glitch)-floor(c.b*_Glitch), c.a);
 float lum = c.r * .3 + c.g*.59 + c.b*.11;
 c = float4(lerp(_ColorA.r, _ColorB.r, lum), lerp(_ColorA.g, _ColorB.g, lum), lerp(_ColorA.b, _ColorB.b, lum), 1);
 return c;
 }
 ENDCG
 }
 }
}
