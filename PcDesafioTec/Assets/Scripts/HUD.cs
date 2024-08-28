using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    public Image lifeBar;

    void Start()
    {
        instance = this;
        SetLife();
    }
    private void Update()
    {
        SetLife();
    }
    public void SetLife()
    {
        lifeBar.fillAmount = (float)PlayerStats.currentLife / (float)PlayerStats.maxLife;
    }
}
