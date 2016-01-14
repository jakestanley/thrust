using UnityEngine;
using System.Collections;

public class CollectOnCollision : MonoBehaviour {

    public ToolController toolController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    void OnCollisionEnter(Collision other){

        Debug.Log("other.gameObject.tag: " + other.gameObject.tag);

        if(other.gameObject.tag == "Collectable" && !toolController.hasCargo
            && toolController.toolSelected == ToolController.TOOL_CLAW 
            && toolController.toolStatus == ToolController.TOOL_DEPLOYED){
            Debug.Log("Claw collided with a collectable");
            Debug.Log(other.gameObject.transform.localPosition);

            Vector3 localPosition = other.gameObject.transform.localPosition;
            localPosition.y = 7.5f;
            other.gameObject.transform.localPosition = localPosition;
            other.gameObject.transform.SetParent(toolController.claw);
        }

        if(!toolController.hasCargo
            && (toolController.toolSelected == ToolController.TOOL_CLAW )
            && (toolController.toolStatus == ToolController.TOOL_DEPLOYED)
            && (other.gameObject.tag == "Collectable")
        ){
        }

    }

}