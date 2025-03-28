using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 5;

    [SerializeField]    
    private Skybox Camera;

    [SerializeField]
    private Material Skybox;
    
    int anum = -1;


    // Start is called before the first frame update

    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;
    void Start()
    {
        
        for (int i = 0; i < numberOfTiles; i++)
        {
            if(i==0)
            {
                SpawnTile(0);
            }
            SpawnTile(Random.Range(1,tilePrefabs.Length));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z - 35 > zSpawn - (numberOfTiles * tileLength))
        {
            if(anum == 1 || anum == 2 || anum ==3)
                SpawnTile(Random.Range(4, 10));
            else if(anum == 4 || anum == 5 || anum == 6)
                SpawnTile(Random.Range(7, 10));
            else if(anum == 7 || anum == 8 || anum == 9 || anum == 10)
                SpawnTile(Random.Range(1, 6));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        if(tileIndex != 0)
        {
            if(tileIndex == anum)
            {
                SpawnTile(Random.Range(1, tilePrefabs.Length));
            }

        }
        if(anum != tileIndex)
        {
            GameObject go =Instantiate(tilePrefabs[tileIndex],transform.forward * zSpawn , transform.rotation);
            activeTiles.Add(go);
            zSpawn += tileLength;
            anum = tileIndex;
        }
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
