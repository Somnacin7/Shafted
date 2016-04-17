using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class PickUp : MonoBehaviour {

    
    public Canvas canvas;
    public GameObject myo = null;
    public string curText { get; set; }
    public List<string> inventory;
    private Thalmic.Myo.Pose lastPose = Thalmic.Myo.Pose.Unknown;

    private void Start()
    {
        curText = "DEFAULT";
    }

    private void Update()
    {
        var thalmicMyo = myo.GetComponent<ThalmicMyo>();
        bool myoTap = false;
        if (thalmicMyo.pose != lastPose && thalmicMyo.pose == Thalmic.Myo.Pose.DoubleTap)
        {
            Debug.Log("TAP");
            myoTap = true;
        }

        var fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            Debug.DrawLine(transform.position, hit.transform.position);
            if (hit.collider.gameObject.tag == "Interactive")
            {

                var objHit = hit.collider.gameObject.GetComponent<Interactable>() ?? hit.collider.gameObject.GetComponentInParent<Interactable>();
                if (objHit != null)
                {
                    if (Input.GetMouseButtonDown(0) || myoTap)
                    {
                        objHit.interact(this);
                    }
                    else
                    {
                        Debug.Log(objHit.hoverText);
                        curText = objHit.hoverText;
                    }
                }
            }
            else
            {
                curText = "";
            }
        }
        if (GetComponentInChildren<Text>().text != null)
            GetComponentInChildren<Text>().text = curText;
        
    }

    
}
