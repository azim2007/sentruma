using UnityEngine;
using UnityEngine.UI;

public class ObjectInfoPanelController : MonoBehaviour
{
    private Text infoText;
    private Button close;
    private int index;
    void Start()
    {
        transform.localScale = Vector3.one;
        infoText = this.transform.GetChild(0).GetComponent<Text>();
        close = this.transform.GetComponent<Button>();
        close.onClick.AddListener(() => Destroy(this.gameObject));
        infoText.text = CurrentProgress.currentProgress.Inventory[index].Item1.Description;
    }

    public void ShowInfo(int id)
    {
        index = id;
    }
}
