using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

    public bool debug;

    public int cargo;
    public float fuel, battery, hull;
    public Rigidbody shipRigidBody;
    public GameObject orePrefab;
    public GameObject cargoObject;
    public GameObject lateralThrusters;
    public LowerBodyThrusters thrusters;
    public LateralThruster northThruster, eastThruster, southThruster, westThruster;
    public ConstantForce upwardForce;
    public System.Random random;

    public ToolController toolController;
    public GameObject clawObject;
    public GameObject drillObject; // TODO get these

    public TerrainController terrainController;

    public ArrayList legs;

    // TODO tidy up this messy ass shit

	// Use this for initialization
	void Start () {

        cargo = CARGO_NONE;
        random = new System.Random();

        thrusters       = GameObject.Find("LowerBody").GetComponent<LowerBodyThrusters>();
        northThruster   = lateralThrusters.transform.Find("RcsThrusterNorth").GetComponent<LateralThruster>();
        eastThruster    = lateralThrusters.transform.Find("RcsThrusterEast").GetComponent<LateralThruster>();
        southThruster   = lateralThrusters.transform.Find("RcsThrusterSouth").GetComponent<LateralThruster>();
        westThruster    = lateralThrusters.transform.Find("RcsThrusterWest").GetComponent<LateralThruster>();

        fuel    = MAX_FUEL;
        battery = MAX_BATTERY;
        hull    = MAX_HULL;

        

        legs = new ArrayList();
        foreach (Transform child in gameObject.transform.Find("Legs")) {
            if (child.tag == "Leg"){
                legs.Add(child.gameObject);
            }
        }

        Debug.Log("legs count: " + legs.Count);

	}
	
	// Update is called once per frame
	void Update () {

        Vector3 newRelativeForce = upwardForce.relativeForce;
        Vector3 newGlobalForce = upwardForce.force;

        // zero forces to begin with
        newRelativeForce.x = 0;
        newRelativeForce.z = 0;

        float consumedFuel = 0;
        float lateralFuelConsumption = LATERAL_FORCE * CONSUMPTION_SCALE;
        float upwardFuelConsumption = UPWARD_FORCE * CONSUMPTION_SCALE;

        // set forces if keys are down
        if(Input.GetKey(KeyCode.W) && (fuel > lateralFuelConsumption)){
            newRelativeForce.x = newRelativeForce.x + LATERAL_FORCE;                
            consumedFuel += lateralFuelConsumption;
            southThruster.engage();
        } else {
            southThruster.disengage();
        }

        if(Input.GetKey(KeyCode.S) && (fuel > lateralFuelConsumption)){
            newRelativeForce.x = newRelativeForce.x - LATERAL_FORCE;
            consumedFuel += lateralFuelConsumption;
            northThruster.engage();
        } else {
            northThruster.disengage();
        }

        if(Input.GetKey(KeyCode.A) && (fuel > lateralFuelConsumption)){
            newRelativeForce.z = newRelativeForce.z + LATERAL_FORCE;
            consumedFuel += lateralFuelConsumption;
            eastThruster.engage();
        } else {
            eastThruster.disengage();
        }

        if(Input.GetKey(KeyCode.D) && (fuel > lateralFuelConsumption)){
            newRelativeForce.z = newRelativeForce.z - LATERAL_FORCE;
            consumedFuel += lateralFuelConsumption;
            westThruster.engage();
        } else {
            westThruster.disengage();
        }

        if (Input.GetKey(KeyCode.Space) && (fuel > upwardFuelConsumption)){
            newGlobalForce.y = UPWARD_FORCE;
            consumedFuel += upwardFuelConsumption;
            thrusters.engage();
        } else {
            newGlobalForce.y = 0;
            thrusters.disengage();
        }

        upwardForce.relativeForce = newRelativeForce;
        upwardForce.force = newGlobalForce;

        if(!debug){
            fuel -= consumedFuel;
        }

	}

    public bool hasLanded(){
        int contacts = 0;
        foreach(GameObject leg in legs){
            // if(leg.collider.)
        }
        return (contacts > 2);
    }

    public const float MAX_FUEL     = 100f;
    public const float MAX_BATTERY  = 100f;
    public const float MAX_HULL     = 100f;
    public const float CONSUMPTION_SCALE = 0.005f;

    public const float UPWARD_FORCE = 20f;
    public const float LATERAL_FORCE = 10f;

    public const float SPAWN_OFFSET_Y = -0.5f;

    public const int CARGO_NONE = 0;
    public const int CARGO_ORE = 1;
    public const int CARGO_SALVAGE = 2; 

}
