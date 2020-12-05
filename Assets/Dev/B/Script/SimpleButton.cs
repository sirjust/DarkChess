using UnityEngine;
using UnityEngine.UI;

public class SimpleButton : MonoBehaviour
{
    public GameObject button;

    public void ControlMenu(GameObject target)
    {
        if (target.GetComponent<Image>().enabled)
        {
            target.GetComponent<CharInfo>().DisableMenu(false);
        }
        else
        {
            target.GetComponent<CharInfo>().DisableMenu(true);
        }
    }
}
