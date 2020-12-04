using TMPro;
using UnityEngine;

public class GetVersion : MonoBehaviour
{
    public TextMeshProUGUI versionText;

    private void Start()
    {
        versionText.text = "Build v" + Application.version;
    }
}