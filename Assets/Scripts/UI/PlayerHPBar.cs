using UnityEngine;

public class PlayerHPBar : MonoBehaviour
{
    public void ChangeHPBar(float hpCurrent, float hpMax)
    {
        transform.localScale = new Vector3(hpCurrent/hpMax, 1, 1);
    }
}
