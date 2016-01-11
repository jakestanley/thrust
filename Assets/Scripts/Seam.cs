using UnityEngine;
using System.Collections;

public class Seam : MonoBehaviour {

    public System.Random random;
    public GameObject seam;
    public GameObject orePrefab;

	// Use this for initialization
	void Start () {
        random = new System.Random();
	}
	
	// Update is called once per frame
	void Update () {
        if(random.Next(30) == 1){
            GameObject obj = Instantiate(orePrefab) as GameObject;
            Vector3 spawnPosition = seam.transform.position;
            spawnPosition.y = spawnPosition.y + SPAWN_OFFSET_Y;
            obj.transform.position = spawnPosition;
            float ySpeed = 0;
            Debug.Log("ySpeed: " + ySpeed);
            // Vector3 direction = new Vector3((random.Next(3) - 3), (random.Next(3) + 2), (random.Next(3) - 3));
            Vector3 direction = new Vector3(0, (random.Next(3) + 2), 0);
            obj.GetComponent<Rigidbody>().AddForce((direction * 1), ForceMode.Impulse);            
        }
	}

    public const float SPAWN_OFFSET_Y = 1.5f;
}
