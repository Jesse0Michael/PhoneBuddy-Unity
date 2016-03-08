using UnityEngine;
using System.Collections;

public class SliderScript : MonoBehaviour {

	public GameObject slider;

	private float grabPosX;
	private float startX;

	private float xMax;
	private float xMin;

	private bool open;
	private bool close;
	private float speed;

	void Start () {
		xMax = -5.0f;
		xMin = -160.0f;

		open = false;
		close = false;
		speed = 10.0f;
	}

	// Update is called once per frame
	void Update () {
		if (open) {
			Vector3 target = new Vector3(xMax, slider.transform.localPosition.y, slider.transform.localPosition.z);
			slider.transform.localPosition = Vector3.MoveTowards(slider.transform.localPosition, target, speed);
			if(slider.transform.localPosition.x == xMax) {
				open = false;
			}
		}

		if (close) {
			Vector3 target = new Vector3(xMin, slider.transform.localPosition.y, slider.transform.localPosition.z);
			slider.transform.localPosition = Vector3.MoveTowards(slider.transform.localPosition, target, speed);
			if(slider.transform.localPosition.x == xMin) {
				close = false;
			}
		}

	}

	public void Clicked () {
		Debug.Log ("Clicked");
		//TODO toggle pos ... if far away from grabPosX
		close = true;
	}

	public void Dragged () {
//		Debug.Log (Input.mousePosition.x + " - " + grabPosX + " + " + startX + " = " + slider.transform.localPosition.x);
		float xLoc = Input.mousePosition.x - grabPosX + startX;
		if(xLoc >= xMin && xLoc <= xMax) {
			slider.transform.localPosition = new Vector3(xLoc, slider.transform.localPosition.y, slider.transform.localPosition.z);
		}
	}
	
	public void Down () {
		Debug.Log ("Down");
		startX = slider.transform.localPosition.x;
		grabPosX = Input.mousePosition.x;
	}

	public void Released () {
		Debug.Log ("Released");
		//TODO move towards closest pos
	}
}
