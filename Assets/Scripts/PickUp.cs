using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class PickUp : MonoBehaviour {

    
    public Canvas canvas;
    public string curText { get; set; }
    public List<string> inventory;

    private void Start()
    {
        curText = "DEFAULT";
    }

    private void Update()
    {
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
                    if (Input.GetMouseButtonDown(0))
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
