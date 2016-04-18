using UnityEngine;
using System.Collections;

public class Dog {
	public GameObject me;
	
	public bool returnHome;
	
	public Vector3 origin;
	
	public float statHunger;
	public float statHygiene;
	public float statThirst;
	
	public float returnSpeedX;
	public float returnSpeedY;
	
	public float dogScale;
	public float returnSpeedS;
	
	public Animator dogAnim;
	public bool tugboolean;

	private FoodActivity foodActivity;
//	private WaterActivity waterActivity;

	public Dog (GameObject dog) {
		me = dog;
		statThirst = 1.0f;
		statHygiene = 1.0f;
		statHunger = 1.0f;
		returnHome = false;
		tugboolean = false;
		
		dogScale = 1.0f;
		origin = new Vector3(0, 0, 0);
		//		Dog.transform.localPosition = origin;
		returnSpeedX = .2f;
		returnSpeedY = .1f;
		returnSpeedS = .005f;

		dogAnim = me.GetComponent<Animator> ();
		
		foodActivity = new FoodActivity (this);
//		waterActivity = new WaterActivity ();
	}

	public void Update() {
		Debug.Log (me);
		//		Debug.Log (Dog.transform.localPosition);
		if (statThirst > 0.0f)
		{
			statThirst -= .0001f;
		}
		if (statHunger > 0.0f)
		{
			statHunger -= .0001f;
		}
		if (statHygiene > 0.0f)
		{
			statHygiene -= .00005f;
		}
		
		if (returnHome == false)
		{
			switch (Controller.myActivity)
			{
			case Activity.dogFetch:
				FetchActivity.Run();
				break;
				
			case Activity.dogTug:
				TugActivity.Run();
				break;
				
			case Activity.dogFood:
				foodActivity.Run();
				break;
				
			case Activity.dogWater:
				WaterActivity.Run();
				break;
			}
		}
		
		if (tugboolean == true)
		{
			returnSpeedS = .1f;
		}
		else
		{
			returnSpeedS = .005f;
		}
		
		if (returnHome == true)
		{
			//Game1.appDJ.drinkOn = false;
			//Game1.appDJ.foodOn = false;
			me.transform.localPosition = Vector3.MoveTowards(me.transform.localPosition, origin, returnSpeedS);
			if (me.transform.localPosition != origin)
			{
				//Game1.appDJ.runningOn = true;
				if (me.transform.localPosition.x >= origin.x)
				{
					AnimTrigger("dogSheet_walk");
				}
				else if (me.transform.localPosition.x <= origin.x)
				{
					AnimTrigger("dogSheet_walk");
				}
				
				if (me.transform.localPosition.y >= origin.y)
				{
					AnimTrigger("dogSheet_runAway");
				}
				else if (me.transform.localPosition.y <= origin.y)
				{
					AnimTrigger("dogSheet_runTowards");
					
				}
			}
			else
			{
				AnimTrigger("dogSheet_idle");
				//Game1.appDJ.runningOn = false;
				returnHome = false;
				tugboolean = false;
			}
		}

	}

	public void AnimTrigger(string trigger) {
		if(!dogAnim.GetCurrentAnimatorStateInfo(0).IsName(trigger)) {
			dogAnim.SetTrigger(trigger);
		}
	}
}
