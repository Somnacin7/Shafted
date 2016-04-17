using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    public string hoverText;
    public InteractableType type;
    public enum InteractableType
    {
        DOORS,
        BRIEFCASE,
        DOOR_OPEN_BUTTON,
        KEY_SLOT,
        KEY_PANEL,
        KEY,
        POSTER,
        SECRET_POSTER,
        LOOSE_TILE,
        BOBBY_PIN,
        SCREWDRIVER,
        TOOL_CASE,
        RAIL1,
        RAIL2,
        RAIL3,
        CROWBAR,
        POSTER2
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void interact(PickUp pickUp)
    {
        switch (type)
        {
            case InteractableType.DOORS:
                pickUp.curText = "The doors won't budge";
                break;
            case InteractableType.BRIEFCASE:
                if (pickUp.inventory.Contains("BOBBY_PIN"))
                {
                    gameObject.SetActive(false);
                    transform.parent.FindChild("BriefOpen").gameObject.SetActive(true);
                    transform.parent.FindChild("Screwdriver").gameObject.SetActive(true);
                }
                else
                {
                    hoverText = "The briefcase is locked";
                }
                break;
            case InteractableType.DOOR_OPEN_BUTTON:
                hoverText = "CONGRATULATIONS!  YOU WIN!";
                break;
            case InteractableType.KEY_SLOT:
                if (pickUp.inventory.Contains("KEY"))
                {
                    var p = transform.parent.parent;
                    p.FindChild("button").FindChild("button off").gameObject.SetActive(false);
                    p.FindChild("button").FindChild("button on").gameObject.SetActive(true);
                }
                break;
            case InteractableType.KEY_PANEL:
                hoverText = "There must be some way to open it...";
                break;
            case InteractableType.KEY:
                Destroy(gameObject);
                pickUp.inventory.Add("KEY");
                break;
            case InteractableType.POSTER:
                hoverText = "The back has three symbols: | - |";
                break;
            case InteractableType.LOOSE_TILE:
                if (pickUp.inventory.Contains("CROWBAR"))
                {
                    Destroy(gameObject);
                    transform.parent.FindChild("key").gameObject.SetActive(true);
                }
                else
                {
                    hoverText = "It’ll take a lot of force to pull this tile up.";
                }
                break;
            case InteractableType.BOBBY_PIN:
                Destroy(gameObject);
                pickUp.inventory.Add("BOBBY_PIN");
                break;
            case InteractableType.SCREWDRIVER:
                Destroy(gameObject);
                pickUp.inventory.Add("SCREWDRIVER");
                break;
            case InteractableType.TOOL_CASE:
                if (pickUp.inventory.Contains("SCREWDRIVER"))
                {
                    Destroy(transform.FindChild("Panel").gameObject);
                    transform.FindChild("Crowbar").gameObject.SetActive(true);
                }
                else
                {
                    hoverText = "It appears to be screwed shut.";
                }
                break;
            case InteractableType.RAIL1:
                if (transform.FindChild("Horizontal1").gameObject.activeInHierarchy)
                {
                    transform.FindChild("Horizontal1").gameObject.SetActive(false);
                    transform.FindChild("Vertical1").gameObject.SetActive(true);
                }
                else
                {
                    transform.FindChild("Horizontal1").gameObject.SetActive(true);
                    transform.FindChild("Vertical1").gameObject.SetActive(false);
                }
                break;
            case InteractableType.RAIL2:
                if (transform.FindChild("Horizontal2").gameObject.activeInHierarchy)
                {
                    transform.FindChild("Horizontal2").gameObject.SetActive(false);
                    transform.FindChild("Vertical2").gameObject.SetActive(true);
                }
                else
                {
                    transform.FindChild("Horizontal2").gameObject.SetActive(true);
                    transform.FindChild("Vertical2").gameObject.SetActive(false);
                }
                break;
            case InteractableType.RAIL3:
                if (transform.FindChild("Horizontal3").gameObject.activeInHierarchy)
                {
                    transform.FindChild("Horizontal3").gameObject.SetActive(false);
                    transform.FindChild("Vertical3").gameObject.SetActive(true);
                }
                else
                {
                    transform.FindChild("Horizontal3").gameObject.SetActive(true);
                    transform.FindChild("Vertical3").gameObject.SetActive(false);
                }
                break;
            case InteractableType.CROWBAR:
                Destroy(gameObject);
                pickUp.inventory.Add("CROWBAR");
                break;
        }
        var interactive = transform.parent;
        var r1 = interactive.FindChild("Code1").FindChild("Horizontal1").gameObject.activeInHierarchy;
        var r2 = interactive.FindChild("Code2").FindChild("Horizontal2").gameObject.activeInHierarchy;
        var r3 = interactive.FindChild("Code3").FindChild("Horizontal3").gameObject.activeInHierarchy;

        if (!r1 && r2 && !r3)
        {
            interactive.FindChild("Keyhole").FindChild("keyhole panel").gameObject.SetActive(false);
            interactive.FindChild("Keyhole").FindChild("Keyhole").gameObject.SetActive(true);
        }
        else
        {
            interactive.FindChild("Keyhole").FindChild("keyhole panel").gameObject.SetActive(true);
            interactive.FindChild("Keyhole").FindChild("Keyhole").gameObject.SetActive(false);
        }
    }
}
