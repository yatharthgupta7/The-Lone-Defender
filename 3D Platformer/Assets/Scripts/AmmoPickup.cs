using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 20;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController.Instance.currentAmmo = ammoAmount;
            PlayerController.Instance.AddAmmoUI();

            AudioController.Instance.PlayAmmoPickup();
            Destroy(gameObject);
        }
    }
}
