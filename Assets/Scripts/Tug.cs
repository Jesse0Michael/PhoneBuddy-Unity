using UnityEngine;
using System.Collections;

public class TugActivity {
	
	private Dog dog;
	private bool atTarget;
	private Vector3 target;
	private Vector3 targetScale;
	
	public GameObject TugRope;
	public GameObject TugRope60;

	public TugActivity (Dog thatDog) {
		atTarget = false;
		target = new Vector3 (0.0f, -1.25f, 0.0f);
		targetScale = new Vector3 (4.0f, 4.0f, 1.0f);
		dog = thatDog;
		
		TugRope = GameObject.Find("Rope");
		TugRope60 = GameObject.Find("Rope60");
	}
	
	public void Run () {
		if (atTarget) {
			if(TugRope.GetComponent<SpriteRenderer> ().isVisible) {
				bool changed;
				if(TugRope.transform.localPosition.x > 1.3f) {
					if(TugRope.transform.localPosition.y > 0.0f) {
						changed = dog.AnimTrigger("tugSheet_UR");
					} else {
						changed = dog.AnimTrigger("tugSheet_LR");
					}
				} else if(TugRope.transform.localPosition.x < -1.3f){
					if(TugRope.transform.localPosition.y > 0.0f) {
						changed = dog.AnimTrigger("tugSheet_UL");
					} else {
						changed = dog.AnimTrigger("tugSheet_LL");
					}
				} else {
					changed = dog.AnimTrigger("tugSheet_C");
				}
				
				if(changed) {
					//dog.Vibrate();
					//dog.GrowlSound();
				}
			} else {
				dog.AnimTrigger("dogSheet_idle");
			}
			
		} else {
			dog.me.transform.localPosition = Vector3.MoveTowards(dog.me.transform.localPosition, target, dog.returnSpeedX);
			dog.me.transform.localScale = Vector3.MoveTowards(dog.me.transform.localScale, targetScale, dog.returnSpeedS);
			dog.AnimTrigger("dogSheet_runTowards");
			// Game1.appDJ.runningOn = true;
			if(dog.me.transform.localPosition == target && dog.me.transform.localScale == targetScale) {
				// Game1.appDJ.runningOn = false;
				TugRope60.GetComponent<SpriteRenderer> ().enabled = true;
				dog.AnimTrigger("dogSheet_idle");
				atTarget = true;
			}
		}
	}
}
