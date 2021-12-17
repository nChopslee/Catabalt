using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float buildingGap;
    public GameObject building;
    public GameObject hero;
    public GameObject currentBuilding;
    public GameObject satellite;
    public GameObject box;
    public GameObject despawner;
    public Vector3 currentBuildingSize;


    // Start is called before the first frame update
    void Start()
    {
        buildingGap = 2f;
        SpawnBuilding();
    }

    // Update is called once per frame
    void Update()
    {

        //right most point of building
        float rightMostPoint = currentBuilding.transform.position.x + currentBuildingSize.x + buildingGap;

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
        float buildingXScale = Random.RandomRange(3f, 20f);


        //futz with it a little
        float adjustment = Random.RandomRange(-2.5f, 2.5f);
        buildingGap = Random.RandomRange(2f, 4f);

        //TODO: make sure building doesn't fall off the side of the screen.
        float newBuildingY = currentBuildingY + adjustment;


        Vector3 spawnPosition = new Vector3(this.transform.position.x, newBuildingY, this.transform.position.z);
        currentBuilding = Instantiate(building, spawnPosition, Quaternion.identity);
        //Passes in the new building width calculated earlier
        currentBuilding.transform.localScale = new Vector2(buildingXScale, currentBuilding.transform.localScale.y);
        SpriteRenderer renderer = currentBuilding.GetComponent<SpriteRenderer>();
        currentBuildingSize = renderer.bounds.size;
        //We get a random number between 0 and 3 and if that number is 2 we spawn a box
        int boxChance = Random.Range(0, 3);
        if(boxChance == 2)
        {
            SpawnBox(renderer, newBuildingY);
        }
        /*Vector2 boxPosition = new Vector2( Random.Range(renderer.bounds.min.x ,renderer.bounds.max.x - 1f), newBuildingY + .5f);
        Instantiate(box, boxPosition, Quaternion.identity);*/
    }

    void SpawnSatellite()
    {
        //Spawns a satellite in some range from the despawner to the spawner 
        Vector2 spawnPosition = new Vector2(Random.Range(this.transform.position.x, despawner.transform.position.x + 35), this.transform.position.y + 15f);
        Instantiate(satellite, spawnPosition, Quaternion.identity);
    }

    //Spawns a box on top of the currentbuilding with a random x taken from the buildings spriterenderer bounds
    //Right now the box just acts as an object that needs to be jumped over but when speed is added it will need to be changed
    //to lower the players speed then despawn if the player hits it.
    void SpawnBox(SpriteRenderer sRenderer, float buildingY)
    {
        Vector2 boxPosition = new Vector2(Random.Range(sRenderer.bounds.min.x, sRenderer.bounds.max.x - 1f), buildingY + .5f);
        Instantiate(box, boxPosition, Quaternion.identity);
    }
}
