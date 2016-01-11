using UnityEngine;
using System.Collections;

public class GuiController : MonoBehaviour {

    public ShipController ship;
    private int width, height;

    // shapes
    private Rect infoBox;

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

        // box
        int infoBoxHeight = (VERTICAL_MARGIN * 4) + (SINGLE_LINE_HEIGHT * 3);
        infoBox = new Rect(HORIZONTAL_MARGIN, VERTICAL_MARGIN, INFO_BOX_WIDTH, infoBoxHeight);
        

        // labels
        float x = infoBox.x + HORIZONTAL_MARGIN;
        float labelWidth = INFO_BOX_WIDTH - (HORIZONTAL_MARGIN*2);

        // create rect
        Rect topLabelRect       = new Rect(x, infoBox.y + VERTICAL_MARGIN*2,         labelWidth, SINGLE_LINE_HEIGHT);
        Rect middleLabelRect    = new Rect(x, topLabelRect.y + VERTICAL_MARGIN*2,    labelWidth, SINGLE_LINE_HEIGHT);
        Rect bottomLabelRect    = new Rect(x, middleLabelRect.y + VERTICAL_MARGIN*2, labelWidth, SINGLE_LINE_HEIGHT);

        // update info box y max
        infoBox.yMax = bottomLabelRect.yMax + VERTICAL_MARGIN;

        // create the GUIs
        GUI.Box(infoBox, "Parameters");
        GUI.Label(topLabelRect,     "Fuel: "        + ship.fuel);
        GUI.Label(middleLabelRect,  "Battery: "     + ship.battery);
        GUI.Label(bottomLabelRect,  "Integrity: "   + ship.hull);

    }

    private const int VERTICAL_MARGIN   = 10;
    private const int HORIZONTAL_MARGIN = 10;
    private const int SINGLE_LINE_HEIGHT = 24;
    private const int BUTTON_WIDTH = 200;
    private const int BUTTON_HEIGHT = 24;
    private const int INFO_BOX_WIDTH = 240;
    private const int INFO_BOX_HEIGHT = 80;

}
