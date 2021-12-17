using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomSpawner : MonoBehaviour
{
    public List<GameObject> buildingList;
    public GameObject[] buildingListArray;
    public float buildingGap;
    public GameObject building;
    public GameObject hero;
    public GameObject currentBuilding;
    public Vector3 currentBuildingSize;


    // Start is called before the first frame update
    void Start()
    {
        buildingGap = -1f;
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
        }
    }

    void SpawnBuilding()
    {

        //Figure out a good "y" position.
        //have it be relative to the previous building
        float currentBuildingY = currentBuilding.transform.position.y;

        //futz with it a little
        float adjustment = Random.RandomRange(-2f, 2f);
        //buildingGap = Random.RandomRange(2f, 4f);

        //TODO: make sure building doesn't fall off the side of the screen.
        float newBuildingY = currentBuildingY; // + adjustment;

        
        Vector3 spawnPosition = new Vector3(this.transform.position.x, newBuildingY, this.transform.position.z);

        buildingListArray = Resources.LoadAll<GameObject>("Buildings");
        buildingList = buildingListArray.ToList();
        GameObject buildingToSpawn = buildingList[Random.RandomRange(0, buildingList.Count)];
        currentBuilding = Instantiate(buildingToSpawn, spawnPosition, Quaternion.identity) as GameObject;

        SpriteRenderer renderer = currentBuilding.GetComponent<SpriteRenderer>();
        currentBuildingSize = renderer.bounds.size;
    }
}
