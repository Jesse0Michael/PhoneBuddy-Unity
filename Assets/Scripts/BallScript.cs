using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
	
	public GameObject Shadow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown() {
		Debug.Log("Ball down");
		Shadow.GetComponent<SpriteRenderer> ().enabled = true;
	}
	
	void OnMouseDrag() {
		Debug.Log("Ball drag");
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Shadow.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
	}
	
	void OnMouseUp() {
		Debug.Log("Ball up");
		Shadow.GetComponent<SpriteRenderer> ().enabled = false;
		Shadow.transform.localPosition = Vector3.zero;
	}
}
