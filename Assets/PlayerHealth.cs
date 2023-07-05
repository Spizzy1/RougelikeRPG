using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    float invincibility;
    [SerializeField]
    float HP;
    [SerializeField]
    float invincibilityDuration;
    // Start is called before the first frame update
    void Start()
    {
        invincibility = - 1;
        HP = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibility >= 0)
        {
            invincibility += Time.deltaTime;
            if(invincibility > invincibilityDuration)
            {
                invincibility = -1;
            }
        }
    }
    public void Damage(float Amount)
    {
        if(!(invincibility >= 0))
        {
            invincibility = 0;
            HP -= Amount;
        }
    }
}
