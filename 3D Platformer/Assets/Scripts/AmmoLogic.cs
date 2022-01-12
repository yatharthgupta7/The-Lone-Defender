using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoLogic : MonoBehaviour
{

    public int reloadAmmo = 25;
    // Start is called before the first frame update
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
            PlayerController.Instance.currentAmmo = reloadAmmo;
            Destroy(gameObject);
        }
    }
}
