Shader "Bump with SpecMap" {

    Properties {

        _Color ("Main Color", Color) = (1,1,1,1)

        _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)

        _SpecMap ("Specular Level (R)", 2D) = "grey" {} 
		
        _Shininess ("Shininess", Range (0.01, 1)) = 0.078125

        _MainTex ("Base (RGB) TransGloss (A)", 2D) = "white" {}

        _BumpMap ("Normalmap", 2D) = "bump" {}

        
        

    }

 

    SubShader {

        Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}

        LOD 400

        

        

        CGPROGRAM

            #pragma surface surf BlinnPhong alphatest:_Cutoff

            #pragma exclude_renderers flash

             

            sampler2D _MainTex;

            sampler2D _BumpMap;
            


            fixed4 _Color;

            half _Shininess;

            sampler2D _SpecMap;

             

            struct Input {

                float2 uv_MainTex;

                float2 uv_BumpMap;
                


            };

             

            void surf (Input IN, inout SurfaceOutput o) {

                fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);

                o.Albedo = tex.rgb * _Color.rgb;

                o.Gloss = tex2D(_SpecMap, IN.uv_MainTex).r; // Sample the new spec texture, reusing the diffuse map's UVs.

                o.Specular = _Shininess;

                o.Alpha = tex.a * _Color.a;

                o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));

            }

        ENDCG

    }

     

    FallBack "Transparent/Cutout/VertexLit"

}