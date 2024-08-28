using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum PlayerModes
{
    Walking, UIing
}
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    PlayerModes modes;
    FirstPersonController controller;

    public static int currentLife, maxLife = 100;

    public UnityEvent OnPause, OnUnpause;

    void Awake()
    {
        instance = this;
        controller = GetComponent<FirstPersonController>();
        currentLife = maxLife;
    }
    void Update()
    {
        switch (modes)
        {
            case PlayerModes.Walking:
                controller.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case PlayerModes.UIing:
                controller.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                modes = PlayerModes.UIing;
                OnPause.Invoke();
            }
            else
            {
                Time.timeScale = 1;
                modes = PlayerModes.Walking;
                OnUnpause.Invoke();
            }
        }

        if(currentLife <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.rigidbody)
        {
            currentLife--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Projectile"))
        {
            currentLife--;
        }
    }
}
