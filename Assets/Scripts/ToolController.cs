using UnityEngine;
using System.Collections;

public class ToolController : MonoBehaviour {

    public int toolSelected;
    public int toolStatus;
    public bool toolLocked, hasCargo;
    public float toolOffsetY;
    public Transform drill, claw;

	// Use this for initialization
	void Start () {

        toolOffsetY = 0;
        toolSelected = TOOL_DRILL;
        toolStatus = TOOL_RETRACTED;
        toolLocked = false;
        hasCargo = false;

    }
    
    // Update is called once per frame
    void Update () {

        if(!toolLocked){
			if(Input.GetKey(KeyCode.Alpha1) && toolSelected != TOOL_DRILL){
                toolStatus = TOOL_SWITCHING_TO_DRILL;
			} else if(Input.GetKey(KeyCode.Alpha2) && toolSelected != TOOL_CLAW){
                toolStatus = TOOL_SWITCHING_TO_CLAW;
            }

            if(Input.GetKey(KeyCode.LeftShift) && (toolStatus != TOOL_SWITCHING_TO_DRILL) && (toolStatus != TOOL_SWITCHING_TO_CLAW)){
                toolStatus = TOOL_DEPLOYED;
            } else if((toolStatus != TOOL_SWITCHING_TO_DRILL) && (toolStatus != TOOL_SWITCHING_TO_CLAW)){
                toolStatus = TOOL_RETRACTED;
            }

        }

        float deployAmount = Time.deltaTime * TOOL_DEPLOY_SPEED;

        switch(toolStatus){
            case TOOL_RETRACTED:
            case TOOL_SWITCHING_TO_CLAW:
            case TOOL_SWITCHING_TO_DRILL:
                toolOffsetY -= deployAmount;
                break;
            case TOOL_DEPLOYED:
                toolOffsetY += deployAmount;
                break;
        }

        if(toolOffsetY <= 0){ // TODO make this tool offset just switch the model?
            toolOffsetY = 0;
            switch(toolStatus){
                case TOOL_SWITCHING_TO_CLAW:
                    toolSelected = TOOL_CLAW;
                    toolStatus = TOOL_RETRACTED;
                    break;
                case TOOL_SWITCHING_TO_DRILL:
                    toolSelected = TOOL_DRILL;
                    toolStatus = TOOL_RETRACTED;
                    break;
                }
        } else if(toolOffsetY > TOOL_OFFSET_MAX){
            toolOffsetY = TOOL_OFFSET_MAX;
        }

        Vector3 newToolPosition = new Vector3(0, -toolOffsetY, 0);

        if(TOOL_DRILL == toolSelected){
            drill.localPosition = newToolPosition;
        } else if(TOOL_CLAW == toolSelected){
            claw.localPosition = newToolPosition;
        }

	}

    public const int TOOL_DRILL                 = 1;
    public const int TOOL_CLAW                  = 2;
    public const int TOOL_DEPLOYED              = 11;
    public const int TOOL_RETRACTED             = 12;
    public const int TOOL_SWITCHING_TO_CLAW     = 13;
    public const int TOOL_SWITCHING_TO_DRILL    = 14;
    public const float TOOL_OFFSET_MAX          = 0.85f;
    public const float TOOL_DEPLOY_SPEED        = 1.2f;

}
