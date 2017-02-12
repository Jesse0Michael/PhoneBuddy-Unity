using UnityEngine;
using System.Collections;

public enum Activity
{
	dogFetch,
	dogTug,
	dogFood,
	dogWater,
	dogPoo,
	dogPet,
	dogIdle
	
};

public class Controller : MonoBehaviour {

	public GameObject Dog;

	public GameObject FoodDish;
	public GameObject WaterDish;
	public GameObject TugRope;
	public GameObject TugRope60;
	public GameObject Ball;
	public GameObject Shadow;


	public static Activity myActivity;

	public static Dog myDog;


	// Use this for initialization
	void Start () {
		myActivity = Activity.dogIdle;
		myDog = new Dog(Dog, Ball, TugRope, TugRope60);

		HideExcept(null);
	}

	void HideExcept(GameObject except) {
		if (except != FoodDish) {
			FoodDish.GetComponent<SpriteRenderer> ().enabled = false;
		} 
		if (except != WaterDish) {
			WaterDish.GetComponent<SpriteRenderer> ().enabled = false;
		} 
		if (except != TugRope) {
			TugRope.GetComponent<SpriteRenderer> ().enabled = false;
			TugRope60.GetComponent<CircleCollider2D> ().enabled = false;
			TugRope60.GetComponent<SpriteRenderer> ().enabled = false;
		}
		if (except != Ball) {
			Ball.GetComponent<SpriteRenderer> ().enabled = false;
			Shadow.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}
	
	void SetActivity(Activity act) {
		if (myActivity != act) {
			myDog.ReturnHome();
		}
		myActivity = act;
	}
	
	// Update is called once per frame
	void Update () {
		myDog.Update ();	
	}

	public void Fetch () {
		Debug.Log ("Fetch");
		SliderScript.close = true;
		HideExcept (Ball);
		Ball.GetComponent<SpriteRenderer> ().enabled = true;
		SetActivity(Activity.dogFetch);
	}
	
	public void Tug () {
		Debug.Log ("Tug");
		SliderScript.close = true;
		HideExcept (TugRope);
		SetActivity(Activity.dogTug);
	}
	
	public void Food () {
		Debug.Log ("Food");
		SliderScript.close = true;
		HideExcept (FoodDish);
		FoodDish.GetComponent<SpriteRenderer> ().enabled = true;
		SetActivity(Activity.dogFood);
	}
	
	public void Water () {
		Debug.Log ("Water");
		SliderScript.close = true;
		HideExcept (WaterDish);
		WaterDish.GetComponent<SpriteRenderer> ().enabled = true;
		SetActivity(Activity.dogWater);
	}
	
	public void Poo () {
		Debug.Log ("Poo");
		SliderScript.close = true;
		SetActivity(Activity.dogPoo);
	}

	public void Vibrate () {
//		if (Game1.appDJ.volOn)
//		{
//			vibrateDelay.start();
//		}
	}
}
