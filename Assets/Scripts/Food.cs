using UnityEngine;
using System.Collections;

public class FoodActivity {
	
	private Dog dog;
	private bool atTarget;
	private Vector3 target;

	public FoodActivity(Dog dog) {
		Init();
		Vector3 dishPosition = GameObject.Find("FoodDish").transform.position;
		target = new Vector3 (dishPosition.x - .55f, dishPosition.y + .7f, 0.0f);
		this.dog = dog;
	}

	public void Init() {
		atTarget = false;
	}
	
	public void Run () {
		if (atTarget) {
			if (dog.statHunger <= 1.0f) {
				dog.AnimTrigger("dogSheet_eat");
				dog.Eating();
				dog.statHunger += 0.001f;
			} else {
				dog.StopSounds();
				dog.ReturnHome();
				atTarget = false;
			}

		} else if (dog.statHunger <= 0.8f) {
			dog.transform.localPosition = Vector3.MoveTowards(dog.transform.localPosition, target, dog.returnSpeedX);
			dog.AnimTrigger("dogSheet_walk");
			dog.Bark();
			if(dog.transform.localPosition == target) {
				dog.StopSounds();
				atTarget = true;
			}
		}
	}
}
