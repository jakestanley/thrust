using UnityEngine;
using System.Collections;

public class GuiController : MonoBehaviour {

    public ShipController ship;
    public LandingPadController landingPad;
    private int width, height;

    // shapes
    private Rect shipInfoBox, padInfoBox;

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    // Render GUI
    void OnGUI(){

        // set resolution variables
        width = Screen.width;
        height = Screen.height;

        // set boxStyle
        // boxStyle = new GUIStyle(GUI.skin.box);
        // boxStyle.font = font;

        // set buttonStyle
        // buttonStyle = new GUIStyle(GUI.skin.button);
        // buttonStyle.font = font;
        
        buildInfoBox();

    }

    private void buildInfoBox(){

        // boxes
        int infoBoxHeight = (VERTICAL_MARGIN * 4) + (SINGLE_LINE_HEIGHT * 3);

        shipInfoBox = new Rect( HORIZONTAL_MARGIN, 
                                VERTICAL_MARGIN, 
                                INFO_BOX_WIDTH, 
                                infoBoxHeight);

        padInfoBox  = new Rect( width - HORIZONTAL_MARGIN - INFO_BOX_WIDTH, 
                                height - infoBoxHeight - VERTICAL_MARGIN, 
                                INFO_BOX_WIDTH, 
                                infoBoxHeight);
        
        // labels
        float x = shipInfoBox.x + HORIZONTAL_MARGIN;
        float labelWidth = INFO_BOX_WIDTH - (HORIZONTAL_MARGIN*2);
        float padLabelX = padInfoBox.x + HORIZONTAL_MARGIN;

        // create rects
        Rect topLabelRect       = new Rect(x, shipInfoBox.y + VERTICAL_MARGIN*2,     labelWidth, SINGLE_LINE_HEIGHT);
        Rect middleLabelRect    = new Rect(x, topLabelRect.y + VERTICAL_MARGIN*2,    labelWidth, SINGLE_LINE_HEIGHT);
        Rect bottomLabelRect    = new Rect(x, middleLabelRect.y + VERTICAL_MARGIN*2, labelWidth, SINGLE_LINE_HEIGHT);

        Rect padInfoTopLabelRect       = new Rect(padLabelX, this.height - infoBoxHeight + VERTICAL_MARGIN*3,             labelWidth, SINGLE_LINE_HEIGHT);
        Rect padInfoMiddleLabelRect    = new Rect(padLabelX, padInfoTopLabelRect.y + VERTICAL_MARGIN*2,    labelWidth, SINGLE_LINE_HEIGHT);
        Rect padInfoBottomLabelRect    = new Rect(padLabelX, padInfoMiddleLabelRect.y + VERTICAL_MARGIN*2, labelWidth, SINGLE_LINE_HEIGHT);

        // update info box y max
        shipInfoBox.yMax = bottomLabelRect.yMax + VERTICAL_MARGIN;
        padInfoBox.yMax = padInfoBottomLabelRect.yMax - VERTICAL_MARGIN;
        padInfoBox.height = padInfoBox.yMax - padInfoBox.y;
        padInfoBox.y += VERTICAL_MARGIN*2;

        // create the GUIs
        GUI.Box(shipInfoBox, "Ship supplies");
        GUI.Label(topLabelRect,     "Fuel: "        + ship.fuel);
        GUI.Label(middleLabelRect,  "Battery: "     + ship.battery);
        GUI.Label(bottomLabelRect,  "Integrity: "   + ship.hull);

        if(landingPad.isAttached){
            GUI.Box(padInfoBox, "Base supplies");
            GUI.Label(padInfoTopLabelRect,      "Fuel supplies: "       + landingPad.fuelSupplies);
            GUI.Label(padInfoMiddleLabelRect,   "Battery supplies: "    + landingPad.batterySupplies);
            GUI.Label(padInfoBottomLabelRect,   "Repair supplies: "     + landingPad.repairSupplies);
        }

    }

    private const int VERTICAL_MARGIN   = 10;
    private const int HORIZONTAL_MARGIN = 10;
    private const int SINGLE_LINE_HEIGHT = 24;
    private const int BUTTON_WIDTH = 200;
    private const int BUTTON_HEIGHT = 24;
    private const int INFO_BOX_WIDTH = 240;
    private const int INFO_BOX_HEIGHT = 80;

}
