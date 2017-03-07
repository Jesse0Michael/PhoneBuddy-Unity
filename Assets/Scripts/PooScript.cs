using UnityEngine;
using System.Collections;

public class PooScript : MonoBehaviour {

	public Sprite Poo1;
	public Sprite Poo2;

  private Vector3 origin;
	
	// Use this for initialization
	void Start () {
		Sprite[] pooSprites = new Sprite[2]{Poo1, Poo2};
		int pooSelection = Random.Range(0, 2);
		GetComponent<SpriteRenderer> ().sprite = pooSprites[pooSelection];

		origin = transform.localPosition;

    float pooSize = (2.5f - transform.localPosition.y) / 2;
		transform.localScale = new Vector3(1 - pooSize, 1 - pooSize, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnMouseDown() {
    if(Controller.myActivity ==  Activity.dogPoo) {
		  Debug.Log("Poo down");
    }
	}
	
	void OnMouseDrag() {
    if(Controller.myActivity ==  Activity.dogPoo) {
		  Debug.Log("Poo drag");
		  Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		  transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
    }
	}
	
	void OnMouseUp() {
		Debug.Log("Poo up");
		if (Controller.myActivity ==  Activity.dogPoo && false) { //mouse intersect with bag && poo activity
      Controller.myDog.statHygiene += 40.0f;
      Destroy(gameObject);
    } else {
      transform.localPosition = origin;
    }
	}

	
}
