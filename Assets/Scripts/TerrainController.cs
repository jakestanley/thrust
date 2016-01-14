using UnityEngine;
using System.Collections;

public class TerrainController : MonoBehaviour {

    public int legsTouching;
    public int landCountdown;

	// Use this for initialization
	void Start () {
	   legsTouching = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnCollisionEnter(Collision other){
        if(other.collider.gameObject.tag == "Leg"){
            legsTouching++;
        }
    }

    void OnCollisionExit(Collision other){
        if(other.collider.gameObject.tag == "Leg"){
            legsTouching--;
        }
    }

    public bool hasLanded(){
        return (legsTouching > 2);
    }

}
