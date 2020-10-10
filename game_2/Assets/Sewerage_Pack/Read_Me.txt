
// Sewer Underground modular pack V.4.0 HDRP version

1.	Models
	
	*	This package contains 6 sub modules each with a bit different look and design.
		You can use any of them separately to create a separate scene or you can combine them as it is done in the demo scene !
		Most of the assets have separate textures ( materials ) and others are using common materials like concrete.

	*	Contains two "Pipes" modules

2.	Textures
	
	*	All textures are 2048x2048 so every user can decide where to compensate from,
		by compressing down any texture in the import settings !
		All textures are stored in the "Textures" folder and are gouped in sub folders.

	*	Warning signs - there are a few types of signs - vertical, horizonal and triangular
		and each group has its own atlas of textures. The full atlas space wont be used and only
		a few signs will be provided at first. This way you can add your own to it by editing in any image editor !


3.	Particle Systems

	*	Particle Systems are currently using Legacy/Particle shaders which seem to work with HDRP. Particle shaders may get updated later !


4.	Water

	*	Water planes are using a shader created in ShaderGraph based on a Shader Force shader available in internet and it requires FlowMaps to work.
		The shader is based on this shader from the Shader Forge wiki: http://acegikmo.com/shaderforge/wiki/index.php?title=Flowmap
		NOTE :	there is for example a flow map tool out there in internet for creating the maps. You can download it and
				create the flowmaps you want. You can find the tool at: http://teckartist.com/?page_id=107 or use 
				any other way you know for creating flow maps.


5.	Decals
	
	*	Some of the decals are using the HDRP decal shader while other decals are using the HDRP Lit shader.
		

6.	Occlusion Culling

	*	It is recommended to use occlusion culling for better performance !
		NOTE :	setting up temporary occluder object like those in the demo scene in "Occlusion_Culling_Helpers" object
				are useful to achieve a little better culling.