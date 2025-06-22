using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject experience;
    [SerializeField] GameObject level;
    [SerializeField] GameObject hpStat;
    [SerializeField] GameObject dmgStat;
    [SerializeField] GameObject speedStat;
    [SerializeField] GameObject statsPoints;

    private void Start()
    {
        menu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Confined;
            menu.SetActive(!menu.activeSelf);
            experience.SetActive(!menu.activeSelf);
        }

        if (menu.activeSelf)
        {
            level.GetComponent<TextMeshProUGUI>().text = "Level: " + ExperienceManager.Instance.currentLevel.ToString();
            hpStat.GetComponent<TextMeshProUGUI>().text = PlayerStats.Instance.maxHealth.ToString();
            dmgStat.GetComponent<TextMeshProUGUI>().text = PlayerStats.Instance.damage.ToString();
            speedStat.GetComponent<TextMeshProUGUI>().text = PlayerStats.Instance.speed.ToString();
            statsPoints.GetComponent<TextMeshProUGUI>().text = "Point: " + PlayerStats.Instance.statsPoints.ToString();
        }
    }
}
