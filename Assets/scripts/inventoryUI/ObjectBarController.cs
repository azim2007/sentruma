using UnityEngine;
using UnityEngine.UI;

public class ObjectBarController : MonoBehaviour
{
    private Button throwB, throwAll, use, info, close;
    private Image preview;
    private int index;
    void Start()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        SetButtons();
        preview = transform.GetChild(5).GetComponent<Image>();
        SetButtonsOnClick();
        UpdateValues();
    }

    void Update()
    {
        
    }

    private void SetButtons()
    {
        info = transform.GetChild(0).GetComponent<Button>();
        throwB = transform.GetChild(1).GetComponent<Button>();
        throwAll = transform.GetChild(2).GetComponent<Button>();
        use = transform.GetChild(3).GetComponent<Button>();
        close = transform.GetChild(4).GetComponent<Button>();
    }

    private void SetButtonsOnClick()
    {
        use.onClick.AddListener(() =>
        {
            try { CurrentProgress.currentProgress.Inventory.Use(index); }
            catch { }
            UpdateValues();
        });
        throwB.onClick.AddListener(Throw);
        throwAll.onClick.AddListener(ThrowAll);
        info.onClick.AddListener(ShowInfo);
        close.onClick.AddListener(() => Destroy(this.gameObject));
    }

    private void Throw()
    {
        CurrentProgress.currentProgress.Inventory.Throw(index, 1);
        UpdateValues();
    }

    private void ThrowAll()
    {
        CurrentProgress.currentProgress.Inventory.Throw(index,
            CurrentProgress.currentProgress.Inventory[index].Item2);
        UpdateValues();
    }
    
    public void SetIndex(int index)
    {
        this.index = index;
        try { UpdateValues(); }
        catch { }
    }

    private void ShowInfo()
    {
        var panel = new InventoryFactory().Instantiate("infPnl");
        panel.transform.SetParent(GameObject.FindGameObjectWithTag("InventoryUI").transform);
        panel.GetComponent<ObjectInfoPanelController>().ShowInfo(index);
    }

    public void UpdateValues()
    {
        var o = CurrentProgress.currentProgress.Inventory[index];
        if (o == null) 
        { 
            Destroy(this.gameObject); 
            return; 
        }

        preview.sprite = o.Item1.GetImage();
        Debugger.Log("" + index + " " + o.Item1.Name);
    }
}
