using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    #region Singleton
    public static AudioController Instance;
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }

    #endregion

    public AudioSource ammo, enemyDeath, enemtShot, gunShot, health, playerHurt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAmmoPickup()
    {
        ammo.Stop();
        ammo.Play();
    }


    public void PlayEnemyDeath()
    {
        enemyDeath.Stop();
        enemyDeath.Play();
    }


    public void PlayEnemyShot()
    {
        enemtShot.Stop();
        enemtShot.Play();
    }


    public void PlayGunShot()
    {
        gunShot.Stop();
        gunShot.Play();
    }

    public void PlayHealthPickup()
    {
        health.Stop();
        health.Play();
    }

    public void PlayPlayerHurt()
    {
        playerHurt.Stop();
        playerHurt.Play();
    }
}
