using UnityEngine;
using UnityEngine.UI;

public class ObjectBarController : MonoBehaviour
{
    private Button throwB, throwAll, use;
    private Text objName;
    private Text objInfo;
    private int index;
    void Start()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        transform.localPosition = new Vector3(-280f, -60f, 0f);
        throwB = transform.GetChild(1).GetComponent<Button>();
        throwAll = transform.GetChild(2).GetComponent<Button>();
        use = transform.GetChild(3).GetComponent<Button>();
        objName = transform.GetChild(0).GetComponent<Text>();
        objInfo = transform.GetChild(4).GetComponent<Text>();
        SetButtonsOnClick();
        UpdateValues();
    }

    void Update()
    {
        
    }

    private void SetButtonsOnClick()
    {
        use.onClick.AddListener(() =>
        {
            CurrentProgress.currentProgress.Inventory.Use(index);
            UpdateValues();
        });
        throwB.onClick.AddListener(Throw);
        throwAll.onClick.AddListener(ThrowAll);
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

    public void UpdateValues()
    {
        var o = CurrentProgress.currentProgress.Inventory[index];
        if (o == null) 
        { 
            Destroy(this.gameObject); 
            return; 
        }

        Debugger.Log("" + index + " " + o.Item1.Name);
        objName.text = o.Item1.Name;
        objInfo.text = o.Item1.Description;
        use.gameObject.SetActive(o.Item1 is IUsableObject);
    }
}
