using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float buildingGap;
    public GameObject building;
    public GameObject hero;
    public GameObject currentBuilding;
    public GameObject lastBuilding;
    public GameObject satellite;
    public GameObject box;
    public GameObject despawner;
    public Vector3 currentBuildingSize;
    public Vector3 lastBuildingSize;

    string[] differentBuildings = new string[5];

    // Start is called before the first frame update
    void Start()
    {
        differentBuildings[0] = "building_1";
        differentBuildings[1] = "building_2";
        differentBuildings[2] = "building_3";
        differentBuildings[3] = "building_4";
        differentBuildings[4] = "building_5";
        buildingGap = .1f;
        SpawnBuilding();
    }

    // Update is called once per frame
    void Update()
    {

        BoxCollider2D currentBuildingCol = currentBuilding.GetComponent<BoxCollider2D>();
        //right most point of building
        float rightMostPoint = currentBuilding.transform.position.x + currentBuildingCol.bounds.extents.x;

        if (rightMostPoint < this.transform.position.x)
        {
            //Create a new building
            SpawnBuilding();
            //Has a 1/8 chance to spawn a satellite every time a building is spawned
            int satelliteChance = Random.Range(0, 5);
            if(satelliteChance == 4)
            {
                SpawnSatellite();
            }
        }
    }

    void SpawnBuilding()
    {

        //Figure out a good "y" position.
        //have it be relative to the previous building
        float currentBuildingY = currentBuilding.transform.position.y;
        //Pick a random x value to give randomness to the width of buildings
        float buildingXScale = Random.RandomRange(.6f, 1.5f);


        //futz with it a little
        float adjustment = Random.RandomRange(-.5f, .5f);
        buildingGap = Random.RandomRange(4f, 6.5f);

        //TODO: make sure building doesn't fall off the side of the screen.
        float newBuildingY = currentBuildingY + adjustment;

        Vector3 spawnPosition = new Vector3(0, newBuildingY, this.transform.position.z);
        //Vector3 spawnPosition = new Vector3(this.transform.position.x, newBuildingY, this.transform.position.z);

        BoxCollider2D lastBuildingCol = lastBuilding.GetComponent<BoxCollider2D>();
        lastBuildingSize = lastBuildingCol.size;

        //We get a random index of our building array to use later
        int randomIndex = Random.Range(0, differentBuildings.Length);

        //We spawn a building based on our randomIndex and then we get it's collider to move it specifically where we want it
        currentBuilding = Instantiate(Resources.Load<GameObject>(differentBuildings[randomIndex]), spawnPosition, Quaternion.identity);
        BoxCollider2D currentBuildingCol = currentBuilding.GetComponent<BoxCollider2D>();
        currentBuilding.transform.position = new Vector3(((lastBuilding.transform.position.x + lastBuildingCol.bounds.extents.x) + currentBuildingCol.bounds.extents.x) + buildingGap, currentBuilding.transform.position.y, currentBuilding.transform.position.z);

        //Passes in the new building width calculated earlier
        //currentBuilding.transform.localScale = new Vector2(currentBuilding.transform.localScale.x, currentBuilding.transform.localScale.y);
        SpriteRenderer renderer = currentBuilding.GetComponent<SpriteRenderer>();
        //Attempting to use the box collider to get an accurate size

        //We get a random number between 0 and 3 and if that number is 2 we spawn a box
        int boxChance = Random.Range(0, 3);
        if(boxChance == 2)
        {
            SpawnBox(renderer, newBuildingY);
        }

        //The building we were creating becomes the last building for the next iteration
        lastBuilding = currentBuilding;
    }

    void SpawnSatellite()
    {
        //Spawns a satellite in some range from the despawner to the spawner 
        Vector2 spawnPosition = new Vector2(Random.Range(this.transform.position.x, despawner.transform.position.x + 35), this.transform.position.y + 15f);
        Instantiate(satellite, spawnPosition, Quaternion.identity);
    }

    //Spawns a box on top of the currentbuilding with a random x taken from the buildings spriterenderer bounds
    void SpawnBox(SpriteRenderer sRenderer, float buildingY)
    {
        Vector2 boxPosition = new Vector2(Random.Range(sRenderer.bounds.min.x, sRenderer.bounds.max.x - 1f), buildingY + .5f);
        Instantiate(box, boxPosition, Quaternion.identity);
    }
}
