using UnityEngine;
using System.Collections;

public class FoodActivity {
	
	private Dog dog;
	private bool atFood;
	private Vector3 eatingPos;

	public FoodActivity(Dog dog) {
		atFood = false;
		eatingPos = new Vector3 (2.15f, -0.6f, 0.0f);
		this.dog = dog;
	}
	
	public void Run () {
		Debug.Log (dog.me);
		if (atFood) {
			dog.AnimTrigger("dogSheet_eat");

		} else if (dog.statHunger <= 0.98f) {
			dog.me.transform.localPosition = Vector3.MoveTowards(dog.me.transform.localPosition, eatingPos, 0.02f);
			dog.AnimTrigger("dogSheet_walk");
			if(dog.me.transform.localPosition == eatingPos) {
				atFood = true;
			}
		}
	}
}
