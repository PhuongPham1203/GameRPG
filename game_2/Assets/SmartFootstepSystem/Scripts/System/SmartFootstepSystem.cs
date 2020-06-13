using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmartFootstepSystem : MonoBehaviour {
	
	[System.Serializable]
	public class GroundType{
		public string name;
		public Texture2D texture;
		public AudioClip[] sounds;
	}
	
	public AudioSource footstepAudio;
	public float groundCheckDistance = 0.25f;
	public List <GroundType> groundTypes = new List<GroundType>();
	private Terrain terrain;
	private TerrainData terrainData;
	private SplatPrototype[] splatPrototypes;
	private RaycastHit hit;
	[HideInInspector]public Texture2D currentTexture;
	[HideInInspector]public bool onTerrain;
	private bool usingTerrain;

	void Start(){
		GetTerrainInfo();
	}
	
	void GetTerrainInfo(){
		if(!Terrain.activeTerrain){
			usingTerrain = false;
		}
		else{
			terrain = Terrain.activeTerrain;
			terrainData = terrain.terrainData;
			splatPrototypes = terrain.terrainData.splatPrototypes;
			usingTerrain = true;
		}
	}
	
	void Update () {
	
	    Ray ray = new Ray(transform.position + (Vector3.up * 0.1f), Vector3.down);
		
		//check if the character is currently on a terrain and toggle the "onTerrain" bool
		if(Physics.Raycast(ray, out hit, groundCheckDistance)){
		
			if(hit.collider.GetComponent<Terrain>()){
				onTerrain = true;
			}
			else{
				onTerrain = false;
			}
		}
		
		//Get the current texture the character is standing on
		if(usingTerrain && onTerrain){
			currentTexture = splatPrototypes[GetMainTexture(transform.position)].texture;
		}
		else{
			currentTexture = GetRendererTexture();
		}
		
		//helper to visualize the ground checker ray
        #if UNITY_EDITOR
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance),Color.green);
    	#endif
	}
	
	public void Footstep(){
		for(int i = 0; i < groundTypes.Count; i++){
			if(currentTexture == groundTypes[i].texture){
				footstepAudio.PlayOneShot(groundTypes[i].sounds[Random.Range(0,groundTypes[i].sounds.Length)]);
			}
		}
	}
	
	/*returns an array containing the relative mix of textures
       on the main terrain at this world position.*/
	public float[] GetTextureMix(Vector3 worldPos) {
		
		terrain = Terrain.activeTerrain;
		terrainData = terrain.terrainData;
		Vector3 terrainPos = terrain.transform.position;
		
		int mapX = (int)(((worldPos.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth);
		int mapZ = (int)(((worldPos.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight);
		
		float[,,] splatmapData = terrainData.GetAlphamaps(mapX,mapZ,1,1);
		
		float[] cellMix = new float[splatmapData.GetUpperBound(2)+1];
		for (int n=0; n<cellMix.Length; ++n){
			cellMix[n] = splatmapData[0,0,n];    
		}
		
		return cellMix;        
	}
	
	/*returns the zero-based index of the most dominant texture
       on the main terrain at this world position.*/
	public int GetMainTexture(Vector3 worldPos) {
		
		float[] mix = GetTextureMix(worldPos);
		float maxMix = 0;
		int maxIndex = 0;
		
		for (int n=0; n<mix.Length; ++n){
			
			if (mix[n] > maxMix){
				maxIndex = n;
				maxMix = mix[n];
			}
		}
		
		return maxIndex;
	}
	
	//returns the mainTexture of a renderer's material at this position
	public Texture2D GetRendererTexture(){
		Texture2D texture = null;
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hit, groundCheckDistance)){
			if(hit.collider.gameObject.GetComponent<Renderer>()){
				MeshFilter meshFilter = (MeshFilter)hit.collider.GetComponent(typeof(MeshFilter));
				Mesh mesh = meshFilter.mesh;
				int totalSubMeshes = mesh.subMeshCount;
				int[] subMeshes = new int[totalSubMeshes];
				for(int i = 0; i < totalSubMeshes; i++){
					subMeshes[i] = mesh.GetTriangles(i).Length / 3;
				}
				
				int hitSubMesh = 0;
				int maxVal = 0;
				
				for(int i = 0; i < totalSubMeshes; i ++){
					maxVal += subMeshes[i];
						if(hit.triangleIndex <= maxVal - 1){
							hitSubMesh = i + 1;
							break;
						}
				}
					texture = (Texture2D)hit.collider.gameObject.GetComponent<Renderer>().materials[hitSubMesh - 1].mainTexture;
			}
		}
		return texture;
	}
}
