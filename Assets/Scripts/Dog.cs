using UnityEngine;
using System.Collections;

public class Dog : MonoBehaviour
{
    public GameObject TugRope;
    public GameObject TugRope60;
    public GameObject Ball;

    public bool returnHome;
    public bool barking;

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
    private FetchActivity fetchActivity;
    private TugActivity tugActivity;

    public AudioClip Growl1;
    public AudioClip Growl2;
    public AudioClip Growl3;
    public AudioClip Growl4;
    public AudioClip Bark1;
    public AudioClip Bark2;
    public AudioClip Bark3;
    public AudioClip Bark4;
    public AudioClip Bark5;
    public AudioClip Eat;
    public AudioClip Drink;

    private AudioSource source;
    private AudioClip[] growlSounds;
    private AudioClip[] barkSounds;

    void Start()
    {
        statThirst = .95f;
        statHygiene = .95f;
        statHunger = .95f;
        returnHome = false;
        returningFlag = false;
        barking = false;

        origin = new Vector3(0, 0, 0);
        originScale = new Vector3(1, 1, 1);
        returnSpeedX = .02f;
        returnSpeedS = .05f;

        dogAnim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        foodActivity = new FoodActivity(this);
        waterActivity = new WaterActivity(this);
        fetchActivity = new FetchActivity(this, Ball);
        tugActivity = new TugActivity(this, TugRope, TugRope60);

        source = GetComponent<AudioSource>();
        growlSounds = new AudioClip[4] { Growl1, Growl2, Growl3, Growl4 };
        barkSounds = new AudioClip[5] { Bark1, Bark2, Bark3, Bark4, Bark5 };
    }

    void Update()
    {
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
            statHygiene -= .0001f;
        }

        if (returnHome == false)
        {
            switch (Controller.myActivity)
            {
                case Activity.dogFetch:
                    fetchActivity.Run();
                    if (returningFlag)
                    {
                        fetchActivity.Init();
                        returningFlag = false;
                    }
                    break;

                case Activity.dogTug:
                    tugActivity.Run();
                    if (returningFlag)
                    {
                        tugActivity.Init();
                        returningFlag = false;
                    }
                    break;

                case Activity.dogFood:
                    foodActivity.Run();
                    if (returningFlag)
                    {
                        foodActivity.Init();
                        returningFlag = false;
                    }
                    break;

                case Activity.dogWater:
                    waterActivity.Run();
                    if (returningFlag)
                    {
                        waterActivity.Init();
                        returningFlag = false;
                    }
                    break;
            }
        }

        if (returnHome == true)
        {
            StopSounds();
            RunTowards(origin, originScale, returnSpeedX, returnSpeedS);
        }

        if (barking == true)
        {
            if (!source.isPlaying)
            {
                int bark = Random.Range(0, 5);
                source.clip = barkSounds[bark];
                source.loop = false;
                source.Play();
            }
        }
    }

    public void RunTowards(Vector3 target, Vector3 targetScale, float speed, float speedScale)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, speed);
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, speedScale);
        if (transform.localPosition != target || transform.localScale != targetScale)
        {
            //Game1.appDJ.runningOn = true;
            if (transform.localPosition.y >= target.y)
            {
                AnimTrigger("dogSheet_runTowards");
            }
            else if (transform.localPosition.y <= target.y)
            {
                AnimTrigger("dogSheet_runAway");
            }
            else if (transform.localPosition.x >= target.x)
            {
                AnimTrigger("dogSheet_walk_left");
            }
            else if (transform.localPosition.x <= target.x)
            {
                AnimTrigger("dogSheet_walk");
            }
        }
        else
        {
            AnimTrigger("dogSheet_idle");
            StopSounds();
            returnHome = false;
            returnSpeedS = .05f;
        }
    }

    public void ReturnHome()
    {
        returnHome = true;
        returningFlag = true;
    }

    public bool AnimTrigger(string trigger)
    {
        switch (trigger)
        {
            case "dogSheet_walk_left":
                Flip("left");
                trigger = "dogSheet_walk";
                break;
            default:
                Flip("right");
                break;
        }

        if (!dogAnim.GetCurrentAnimatorStateInfo(0).IsName(trigger))
        {
            dogAnim.SetTrigger(trigger);
            return true;
        }
        return false;
    }

    public void Flip(string direction)
    {
        switch (direction.ToLower())
        {
            case "left":
                if (!flippedLeft)
                {
                    flippedLeft = true;
                    transform.Rotate(new Vector3(0, 180, 0));
                }
                break;
            case "right":
                if (flippedLeft)
                {
                    flippedLeft = false;
                    transform.Rotate(new Vector3(0, 180, 0));
                }
                break;
        }
    }

    public void Growl()
    {
        if (!source.isPlaying)
        {
            int growl = Random.Range(0, 4);
            source.clip = growlSounds[growl];
            source.loop = false;
            source.Play();
        }
    }

    public void Bark()
    {
        barking = true;
    }

    public void Eating()
    {
        if (!source.isPlaying)
        {
            source.clip = Eat;
            source.loop = true;
            source.Play();
        }
    }

    public void Drinking()
    {
        if (!source.isPlaying)
        {
            source.clip = Drink;
            source.loop = true;
            source.Play();
        }
    }

    public void StopSounds()
    {
        source.Stop();
        barking = false;
    }
}
