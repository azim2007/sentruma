using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    private int index;
    private Image image;
    private Text count;
    private string objName;
    private bool hasObj;
    private InventoryFactory factory;

    void Start()
    {
        image = this.GetComponent<Image>();
        count = this.transform.GetChild(0).GetComponent<Text>();
        CurrentProgress.currentProgress.Inventory.onChange += UpdateObject;
        UpdateObject();
        transform.localScale = Vector3.one;
        factory = new InventoryFactory();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnDestroy()
    {
        if(index == 0)
            CurrentProgress.currentProgress.Inventory.ResetOnChange();
    }

    public void SetObject(int index)
    {
        this.index = index;
    }

    public void UpdateObject()
    {
        var obj = CurrentProgress.currentProgress.Inventory[index];
        if(obj == null)
        {
            count.text = "";
            hasObj = false;
            image.sprite = InventoryObject.noneImage;
            return;
        }

        hasObj = true;
        count.text = "" + obj.Item2;
        image.sprite = obj.Item1.GetImage();
        objName = obj.Item1.Name;
    }

    public void OnClick()
    {
        var o = GameObject.FindGameObjectWithTag("ObjectBar");
        if (!hasObj && o != null)
        {
            Destroy(o);
        }

        if (o == null)
        {
            o = factory.Instantiate("objBar");
            o.transform.SetParent(GameObject.FindGameObjectWithTag("InventoryUI").transform);
        }

        o.GetComponent<ObjectBarController>().SetIndex(this.index);
    }
}
