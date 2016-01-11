using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

    public float fuel, battery, hull;
    public LowerBodyThrusters thrusters;
    public GameObject lateralThrusters;
    public LateralThruster northThruster, eastThruster, southThruster, westThruster;
    public ConstantForce upwardForce;

	// Use this for initialization
	void Start () {
        thrusters       = GameObject.Find("LowerBody").GetComponent<LowerBodyThrusters>();
        northThruster   = lateralThrusters.transform.Find("RcsThrusterNorth").GetComponent<LateralThruster>();
        eastThruster    = lateralThrusters.transform.Find("RcsThrusterEast").GetComponent<LateralThruster>();
        southThruster   = lateralThrusters.transform.Find("RcsThrusterSouth").GetComponent<LateralThruster>();
        westThruster    = lateralThrusters.transform.Find("RcsThrusterWest").GetComponent<LateralThruster>();

        fuel    = MAX_FUEL;
        battery = MAX_BATTERY;
        hull    = MAX_HULL;
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 newForce = upwardForce.relativeForce;

        // zero forces to begin with
        newForce.x = 0;
        newForce.z = 0;

        // set forces if keys are down
        if(Input.GetKey(KeyCode.W)){
            newForce.x = newForce.x + LATERAL_FORCE;
            southThruster.engage();
        } else {
            southThruster.disengage();
        }

        if(Input.GetKey(KeyCode.S)){
            newForce.x = newForce.x - LATERAL_FORCE;
            northThruster.engage();
        } else {
            northThruster.disengage();
        }

        if(Input.GetKey(KeyCode.A)){
            newForce.z = newForce.z + LATERAL_FORCE;
            eastThruster.engage();
        } else {
            eastThruster.disengage();
        }

        if(Input.GetKey(KeyCode.D)){
            newForce.z = newForce.z - LATERAL_FORCE;
            westThruster.engage();
        } else {
            westThruster.disengage();
        }

        if (Input.GetKey(KeyCode.Space)){
            newForce.y = UPWARD_FORCE;
            thrusters.engage();
        } else {
            newForce.y = 0;
            thrusters.disengage();
        }

        upwardForce.relativeForce = newForce;

	}

    public const float MAX_FUEL     = 100;
    public const float MAX_BATTERY  = 100;
    public const float MAX_HULL     = 100;

    public const float UPWARD_FORCE = 20f;
    public const float LATERAL_FORCE = 10f;



}
