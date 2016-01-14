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

    private float baseLateralFuelConsumption;
    private float baseUpwardFuelConsumption;
    private float baseDrillBatteryConsumption;
    private float baseLateralThrust;
    private float baseUpwardThrust;

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

        baseLateralFuelConsumption  = LATERAL_THRUST_FUEL_USAGE * CONSUMPTION_SCALE;
        baseUpwardFuelConsumption   = UPWARD_THRUST_FUEL_USAGE  * CONSUMPTION_SCALE;
        baseDrillBatteryConsumption = DRILL_BATTERY_USAGE       * CONSUMPTION_SCALE;

        baseLateralThrust           = LATERAL_THRUST            * THRUST_SCALE;
        baseUpwardThrust            = UPWARD_THRUST             * THRUST_SCALE;

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

        float consumedFuel      = 0;
        float consumedBattery   = 0;

        // calculate consumptions
        float lateralThrust             = (Time.deltaTime * baseLateralThrust);
        float lateralFuelConsumption    = (Time.deltaTime * baseLateralFuelConsumption);
        float upwardThrust              = (Time.deltaTime * baseUpwardThrust);
        float upwardFuelConsumption     = (Time.deltaTime * baseUpwardFuelConsumption);
        float batteryDrillConsumption   = (Time.deltaTime * baseDrillBatteryConsumption);

        // set forces if keys are down
        if(Input.GetKey(KeyCode.W) && (fuel > lateralFuelConsumption)){
            newRelativeForce.x = newRelativeForce.x + lateralThrust;                
            consumedFuel += lateralFuelConsumption;
            southThruster.engage();
        } else {
            southThruster.disengage();
        }

        if(Input.GetKey(KeyCode.S) && (fuel > lateralFuelConsumption)){
            newRelativeForce.x = newRelativeForce.x - lateralThrust;
            consumedFuel += lateralFuelConsumption;
            northThruster.engage();
        } else {
            northThruster.disengage();
        }

        if(Input.GetKey(KeyCode.A) && (fuel > lateralFuelConsumption)){
            newRelativeForce.z = newRelativeForce.z + lateralThrust;
            consumedFuel += lateralFuelConsumption;
            eastThruster.engage();
        } else {
            eastThruster.disengage();
        }

        if(Input.GetKey(KeyCode.D) && (fuel > lateralFuelConsumption)){
            newRelativeForce.z = newRelativeForce.z - lateralThrust;
            consumedFuel += lateralFuelConsumption;
            westThruster.engage();
        } else {
            westThruster.disengage();
        }

        if (Input.GetKey(KeyCode.Space) && (fuel > upwardFuelConsumption)){
            newGlobalForce.y = upwardThrust;
            consumedFuel += upwardFuelConsumption;
            thrusters.engage();
        } else {
            newGlobalForce.y = 0;
            thrusters.disengage();
        }

        // get consumed battery
        if(toolController.isDrilling()){
            consumedBattery += batteryDrillConsumption;
        }

        // DON'T USE DELTA TIME BELOW THIS POINT
        upwardForce.relativeForce = newRelativeForce;
        upwardForce.force = newGlobalForce;

        // apply resource expenditures
        if(debug){
            fuel -= consumedFuel;
            battery -= consumedBattery;
        }

	}

    public bool hasLanded(){
        return terrainController.hasLanded();
    }

    public const float MAX_FUEL     = 100f;
    public const float MAX_BATTERY  = 100f;
    public const float MAX_HULL     = 100f;

    public const float THRUST_SCALE     = 10f;
    public const float UPWARD_THRUST    = 100f;
    public const float LATERAL_THRUST   = 50f;

    public const float CONSUMPTION_SCALE = 0.05f;
    public const float UPWARD_THRUST_FUEL_USAGE     = 100f; // TODO decouple thrust force amount and consumption rate
    public const float LATERAL_THRUST_FUEL_USAGE    = 50f;
    public const float DRILL_BATTERY_USAGE          = 20f;

    public const float SPAWN_OFFSET_Y = -0.5f;

    public const int CARGO_NONE         = 0;
    public const int CARGO_ORE          = 1;
    public const int CARGO_SALVAGE      = 2;
    public const int CARGO_METAL        = 3;
    public const int CARGO_CRUDE_OIL    = 4;
    public const int CARGO_FUEL         = 5;

}
