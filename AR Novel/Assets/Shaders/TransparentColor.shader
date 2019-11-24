Shader ".ShaderExample/Transparent/Color" {
  Properties {
    _Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
    _Alpha ("Alpha", Range(0.0, 1.0)) = 1.0
  }
  SubShader {
  
     Tags {"RenderType"="Transparent" "Queue"="Transparent"}
            LOD 200
             Pass {
                 ColorMask 0
             }
             // Render normally
     
                 ZWrite Off
                 Cull Off
                 Blend SrcAlpha OneMinusSrcAlpha
                 ColorMask RGB
 
 
    CGPROGRAM
    #pragma surface surf Lambert alpha
 
    fixed4 _Color;
    float _Alpha;
 
    // Note: pointless texture coordinate. I couldn't get Unity (or Cg)
    //       to accept an empty Input structure or omit the inputs.
    struct Input {
      float2 uv_MainTex;
    };
 
    void surf (Input IN, inout SurfaceOutput o) {
      o.Albedo = _Color.rgb;
      o.Emission = _Color.rgb; // * _Color.a;
      o.Alpha = _Alpha;
    }
    ENDCG
  } 
  FallBack "Diffuse"
}