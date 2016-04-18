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


	public static Activity myActivity;

	private Dog myDog;


	// Use this for initialization
	void Start () {
		myActivity = Activity.dogIdle;
		myDog = new Dog (Dog);

		HideExcept(null);
	}

	void HideExcept(GameObject except) {
		if (except != FoodDish) {
			FoodDish.GetComponent<SpriteRenderer> ().enabled = false;
		}
		if (except != WaterDish) {
			WaterDish.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		myDog.Update ();


		
		if(myActivity != Activity.dogFetch)
		{
//			Game1.cfetch.s_ball.setVisible(false);
//			Game1.cfetch.s_shadow.setVisible(false);
		}
//		
//		if(myActivity == Dog.activity.dogTug)
//		{
//			if (Game1.ctug.playPos == true && Game1.ctug.inPlay == false)
//			{
//				Game1.ctug.s_ropetex.setVisible(true);
//			}
//			else
//			{
//				
//				Game1.ctug.s_ropetex.setVisible(false);
//			}
//			
//			if(Game1.ctug.inPlay == true)
//			{
//				Game1.ctug.s_ropetex2.setVisible(true);
//			}
//			else
//			{
//				Game1.ctug.s_ropetex2.setVisible(false);
//				Game1.ctug.ropePos = new Vector2((int)((float)Game1.screenWidth * .5), (int)((float)Game1.screenHeight * .6));
//				Game1.ctug.ropePos2 = new Vector2((int)((float)Game1.screenWidth * .5 ), (int)((float)Game1.screenHeight * .6));
//				Game1.ctug.s_ropetex.setPosition(Game1.ctug.ropePos.x - (Game1.ropeTex.getWidth() / 2), Game1.ctug.ropePos.y - (Game1.ropeTex.getHeight() / 2));
//				Game1.ctug.s_ropetex2.setPosition(Game1.ctug.ropePos2.x - (Game1.ropeTex2.getWidth() / 2), Game1.ctug.ropePos2.y - (Game1.ropeTex2.getHeight() / 2));
//				
//				
//			}
//		}
//		else
//		{
//			Game1.ctug.s_ropetex2.setVisible(false);
//			Game1.ctug.s_ropetex.setVisible(false);
//		}
//		
//		
	}

	public void Fetch () {
		Debug.Log ("Fetch");
		SliderScript.close = true;
		myActivity = Activity.dogFetch;
	}
	
	public void Tug () {
		Debug.Log ("Tug");
		SliderScript.close = true;
		myActivity = Activity.dogTug;
	}
	
	public void Food () {
		Debug.Log ("Food");
		SliderScript.close = true;
		HideExcept (FoodDish);
		FoodDish.GetComponent<SpriteRenderer> ().enabled = true;
		myActivity = Activity.dogFood;
	}
	
	public void Water () {
		Debug.Log ("Water");
		SliderScript.close = true;
		HideExcept (WaterDish);
		WaterDish.GetComponent<SpriteRenderer> ().enabled = true;
		myActivity = Activity.dogWater;
	}
	
	public void Poo () {
		Debug.Log ("Poo");
		SliderScript.close = true;
		myActivity = Activity.dogPoo;
	}

	public void Vibrate () {
//		if (Game1.appDJ.volOn)
//		{
//			vibrateDelay.start();
//		}
	}
}
