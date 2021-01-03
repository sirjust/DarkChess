using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public delegate void aa(int num);
    public aa a;

    public void Ak(int num)
    {
        Debug.Log($" Ak trigger: {num}");
    }

    public void As(int num)
    {
        Debug.Log($" As trigger: {num}");
    }

    public void chnage()
    {
        if (a == Ak)
            a = As;
        else
            a = Ak;
        a(10);
    }
}
