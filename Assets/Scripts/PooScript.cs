using UnityEngine;
using System.Collections;

public class PooScript : MonoBehaviour {

	public Sprite Poo1;
	public Sprite Poo2;
	public AudioSource BagSound;

	private GameObject bag;
  private Vector3 origin;
	
	// Use this for initialization
	void Start () {
		Sprite[] pooSprites = new Sprite[2]{Poo1, Poo2};
		int pooSelection = Random.Range(0, 2);
		GetComponent<SpriteRenderer> ().sprite = pooSprites[pooSelection];

		origin = transform.localPosition;

    float pooSize = (transform.localPosition.y) / 3;
		transform.localScale = new Vector3(1 - pooSize, 1 - pooSize, 1);

	 	bag = GameObject.Find("PooBag");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnMouseDown() {
    if(Controller.myActivity == Activity.dogPoo) {
		  Debug.Log("Poo down");
    }
	}
	
	void OnMouseDrag() {
    if(Controller.myActivity == Activity.dogPoo) {
		  Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		  transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
    }
	}
	
	void OnMouseUp() {
		Debug.Log("Poo up");
		Debug.Log(bag.GetComponent<BoxCollider2D>());
		bool coll = this.GetComponent<BoxCollider2D>().IsTouching(bag.GetComponent<BoxCollider2D>());
		Debug.Log("Colliding :" + coll);
		if (Controller.myActivity == Activity.dogPoo && coll) {
      Controller.myDog.statHygiene += 0.3f;
			BagSound.Play();
			GetComponent<SpriteRenderer>().enabled = false;
      Destroy(gameObject, BagSound.clip.length);
    } else {
      transform.localPosition = origin;
    }
	}

	
}
