using UnityEngine;
using UnityEngine.UI;

public class SimpleButton : MonoBehaviour
{
    public GameObject button;

    public void ControlMenu(GameObject _target)
    {
        if (_target.GetComponent<Image>().enabled)
        {
            _target.GetComponent<CharInfo>().DisableMenu(false);
        }
        else
        {
            _target.GetComponent<CharInfo>().DisableMenu(true);
        }
    }
}
