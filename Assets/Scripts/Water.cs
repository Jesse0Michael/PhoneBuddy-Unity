using UnityEngine;
using System.Collections;

public class WaterActivity
{

    private Dog dog;
    private bool atTarget;
    private Vector3 target;

    public WaterActivity(Dog dog)
    {
        Init();
        Vector3 dishPosition = GameObject.Find("WaterDish").transform.position;
        target = new Vector3(dishPosition.x + .55f, dishPosition.y + .7f, 0.0f);
        this.dog = dog;
    }

    public void Init()
    {
        atTarget = false;
    }

    public void Run()
    {
        if (atTarget)
        {
            if (dog.statThirst <= 1.0f)
            {
                dog.AnimTrigger("dogSheet_drink");
                dog.Drinking();
                dog.statThirst += 0.001f;
            }
            else
            {
                dog.StopSounds();
                dog.ReturnHome();
                atTarget = false;
            }
        }
        else if (dog.statThirst <= 0.8f)
        {
            dog.transform.localPosition = Vector3.MoveTowards(dog.transform.localPosition, target, dog.returnSpeedX);
            dog.AnimTrigger("dogSheet_walk_left");
            dog.Bark();
            if (dog.transform.localPosition == target)
            {
                dog.StopSounds();
                atTarget = true;
            }
        }
    }
}
