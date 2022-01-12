using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerController.Instance.transform.position,-Vector3.forward);
    }
}
