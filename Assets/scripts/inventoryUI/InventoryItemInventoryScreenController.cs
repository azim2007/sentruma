using UnityEngine;
using UnityEngine.UI;

public abstract class InventoryItemController : MonoBehaviour
{
    private int index;
    public int Index { get { return index; } }
    private Image image;
    private Text count;
    private string objName;
    public string ObjName { get { return objName; } }
    private bool hasObj;
    public bool HasObj { get { return hasObj; } }
    private InventoryFactory factory;
    public InventoryFactory Factory { get { return factory; } }

    public virtual InventoryObjectsHolder Holder() 
    {
        return null;
    }

    void Start()
    {
        image = this.GetComponent<Image>();
        count = this.transform.GetChild(0).GetComponent<Text>();
        Holder().onChange += UpdateObject;
        UpdateObject();
        transform.localScale = Vector3.one;
        factory = new InventoryFactory();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnDestroy()
    {
        if (index == 0)
            Holder().ResetOnChange();
    }

    public void SetObject(int index)
    {
        this.index = index;
    }

    public void UpdateObject()
    {
        var obj = Holder()[index];
        if (obj == null)
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

    public virtual void OnClick() { }
}

public class InventoryItemInventoryScreenController : InventoryItemController
{
    public override void OnClick()
    {
        var o = GameObject.FindGameObjectWithTag("ObjectBar");
        if (!HasObj && o != null)
        {
            Destroy(o);
        }

        if (o == null)
        {
            o = Factory.Instantiate("objBar");
            o.transform.SetParent(GameObject.FindGameObjectWithTag("InventoryUI").transform);
        }

        o.GetComponent<ObjectBarController>().SetIndex(this.Index);
    }

    public override InventoryObjectsHolder Holder()
    {
        return CurrentProgress.currentProgress.Inventory;
    }

    public override bool Equals(object other)
    {
        if(other == null || !(other is InventoryItemInventoryScreenController))
            return false;
        InventoryItemInventoryScreenController o = (InventoryItemInventoryScreenController)other;
        if(o.Index == this.Index && o.ObjName == this.ObjName)
            return true;
        return false;
    }

    public override int GetHashCode()
    {
        return Index ^ ObjName.Length;
    }
}
