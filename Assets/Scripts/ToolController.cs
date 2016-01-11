using UnityEngine;
using System.Collections;

public class ToolController : MonoBehaviour {

    public int tool;
    public int toolStatus;
    public bool toolLocked;
    public float drillInitialOffsetY, drillOffsetY, clawInitialOffsetY, clawOffsetY, maxFuckingOffsetY, minFuckingOffsetY;
    public Transform drill, claw;

	// Use this for initialization
	void Start () {

        drillOffsetY = 0;
        clawOffsetY = 0;

        tool = TOOL_DRILL;
        toolStatus = TOOL_RETRACTED;
        toolLocked = false;

        maxFuckingOffsetY  = drill.transform.position.y;
        minFuckingOffsetY  = maxFuckingOffsetY - MAX_OFFSET_Y_DRILL;

    }
    
    // Update is called once per frame
    void Update () {

        drillInitialOffsetY  = drill.transform.position.y;
        clawInitialOffsetY   = claw.transform.position.y;

        if(Input.GetKey(KeyCode.LeftShift) && !toolLocked){
            toolStatus = TOOL_DEPLOYED;
        } else {
            toolStatus = TOOL_RETRACTED;
        }

        float increaseAmount = Time.deltaTime * TOOL_DEPLOY_SPEED;

        if(TOOL_DRILL == tool){
            switch(toolStatus){
                case TOOL_RETRACTED:
                    drillOffsetY = - increaseAmount;
                    break;
                case TOOL_DEPLOYED:
                    drillOffsetY = + increaseAmount;
                    break;
            }
        } else if(TOOL_CLAW == tool){
            // TODO later
        }

        if(drillOffsetY < 0){
            drillOffsetY = 0;
        } else if(drillOffsetY > MAX_OFFSET_Y_DRILL){
            drillOffsetY = MAX_OFFSET_Y_DRILL;
        }

        // drill.transform.position = new Vector3(drillInitialOffset.x, drillInitialOffset.y - drillOffsetY, drillInitialOffset.z);
        // float totalDrillOffset = drillInitialOffsetY + drillOffsetY;
        Vector3 newDrillPosition = drill.transform.position;
        float newY = drillInitialOffsetY - drillOffsetY;
        if(newY > maxFuckingOffsetY){
            newY = maxFuckingOffsetY;
        } else if(newY < minFuckingOffsetY){
            newY = minFuckingOffsetY;
        }
        newDrillPosition.y = newY;


        // drill.transform.position = newDrillPosition;

	}

    public const int TOOL_DRILL   = 1;
    public const int TOOL_CLAW    = 2;
    public const float MAX_OFFSET_Y_DRILL = 0.7f;
    public const float MAX_OFFSET_Y_CLAW = 1f;

    public const int TOOL_DEPLOYED    = 11;
    public const int TOOL_RETRACTED   = 12;
    public const float TOOL_DEPLOY_SPEED = 4f;

}
