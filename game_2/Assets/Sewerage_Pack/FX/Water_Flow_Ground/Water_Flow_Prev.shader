// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:1,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:True,enco:True,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:False,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2439,x:33568,y:32663,varname:node_2439,prsc:2|diff-1622-OUT,spec-2221-OUT,gloss-8370-OUT,normal-483-OUT,alpha-4026-OUT,refract-6850-OUT;n:type:ShaderForge.SFN_Tex2d,id:5698,x:31176,y:32637,ptovrint:False,ptlb:Flow Map,ptin:_FlowMap,varname:_FlowMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e0c7b631c74a631438b5637ba86c24f2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ComponentMask,id:1020,x:31397,y:32589,varname:node_1020,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-5698-RGB;n:type:ShaderForge.SFN_Multiply,id:9444,x:31607,y:32825,varname:node_9444,prsc:2|A-6233-OUT,B-1691-OUT;n:type:ShaderForge.SFN_RemapRange,id:6130,x:31607,y:32565,varname:node_6130,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-1020-OUT;n:type:ShaderForge.SFN_Vector1,id:6233,x:31397,y:32811,varname:node_6233,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:1691,x:31397,y:32914,varname:node_1691,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:7645,x:31828,y:32669,varname:node_7645,prsc:2|A-6130-OUT,B-9444-OUT;n:type:ShaderForge.SFN_Time,id:6245,x:31382,y:33048,varname:node_6245,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1931,x:31607,y:33018,varname:node_1931,prsc:2|A-6245-TSL,B-7909-OUT;n:type:ShaderForge.SFN_Vector1,id:7719,x:31369,y:33402,varname:node_7719,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Frac,id:1636,x:31828,y:32896,varname:node_1636,prsc:2|IN-1931-OUT;n:type:ShaderForge.SFN_Add,id:3704,x:31828,y:33117,varname:node_3704,prsc:2|A-1931-OUT,B-7719-OUT;n:type:ShaderForge.SFN_Multiply,id:8668,x:32044,y:32578,varname:node_8668,prsc:2|A-7645-OUT,B-1636-OUT;n:type:ShaderForge.SFN_Multiply,id:8033,x:32044,y:32738,varname:node_8033,prsc:2|A-7645-OUT,B-6239-OUT;n:type:ShaderForge.SFN_Frac,id:6239,x:32044,y:32896,varname:node_6239,prsc:2|IN-3704-OUT;n:type:ShaderForge.SFN_Subtract,id:499,x:32028,y:33117,varname:node_499,prsc:2|A-7712-OUT,B-1636-OUT;n:type:ShaderForge.SFN_Vector1,id:7712,x:32028,y:33273,varname:node_7712,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Divide,id:8098,x:32261,y:33117,varname:node_8098,prsc:2|A-499-OUT,B-7712-OUT;n:type:ShaderForge.SFN_TexCoord,id:9323,x:32044,y:32368,varname:node_9323,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:6491,x:32536,y:32290,varname:node_6491,prsc:2,ntxv:0,isnm:False|UVIN-4908-OUT,TEX-9328-TEX;n:type:ShaderForge.SFN_Add,id:4908,x:32276,y:32492,varname:node_4908,prsc:2|A-9323-UVOUT,B-8668-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:9328,x:32276,y:32320,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_9328,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:1586,x:32276,y:32647,varname:node_1586,prsc:2|A-9323-UVOUT,B-8033-OUT;n:type:ShaderForge.SFN_Tex2d,id:4471,x:32536,y:32659,varname:node_4471,prsc:2,ntxv:3,isnm:True|UVIN-4908-OUT,TEX-5753-TEX;n:type:ShaderForge.SFN_Tex2d,id:3717,x:32536,y:32845,varname:_node_4471_copy,prsc:2,ntxv:3,isnm:True|UVIN-1586-OUT,TEX-5753-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:5753,x:32276,y:32848,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:_node_9328_copy,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Abs,id:165,x:32536,y:33041,varname:node_165,prsc:2|IN-8098-OUT;n:type:ShaderForge.SFN_Lerp,id:5211,x:32776,y:32456,varname:node_5211,prsc:2|A-6491-RGB,B-3459-RGB,T-165-OUT;n:type:ShaderForge.SFN_Lerp,id:2829,x:32776,y:32703,varname:node_2829,prsc:2|A-4471-RGB,B-3717-RGB,T-165-OUT;n:type:ShaderForge.SFN_Multiply,id:1622,x:32989,y:32439,varname:node_1622,prsc:2|A-2515-RGB,B-5211-OUT;n:type:ShaderForge.SFN_Color,id:2515,x:32776,y:32251,ptovrint:False,ptlb:Tint,ptin:_Tint,varname:node_2515,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:3459,x:32536,y:32456,varname:node_3459,prsc:2,ntxv:0,isnm:False|UVIN-1586-OUT,TEX-9328-TEX;n:type:ShaderForge.SFN_Slider,id:4026,x:32727,y:33097,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_4026,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.75,max:1;n:type:ShaderForge.SFN_Slider,id:8370,x:32727,y:33003,ptovrint:False,ptlb:Rougness,ptin:_Rougness,varname:_Opacity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.75,max:1;n:type:ShaderForge.SFN_Slider,id:2221,x:32727,y:32902,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:_Glossiness_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.75,max:1;n:type:ShaderForge.SFN_Slider,id:9660,x:31124,y:33219,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_9660,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_RemapRange,id:7909,x:31485,y:33224,varname:node_7909,prsc:2,frmn:0,frmx:1,tomn:0,tomx:5|IN-9660-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3669,x:32941,y:33339,ptovrint:False,ptlb:Refraction_Multiplier,ptin:_Refraction_Multiplier,varname:node_3669,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:1832,x:33194,y:33246,varname:node_1832,prsc:2|A-4075-OUT,B-3669-OUT;n:type:ShaderForge.SFN_Multiply,id:6850,x:33355,y:33009,varname:node_6850,prsc:2|A-4474-OUT,B-1832-OUT;n:type:ShaderForge.SFN_Slider,id:4075,x:32847,y:33226,ptovrint:False,ptlb:Refraction,ptin:_Refraction,varname:node_4075,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;n:type:ShaderForge.SFN_Vector3,id:1765,x:32776,y:32613,varname:node_1765,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Lerp,id:483,x:33165,y:32705,varname:node_483,prsc:2|A-1765-OUT,B-2829-OUT,T-4075-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4474,x:33177,y:32859,varname:node_4474,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2829-OUT;proporder:2515-9328-5753-5698-4026-8370-2221-9660-3669-4075;pass:END;sub:END;*/

Shader "Custom/Water_Flow_Prev" {
    Properties {
        _Tint ("Tint", Color) = (1,1,1,1)
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _FlowMap ("Flow Map", 2D) = "white" {}
        _Opacity ("Opacity", Range(0, 1)) = 0.75
        _Rougness ("Rougness", Range(0, 1)) = 0.75
        _Metallic ("Metallic", Range(0, 1)) = 0.75
        _Speed ("Speed", Range(0, 1)) = 1
        _Refraction_Multiplier ("Refraction_Multiplier", Float ) = 0.2
        _Refraction ("Refraction", Range(0, 1)) = 0.1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        GrabPass{ "Refraction" }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d11 
            #pragma target 3.0
            uniform sampler2D Refraction;
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float4 _Tint;
            uniform float _Opacity;
            uniform float _Rougness;
            uniform float _Metallic;
            uniform float _Speed;
            uniform float _Refraction_Multiplier;
            uniform float _Refraction;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 projPos : TEXCOORD5;
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _FlowMap_var = tex2D(_FlowMap,TRANSFORM_TEX(i.uv0, _FlowMap));
                float2 node_7645 = ((_FlowMap_var.rgb.rg*2.0+-1.0)*(0.5*(-1.0)));
                float4 node_6245 = _Time;
                float node_1931 = (node_6245.r*(_Speed*5.0+0.0));
                float node_1636 = frac(node_1931);
                float2 node_4908 = (i.uv0+(node_7645*node_1636));
                float3 node_4471 = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_4908, _Normal)));
                float2 node_1586 = (i.uv0+(node_7645*frac((node_1931+0.5))));
                float3 _node_4471_copy = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_1586, _Normal)));
                float node_7712 = 0.5;
                float node_165 = abs(((node_7712-node_1636)/node_7712));
                float3 node_2829 = lerp(node_4471.rgb,_node_4471_copy.rgb,node_165);
                float3 normalLocal = lerp(float3(0,0,1),node_2829,_Refraction);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (node_2829.rg*(_Refraction*_Refraction_Multiplier));
                float4 sceneColor = tex2D(Refraction, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 1.0 - _Rougness; // Convert roughness to gloss
                float perceptualRoughness = _Rougness;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _Metallic;
                float specularMonochrome;
                float4 node_6491 = tex2D(_Diffuse,TRANSFORM_TEX(node_4908, _Diffuse));
                float4 node_3459 = tex2D(_Diffuse,TRANSFORM_TEX(node_1586, _Diffuse));
                float3 diffuseColor = (_Tint.rgb*lerp(node_6491.rgb,node_3459.rgb,node_165)); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,_Opacity),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d11 
            #pragma target 3.0
            uniform sampler2D Refraction;
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float4 _Tint;
            uniform float _Opacity;
            uniform float _Rougness;
            uniform float _Metallic;
            uniform float _Speed;
            uniform float _Refraction_Multiplier;
            uniform float _Refraction;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 projPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _FlowMap_var = tex2D(_FlowMap,TRANSFORM_TEX(i.uv0, _FlowMap));
                float2 node_7645 = ((_FlowMap_var.rgb.rg*2.0+-1.0)*(0.5*(-1.0)));
                float4 node_6245 = _Time;
                float node_1931 = (node_6245.r*(_Speed*5.0+0.0));
                float node_1636 = frac(node_1931);
                float2 node_4908 = (i.uv0+(node_7645*node_1636));
                float3 node_4471 = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_4908, _Normal)));
                float2 node_1586 = (i.uv0+(node_7645*frac((node_1931+0.5))));
                float3 _node_4471_copy = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_1586, _Normal)));
                float node_7712 = 0.5;
                float node_165 = abs(((node_7712-node_1636)/node_7712));
                float3 node_2829 = lerp(node_4471.rgb,_node_4471_copy.rgb,node_165);
                float3 normalLocal = lerp(float3(0,0,1),node_2829,_Refraction);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (node_2829.rg*(_Refraction*_Refraction_Multiplier));
                float4 sceneColor = tex2D(Refraction, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:

				// Works in Unity5.6
                //float attenuation = LIGHT_ATTENUATION(i);
				// Works in 2017+
				fixed3 worldPos = 0;
				UNITY_LIGHT_ATTENUATION(attenuation, i, worldPos);

                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 1.0 - _Rougness; // Convert roughness to gloss
                float perceptualRoughness = _Rougness;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _Metallic;
                float specularMonochrome;
                float4 node_6491 = tex2D(_Diffuse,TRANSFORM_TEX(node_4908, _Diffuse));
                float4 node_3459 = tex2D(_Diffuse,TRANSFORM_TEX(node_1586, _Diffuse));
                float3 diffuseColor = (_Tint.rgb*lerp(node_6491.rgb,node_3459.rgb,node_165)); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * _Opacity,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
