using UnityEngine;
using System.Collections;

public class GuiController : MonoBehaviour {

    public ShipController ship;
    public LandingPadController landingPad;
    public Texture noCargo, metalCargo;
    private int width, height;
    private int cargoItems;

    // shapes
    private Rect shipInfoBox, padInfoBox, cargoInfoBox;

    // Use this for initialization
    void Start () {
        cargoItems = 8;
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
        buildCargoBox();

        if(landingPad.isAttached){
            buildPadBox();
        }

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

        if(landingPad.isAttached){ // TODO move this into buildPadBox
            GUI.Box(padInfoBox, "Base supplies");
            GUI.Label(padInfoTopLabelRect,      "Fuel supplies: "       + landingPad.fuelSupplies);
            GUI.Label(padInfoMiddleLabelRect,   "Battery supplies: "    + landingPad.batterySupplies);
            GUI.Label(padInfoBottomLabelRect,   "Repair supplies: "     + landingPad.repairSupplies);
        }

    }

    private void buildPadBox(){

    }

    private void buildCargoBox(){
        // TODO adapt to cargo size when i get upgrades
        int cargoBoxHeight = (VERTICAL_MARGIN * 4) + (CARGO_IMAGE_SIZE_SQUARE * 2) + (VERTICAL_MARGIN / 2);
        int cargoBoxWidth = (HORIZONTAL_MARGIN * 5) + (CARGO_IMAGE_SIZE_SQUARE * 4);

        cargoInfoBox = new Rect(width - HORIZONTAL_MARGIN - cargoBoxWidth, VERTICAL_MARGIN, cargoBoxWidth, cargoBoxHeight); // TODO cache. only re-calculate on width/height change
        GUI.Box(cargoInfoBox, "Cargo");

        float initialY = (VERTICAL_MARGIN * 3) + (VERTICAL_MARGIN / 2);

        for(int i = 0; i < cargoItems; i++){
            int xRel = i % 4;
            float x = cargoInfoBox.x + HORIZONTAL_MARGIN + (xRel * HORIZONTAL_MARGIN) + (xRel * CARGO_IMAGE_SIZE_SQUARE);
            float y = initialY;
            if(i < 4){ // DRAW FIRST ROW
                // do nothing for now
            } else if(i < 8){ // DRAW SECOND ROW
                y += (VERTICAL_MARGIN * 1) + (CARGO_IMAGE_SIZE_SQUARE * 1);
            } else if(i < 12){
                y += (VERTICAL_MARGIN * 2) + (CARGO_IMAGE_SIZE_SQUARE * 2);
            }
            GUI.DrawTexture(new Rect(x, y, CARGO_IMAGE_SIZE_SQUARE, CARGO_IMAGE_SIZE_SQUARE), noCargo);
        }

        // Rect topLabelRect = new Rect(padLabelX, this.height - infoBoxHeight + VERTICAL_MARGIN*3,             labelWidth, SINGLE_LINE_HEIGHT);
    }

    private const int VERTICAL_MARGIN   = 10;
    private const int HORIZONTAL_MARGIN = 10;
    private const int SINGLE_LINE_HEIGHT = 24;
    private const int BUTTON_WIDTH = 200;
    private const int BUTTON_HEIGHT = 24;
    private const int INFO_BOX_WIDTH = 240;
    private const int INFO_BOX_HEIGHT = 80;
    private const int CARGO_IMAGE_SIZE_SQUARE = 64;

}
