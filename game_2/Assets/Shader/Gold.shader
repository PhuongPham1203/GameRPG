// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "water"
{
	Properties
	{
		_Float0("Float 0", Range( 0 , 1)) = 0.63
		_Float1("Float 1", Float) = 0
		_Float2("Float 2", Float) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			half filler;
		};

		uniform float _Float0;
		uniform float _Float1;
		uniform float _Float2;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float temp_output_14_0 = ( ( (-0.5 + (_Float0 - 0.0) * (1.5 - -0.5) / (1.0 - 0.0)) - v.texcoord.xy.y ) * _Float1 );
			float3 ase_vertexNormal = v.normal.xyz;
			v.vertex.xyz += ( saturate( ( ( 1.0 - ( temp_output_14_0 * temp_output_14_0 ) ) * _Float2 ) ) * ase_vertexNormal );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color41 = IsGammaSpace() ? float4(1,0.7764706,0.4078431,0) : float4(1,0.5647116,0.1384316,0);
			o.Albedo = color41.rgb;
			o.Metallic = 1.0;
			o.Smoothness = 0.7;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18400
433;151;825;521;-140.6938;399.6715;1.772274;True;True
Node;AmplifyShaderEditor.RangedFloatNode;13;-802.0705,49.9521;Inherit;False;Property;_Float0;Float 0;0;0;Create;True;0;0;False;0;False;0.63;0.63;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;21;-457.4707,-13.74782;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.5;False;4;FLOAT;1.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;11;-424.1703,252.7521;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;17;-106.4705,265.752;Inherit;False;Property;_Float1;Float 1;1;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;12;-215.2706,100.6521;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-37.17052,95.45213;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;119.7295,88.95213;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;277.0294,218.9521;Inherit;False;Property;_Float2;Float 2;2;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;18;267.9295,91.55209;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;453.8295,73.35207;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;23;308.7294,358.0523;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;25;589.1723,166.8225;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;862.6725,69.32249;Inherit;False;Constant;_Float3;Float 3;3;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;41;850.4896,-142.6919;Inherit;False;Constant;_Color0;Color 0;3;0;Create;True;0;0;False;0;False;1,0.7764706,0.4078431,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;27;941.0074,180.3094;Inherit;False;Constant;_Float4;Float 4;3;0;Create;True;0;0;False;0;False;0.7;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;713.0292,257.9523;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1184.524,23.78027;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;21;0;13;0
WireConnection;12;0;21;0
WireConnection;12;1;11;2
WireConnection;14;0;12;0
WireConnection;14;1;17;0
WireConnection;15;0;14;0
WireConnection;15;1;14;0
WireConnection;18;0;15;0
WireConnection;19;0;18;0
WireConnection;19;1;20;0
WireConnection;25;0;19;0
WireConnection;24;0;25;0
WireConnection;24;1;23;0
WireConnection;0;0;41;0
WireConnection;0;3;26;0
WireConnection;0;4;27;0
WireConnection;0;11;24;0
ASEEND*/
//CHKSM=6947FC6E438A276F4EDF414CDB1E767D10D9CA06