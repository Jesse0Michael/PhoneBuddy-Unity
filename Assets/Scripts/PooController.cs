using UnityEngine;
using System.Collections;

public class PooController : MonoBehaviour
{

    public GameObject pooType;
    public int pooCount;

    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Controller.myDog.statHygiene < 0.80f && pooCount <= 0)
        {
            spawnPoo();
        }
        else if (Controller.myDog.statHygiene < 0.6f && pooCount <= 1)
        {
            spawnPoo();
        }
        else if (Controller.myDog.statHygiene < 0.4f && pooCount <= 2)
        {
            spawnPoo();
        }
        else if (Controller.myDog.statHygiene < 0.2f && pooCount <= 3)
        {
            spawnPoo();
        }
        else if (Controller.myDog.statHygiene <= 0.0f && pooCount <= 4)
        {
            spawnPoo();
        }

    }

    void spawnPoo()
    {
        Vector3 pooPosition = new Vector3(Random.Range(-3.3f, 3.3f), Random.Range(1.2f, -1.65f), 0);
        Instantiate(pooType, pooPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
        UpdatePooCount();
    }

    void UpdatePooCount()
    {
        pooCount = GameObject.FindGameObjectsWithTag("poo").Length;
        Debug.Log("Poo Count: " + pooCount);
    }

}
