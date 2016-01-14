using UnityEngine;
using System.Collections;

public class CollectOnCollision : MonoBehaviour { // TODO change to collector?

    public ToolController toolController;
	private GameObject cargo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.C) && toolController.hasCargo) {
			cargo.transform.SetParent(null);
			cargo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			toolController.hasCargo = false; //  TODO set cargo with a method
		}
	}

    void OnCollisionEnter(Collision other){

        Debug.Log("other.gameObject.tag: " + other.gameObject.tag);

        if(other.gameObject.tag == "Collectable" && !toolController.hasCargo
            && toolController.toolSelected == ToolController.TOOL_CLAW 
            && toolController.toolStatus == ToolController.TOOL_DEPLOYED){
            Debug.Log("Claw collided with a collectable");
            Vector3 localPosition = other.gameObject.transform.localPosition;
            localPosition.y = 7.5f; // TODO make constant
            other.gameObject.transform.localPosition = localPosition;
            other.gameObject.transform.SetParent(toolController.claw);
			toolController.hasCargo = true;
			cargo = other.gameObject;
			cargo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        }

    }

}