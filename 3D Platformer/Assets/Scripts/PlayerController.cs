using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion
    Rigidbody2D rb;
    public float moveSpeed = 1f;

    private Vector2 moveInput;
    private Vector2 mouseInput;

    public float mouseSenstivity = 1;

    public Camera viewCam;

    public GameObject bulletImpact;

    public int currentAmmo;

    public Animator gunAnim;
    public Animator anim;

    public int currentHealth;
    public int maxHealth;

    public GameObject deathScreen;
    public GameObject pauseMenu;

    public bool hasDied = false;

    public Text healthText, ammoText;
    public Text killAmt;

    public int enemyKilled;

    private bool isMuted;

    public UnityAds ads;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthText.text = ((currentHealth / maxHealth) * 100).ToString() + "%";
        ammoText.text = currentAmmo.ToString();
        isMuted = PlayerPrefs.GetInt("MUTED") == 1;
        ads.ShowBanner();
    }

    public void SetMouseSenstivity(float m)
    {
        mouseSenstivity = m;
    }

    public void MutePressed()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        PlayerPrefs.SetInt("MUTED", isMuted ? 1 : 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDied)
        {
            MovePlayer();
        }
        //Shoot();
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ToMenu()
    {
        //ads.ShowInterstitial("Menu");
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    void Shoot()
    {
        if (currentAmmo > 0)
        {
            Ray ray = viewCam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                int f = 0;
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.parent.GetComponent<EnemyController>().TakeDamage();
                    if (hit.transform.parent.GetComponent<EnemyController>().health <= 0)
                    {
                        f = 1;
                    }
                }
                if (f == 0)
                {
                    Instantiate(bulletImpact, hit.point, transform.rotation);
                }
                f = 0;
            }
            currentAmmo--;
            gunAnim.SetTrigger("Shoot");
            AudioController.Instance.PlayGunShot();
            AddAmmoUI();
        }
    }

    public void ShootButton()
    {
        Shoot();
    }
    void MovePlayer()
    {
        //player movement
        moveInput = new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));
        Vector3 moveHorizontal = transform.up * -moveInput.x;
        Vector3 moveVertical = transform.right * moveInput.y;
        rb.velocity = (moveHorizontal + moveVertical) * moveSpeed;

        //player mouse control

        mouseInput = new Vector2(SimpleInput.GetAxisRaw("mouseX"), SimpleInput.GetAxisRaw("mouseY")) * mouseSenstivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);

        viewCam.transform.localRotation = Quaternion.Euler(viewCam.transform.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f));

        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            deathScreen.SetActive(true);
            hasDied = true;
            currentHealth = 0;
            Time.timeScale = 0;
        }
        int health = (int)((currentHealth * 100 / maxHealth));
        healthText.text = health + "%";
        AudioController.Instance.PlayPlayerHurt();
    }

    public void AddHeal(int heal)
    {
        currentHealth += heal;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        int health = (int)((currentHealth * 100 / maxHealth));
        healthText.text = health + "%";
    }

    public void AddAmmoUI()
    {
        ammoText.text = currentAmmo.ToString();
    }

    public void KillAmt()
    {
        killAmt.text = "Defeated - " + enemyKilled;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
        //ads.ShowInterstitial("Game");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
