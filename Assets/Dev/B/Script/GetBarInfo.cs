using UnityEngine;
using UnityEngine.UI;

public class GetBarInfo : MonoBehaviour
{
    [Header("Required")]
    public Character player;

    public Slider healthbar;
    public Slider manahbar;

    private void Awake()
    {
        RefreshBar();
    }
    public void RefreshBar()
    {
        healthbar.maxValue = player.health;
        healthbar.value = player.currentHealth;
        manahbar.maxValue = player.mana;
        manahbar.value = player.currentMana;
    }
}
