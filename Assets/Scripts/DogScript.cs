using UnityEngine;
using System.Collections;

public class Dog {
	public GameObject me;
	
	public bool returnHome;
	
	public Vector3 origin;
	public Vector3 originScale;
	
	public float statHunger;
	public float statHygiene;
	public float statThirst;
	
	public float returnSpeedX;
	public float returnSpeedS;
	
	public Animator dogAnim;
	private bool flippedLeft;
	private bool returningFlag;

	private FoodActivity foodActivity;
	private WaterActivity waterActivity;
	private TugActivity tugActivity;

	public Dog (GameObject dog) {
		me = dog;
		statThirst = .95f;
		statHygiene = 1.0f;
		statHunger = .95f;
		returnHome = false;
		returningFlag = false;
		
		origin = new Vector3(0, 0, 0);
		originScale = new Vector3(1, 1, 1);
		returnSpeedX = .02f;
		returnSpeedS = .05f;

		dogAnim = me.GetComponent<Animator> ();
		
		foodActivity = new FoodActivity (this);
		waterActivity = new WaterActivity (this);
		tugActivity = new TugActivity (this);
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
				if(returningFlag) {
					FetchActivity.Init();
					returningFlag = false;
				}
				break;
				
			case Activity.dogTug:
				tugActivity.Run();
				if(returningFlag) {
					tugActivity.Init();
					returningFlag = false;
				}
				break;
				
			case Activity.dogFood:
				foodActivity.Run();
				if(returningFlag) {
					foodActivity.Init();
					returningFlag = false;
				}
				break;
				
			case Activity.dogWater:
				waterActivity.Run();
				if(returningFlag) {
					waterActivity.Init();
					returningFlag = false;
				}
				break;
			}
		}
		
		if (returnHome == true)
		{
			//Game1.appDJ.drinkOn = false;
			//Game1.appDJ.foodOn = false;
			me.transform.localPosition = Vector3.MoveTowards(me.transform.localPosition, origin, returnSpeedX);
			me.transform.localScale = Vector3.MoveTowards(me.transform.localScale, originScale, returnSpeedS);
			if (me.transform.localPosition != origin || me.transform.localScale != originScale)
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
			}
		}
	}

	public void ReturnHome() {
		returnHome = true;
		returningFlag = true;
	}

	public bool AnimTrigger(string trigger) {
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
			return true;
		}
		return false;
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
