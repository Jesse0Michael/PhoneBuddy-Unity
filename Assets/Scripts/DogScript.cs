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
	private bool flippedLeft;

	private FoodActivity foodActivity;
	private WaterActivity waterActivity;

	public Dog (GameObject dog) {
		me = dog;
		statThirst = 1.0f;
		statHygiene = 1.0f;
		statHunger = 1.0f;
		returnHome = false;
		tugboolean = false;
		
		dogScale = 1.0f;
		origin = new Vector3(0, 0, 0);
		returnSpeedX = .02f;
		returnSpeedY = .01f;
		returnSpeedS = .005f;

		dogAnim = me.GetComponent<Animator> ();
		
		foodActivity = new FoodActivity (this);
		waterActivity = new WaterActivity (this);
	}

	public void Update() {
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
				waterActivity.Run();
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
			me.transform.localPosition = Vector3.MoveTowards(me.transform.localPosition, origin, returnSpeedX);
			if (me.transform.localPosition != origin)
			{
				//Game1.appDJ.runningOn = true;
				if (me.transform.localPosition.y >= origin.y)
				{
					AnimTrigger("dogSheet_runTowards");
				}
				else if (me.transform.localPosition.y <= origin.y)
				{
					AnimTrigger("dogSheet_runAway");
				}
				else if (me.transform.localPosition.x >= origin.x)
				{
					AnimTrigger("dogSheet_walk_left");
				}
				else if (me.transform.localPosition.x <= origin.x)
				{
					AnimTrigger("dogSheet_walk");
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
		switch(trigger) {
			case "dogSheet_walk_left":
				Flip("left");
				trigger = "dogSheet_walk";
				break;
			default:
				Flip("right");
				break;
		}
		
		if(!dogAnim.GetCurrentAnimatorStateInfo(0).IsName(trigger)) {
			dogAnim.SetTrigger(trigger);
		}
	}
	
	public void Flip(string direction) {
		switch (direction.ToLower())
		{
			case "left":
				if(!flippedLeft) {
					flippedLeft = true;
					me.transform.Rotate(new Vector3(0,180,0));
				}
				break;
			case "right":
				if(flippedLeft) {
					flippedLeft = false;
					me.transform.Rotate(new Vector3(0,180,0));
				}
				break;
		}
	}
}
