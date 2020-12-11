using UnityEngine;

public enum Skills
{
    strike, move
}

public class AllSkills : MonoBehaviour
{
    public void cast(Skills skill)
    {
        this.SendMessage(skill.ToString());
    }

    public void strike()
    {
        Debug.LogError("strike");
    }

    public void move()
    {
        Debug.LogError("move");
    }
}
