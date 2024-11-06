using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile hay, stones, torch, tree, pole, fence, bone;
    public Tile[] roads;

  
    public float timer;
    Vector3Int tileCenter;
    Vector3Int newPos;
    Vector3Int placedChurch;

    public List<Vector3Int> placedTilePositions = new List<Vector3Int>();
    public float minDistance;

    public GameObject graveYard;
    public GameObject church;
    public GameObject torchLight;

    public Transform graveManager;
    public GraveManager graveManagerScript;
    public List<Vector3Int> placedGraveYards = new List<Vector3Int>();
    // Start is called before the first frame update
    void Start()
    {

        PlaceChurchAndRoads();
        PlaceRandomRoads();
        PlaceGraveYards();
        PlaceTile(hay, 2, 5);
        PlaceTile(stones, 10, 15);
        PlaceTile(torch, 3, 5);
        PlaceTile(tree, 10, 14);
        PlaceTile(pole, 3, 5);
        PlaceTile(fence, 2, 5);
        PlaceTile(bone, 7, 9);
       

    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    void PlaceChurchAndRoads()
    {
        
        //placing church
        Debug.Log("starting church");
          
            

        
        int randomx = Random.Range(-4, 13);
        int randomy = Random.Range(-2, 0);
        float adjustx = 0.4f;
        newPos = new Vector3Int(randomx, randomy, 0);
        placedGraveYards.Add(newPos);

        Vector3 adjustPos = new Vector3(randomx + adjustx, randomy, 0);
        GameObject newChurch = Instantiate(church, adjustPos, Quaternion.identity);
       

        //placing roads to church
        int startPosx = 4;
        int startPosy = -9;
        newPos = new Vector3Int(startPosx, startPosy, 0);
        int randomRoad = Random.Range(0, roads.Length);
        tilemap.SetTile(newPos, roads[randomRoad]);
        placedTilePositions.Add(newPos);

        while (startPosy < randomy - 6)
        {
            startPosy++;
            newPos = new Vector3Int(startPosx, startPosy, 0);
            randomRoad = Random.Range(0, roads.Length);
            tilemap.SetTile(newPos, roads[randomRoad]);
            placedTilePositions.Add(newPos);

        }
        if (randomx < 4)
        {
            while (startPosx > randomx)
            {
                startPosx--;
                newPos = new Vector3Int(startPosx, startPosy, 0);
                randomRoad = Random.Range(0, roads.Length);
                tilemap.SetTile(newPos, roads[randomRoad]);
                placedTilePositions.Add(newPos);
            }
        }
        if (randomx > 4)
        {
            while (startPosx < randomx)
            {
                startPosx++;
                newPos = new Vector3Int(startPosx, startPosy, 0);
                randomRoad = Random.Range(0, roads.Length);
                tilemap.SetTile(newPos, roads[randomRoad]);
                placedTilePositions.Add(newPos);
            }
        }
        while (startPosy < randomy - 3)
        {
            startPosy++;
            newPos = new Vector3Int(startPosx, startPosy, 0);
            randomRoad = Random.Range(0, roads.Length);
            tilemap.SetTile(newPos, roads[randomRoad]);
            placedTilePositions.Add(newPos);

        }

        for (int i = 0; i < 2; i++)
        {
            startPosy -= i;

            startPosx += 1;
            newPos = new Vector3Int(startPosx, startPosy, 0);
            randomRoad = Random.Range(0, roads.Length);
            tilemap.SetTile(newPos, roads[randomRoad]);
            placedTilePositions.Add(newPos);

            startPosx -= 2;
            newPos = new Vector3Int(startPosx, startPosy, 0);
            randomRoad = Random.Range(0, roads.Length);
            tilemap.SetTile(newPos, roads[randomRoad]);
            placedTilePositions.Add(newPos);

            startPosx += 1;

        }

        //placing random roads
        int randomAmountRoads = Random.Range(4, 10);
        int randomDirection = Random.Range(-1, 1);
        for (int i = 0; i <= randomAmountRoads; i++)
        {
            if (randomDirection < 0)
            {
                startPosx++;
            }
            else
            {
                startPosx--;
            }

            newPos = new Vector3Int(startPosx, startPosy, 0);
            randomRoad = Random.Range(0, roads.Length);
            tilemap.SetTile(newPos, roads[randomRoad]);
            placedTilePositions.Add(newPos);

            if (startPosx < -4 || startPosx > 13)
            {
                break;
            }

            
        }
        randomAmountRoads = Random.Range(5, 15);
        for (int i = 0; i <= randomAmountRoads; i++)
        {
            if (randomDirection < 0)
            {
                startPosy++;
            }

            newPos = new Vector3Int(startPosx, startPosy, 0);
            randomRoad = Random.Range(0, roads.Length);
            tilemap.SetTile(newPos, roads[randomRoad]);
            placedTilePositions.Add(newPos);

            if (startPosy < -7 || startPosy > 13)
            {
                break;
            }

            
        }



    }

    void PlaceRandomRoads()
    {
       
    }
    void PlaceGraveYards()
    {
        int randAmountYards = Random.Range(3, 4);
        for (int i = 0; i <= randAmountYards; i++)
        {
            Debug.Log("starting graveyard");
            minDistance = 10f;
            int attempts = 0;
            bool validPos = false;
            while (validPos != true && attempts < 100)
            {

                int randomx = Random.Range(-4, 13);
                int randomy = Random.Range(-2, 14);
                newPos = new Vector3Int(randomx, randomy, 0);
                validPos = true;

                foreach (Vector3Int pos in placedGraveYards)
                {
                    if (Vector3Int.Distance(newPos, pos) < minDistance)
                    {
                        
                        validPos = false;
                        break;
                    }
                }
                
                attempts++;

            }

            print (attempts);

            if (validPos)
            {
                GameObject newGraveYard = Instantiate(graveYard, newPos, Quaternion.identity);
                newGraveYard.transform.SetParent(graveManager);
                placedGraveYards.Add(newPos);
            }
           

        }
        graveManagerScript.SetAsChild();
    }
    void PlaceTile(Tile tile, int range1, int range2)
    {
        print("starting tilePlacement");
        int RandomAmount = Random.Range(range1, range2);
        print (RandomAmount);
        for (int i = 0; i < RandomAmount; i++)
        {
            bool validYard = false;
            bool validTile = false;
            int attempts = 0;
            int minDistance = 5;
           
            while (!validTile && attempts < 100 || !validYard && attempts < 100)
            {

                int randomx = Random.Range(-7, 16);
                int randomy = Random.Range(-10, 16);
                newPos = new Vector3Int(randomx, randomy, 0);
                validTile = true;
                validYard = true;

                foreach (Vector3Int pos in placedTilePositions)
                {
                    if (newPos == pos)
                    {
                        validTile = false;
                        break;

                    }

                }
                foreach (Vector3Int gravepos in placedGraveYards)
                {
                    if (Vector3Int.Distance(newPos, gravepos) < minDistance)
                    {
                        validYard = false;
                        break;
                    }
                }
                attempts ++;

            }

            print(attempts);
                if (validTile && validYard)
                {
                    tilemap.SetTile(newPos, tile);
                    placedTilePositions.Add(newPos);
                    if (tile == torch)
                    {
                    Vector3 adjustedPos = new Vector3(newPos.x + 0.4f, newPos.y + 0.8f, 0);
                        Instantiate(torchLight, adjustedPos, Quaternion.identity);
                    }

                }
               
            
        }
    }
}
