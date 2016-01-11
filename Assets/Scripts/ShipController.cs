using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

    public float fuel, battery, hull;
    public LowerBodyThrusters thrusters;
    public GameObject lateralThrusters, orePrefab;
    public LateralThruster northThruster, eastThruster, southThruster, westThruster;
    public ConstantForce upwardForce;
    public System.Random random;

	// Use this for initialization
	void Start () {

        random = new System.Random();

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

        float consumedFuel = 0;
        float lateralFuelConsumption = LATERAL_FORCE * CONSUMPTION_SCALE;
        float upwardFuelConsumption = UPWARD_FORCE * CONSUMPTION_SCALE;

        // set forces if keys are down
        if(Input.GetKey(KeyCode.W) && (fuel > lateralFuelConsumption)){
            newForce.x = newForce.x + LATERAL_FORCE;                
            consumedFuel += lateralFuelConsumption;
            southThruster.engage();
        } else {
            southThruster.disengage();
        }

        if(Input.GetKey(KeyCode.S) && (fuel > lateralFuelConsumption)){
            newForce.x = newForce.x - LATERAL_FORCE;
            consumedFuel += lateralFuelConsumption;
            northThruster.engage();
        } else {
            northThruster.disengage();
        }

        if(Input.GetKey(KeyCode.A) && (fuel > lateralFuelConsumption)){
            newForce.z = newForce.z + LATERAL_FORCE;
            consumedFuel += lateralFuelConsumption;
            eastThruster.engage();
        } else {
            eastThruster.disengage();
        }

        if(Input.GetKey(KeyCode.D) && (fuel > lateralFuelConsumption)){
            newForce.z = newForce.z - LATERAL_FORCE;
            consumedFuel += lateralFuelConsumption;
            westThruster.engage();
        } else {
            westThruster.disengage();
        }

        if (Input.GetKey(KeyCode.Space) && (fuel > upwardFuelConsumption)){
            newForce.y = UPWARD_FORCE;
            consumedFuel += upwardFuelConsumption;
            thrusters.engage();
        } else {
            newForce.y = 0;
            thrusters.disengage();
        }

        upwardForce.relativeForce = newForce;
        fuel -= consumedFuel;

        if(Input.GetKey(KeyCode.T)){ // TODO change so it only drops its resource if it has one
            GameObject obj = Instantiate(orePrefab) as GameObject;
            Vector3 spawnPosition = transform.position;
            spawnPosition.y = spawnPosition.y + SPAWN_OFFSET_Y;
            obj.transform.position = spawnPosition;
            float ySpeed = 0;
            Debug.Log("ySpeed: " + ySpeed);
            // Vector3 direction = new Vector3((random.Next(3) - 3), (random.Next(3) + 2), (random.Next(3) - 3));
            Vector3 direction = new Vector3(0, -(random.Next(3) + 2), 0);
            obj.GetComponent<Rigidbody>().AddForce((direction * 1), ForceMode.Impulse);          
        }

	}

    public const float MAX_FUEL     = 100f;
    public const float MAX_BATTERY  = 100f;
    public const float MAX_HULL     = 100f;
    public const float CONSUMPTION_SCALE = 0.005f;

    public const float UPWARD_FORCE = 20f;
    public const float LATERAL_FORCE = 10f;

    public const float SPAWN_OFFSET_Y = -0.5f;

}
