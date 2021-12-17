using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject despawner;
    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        despawner = GameObject.Find("Despawner");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(GameManager.Instance.speed * Time.deltaTime, 0, 0);
        //rigidBody.AddForce(transform.right * -1 * GameManager.Instance.speed);

        if(transform.position.x < despawner.transform.position.x)
        {
            Destroy(this.gameObject);
        }
    }
}
