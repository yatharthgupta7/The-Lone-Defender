using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmmount = 25;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            PlayerController.Instance.AddHeal(healthAmmount);
            AudioController.Instance.PlayHealthPickup();
            Destroy(gameObject);
        }
    }
}
