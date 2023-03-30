using UnityEngine;
using UnityEngine.UI;

public class InventoryView
{
    private Button close, plusForce, plusHarisma;
    private Text forceValue, harismaValue, level, experience;
    private Image armor, weapon, slider;

    public InventoryView(Transform t)
    {
        SetButtons(t);
        SetButtonsOnClick(t.GetComponent<InventoryController>());
    }

    private void SetButtons(Transform t)
    {
        var stats = t.GetChild(9).transform;
        plusForce = stats.GetChild(0).GetComponent<Button>();
        plusHarisma = stats.GetChild(1).GetComponent<Button>();

        forceValue = stats.GetChild(4).GetComponent<Text>();
        harismaValue = stats.GetChild(5).GetComponent<Text>();

        close = t.GetChild(10).GetComponent<Button>();

        level = t.GetChild(2).GetComponent<Text>();
        experience = t.GetChild(3).GetComponent<Text>();

        armor = t.GetChild(5).GetComponent<Image>();
        weapon = t.GetChild(6).GetComponent<Image>();
        slider = t.GetChild(7).GetComponent<Image>();
    }

    private void SetButtonsOnClick(InventoryController p)
    {
        close.onClick.AddListener(p.CloseInventory);
    }

    public void UpdateStats(float force, float harisma, float level, float experience, 
        float rageLevel)
    {
        slider.transform.position = new Vector3(rageLevel * 80, 0f, 0f);

        forceValue.text = ((int)force).ToString();
        harismaValue.text = ((int)harisma).ToString();
        this.experience.text = (int)experience + "/" + 
            (int)CurrentProgress.currentProgress.Player.MaxExp;
        this.level.text = ((int)level).ToString();

        var imp = (experience >= CurrentProgress.currentProgress.Player.MaxExp);
        SetPlusButtonState(plusForce, imp);
        SetPlusButtonState(plusHarisma, imp);
    }

    private void SetPlusButtonState(Button b, bool state)
    {
        b.transform.GetChild(0).GetComponent<Text>().text = state ? "+" : "";
        b.interactable = state;
    }
}
