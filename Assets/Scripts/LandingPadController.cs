using UnityEngine;
using System.Collections;

public class LandingPadController : MonoBehaviour {

    public float fuelSupplies, batterySupplies, repairSupplies;

    public Collider shipCollider;
    public Collider landingPadCollider;
    public ShipController shipController;
    public bool isAttached;


	// Use this for initialization
	void Start () {
        isAttached = false;
	}
	
	// Update is called once per frame
	void Update () {

        // replenish the ship
        if(isAttached){

            // get required amounts
            float fuelAmountRequired        = ShipController.MAX_FUEL       - shipController.fuel;
            float batteryRechargeRequired   = ShipController.MAX_BATTERY    - shipController.battery;
            float repairAmountRequired      = ShipController.MAX_HULL       - shipController.hull;

            // TODO make sure you only recharge/refuel/repair/whatever the amount we have. don't charge 10 if we've only got 5 supplies

            float fuelAmountExpended            = 0;
            float batteryRechargeExpended       = 0;
            float repairSuppliesExpended        = 0;

            if(fuelAmountRequired >= REFUEL_RATE){
                fuelAmountExpended = REFUEL_RATE;
            } else {
                fuelAmountExpended = fuelAmountRequired;
            }

            if(batteryRechargeRequired >= RECHARGE_RATE){
                batteryRechargeExpended = RECHARGE_RATE;
            } else {
                batteryRechargeExpended = batteryRechargeRequired;
            }

            if(repairAmountRequired >= REPAIR_RATE){
                repairSuppliesExpended = REPAIR_RATE;
            } else {
                repairSuppliesExpended = repairAmountRequired;
            }

            if(fuelAmountExpended > fuelSupplies){
                fuelAmountExpended = fuelSupplies;
            }

            if(batteryRechargeExpended > batterySupplies){
                batteryRechargeExpended = batterySupplies;
            }

            if(repairSuppliesExpended > repairSupplies){
                repairSuppliesExpended = repairSupplies;
            }

            shipController.fuel += fuelAmountExpended;
            shipController.battery += batteryRechargeExpended;
            shipController.hull += repairSuppliesExpended;

            fuelSupplies    -= fuelAmountExpended;
            batterySupplies -= batteryRechargeExpended;
            repairSupplies  -= repairSuppliesExpended;

        }

        // TODO stop the relevant animations if at max (or if we've run out)
        // TODO restart the animations if the levels get back up

    }


    // collider
    void OnCollisionEnter(Collision collision){

        // TODO start the relevant animations

        Debug.Log("Collision enter");
        isAttached = true;
    }

    void OnCollisionExit(Collision collision){
        Debug.Log("Collision leave");
        // if(col == shipCollider){
            // Debug.Log("Landed");
        // }
        isAttached = false;
    }

    private const float REFUEL_RATE = 0.2f;
    private const float RECHARGE_RATE = 0.1f;
    private const float REPAIR_RATE = 0.05f;



}
