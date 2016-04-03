using UnityEngine;

public class InfoToggler : MonoBehaviour {

    public GameObject info;
    public GameObject other1;
    public GameObject other2;

    //Use this for initialization
    void Start()
    {
        HideInfo();
    }

    //Toggles the visibility of the information belonging to the PC.
    public void ToggleInfo()
    {
        if (info.activeSelf)
        {
            HideInfo();
        }
        else {
            ShowInfo();
        }
    }

    //Shows the information belonging to the PC.
    public void ShowInfo()
    {
        info.SetActive(true);
        other1.SetActive(false);
        other2.SetActive(false);
    }

    //Hides the information belonging to the PC.
    public void HideInfo()
    {
        info.SetActive(false);
        other1.SetActive(false);
        other2.SetActive(false);
    }

}