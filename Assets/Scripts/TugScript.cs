using UnityEngine;
using System.Collections;

public class TugScript : MonoBehaviour
{

    public GameObject TugRope;

    // Use this for initialization
    void Start()
    {
    }

    void FixedUpdate()
    {

    }

    void OnMouseDown()
    {
        Debug.Log("Rope down");
        TugRope.GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnMouseDrag()
    {
        Debug.Log("Rope drag");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TugRope.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
    }

    void OnMouseUp()
    {
        Debug.Log("Rope up");
        TugRope.GetComponent<SpriteRenderer>().enabled = false;
        TugRope.transform.localPosition = Vector3.zero;
    }

}
