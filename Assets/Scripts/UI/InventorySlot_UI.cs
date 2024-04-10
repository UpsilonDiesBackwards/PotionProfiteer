using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedInventorySlot;

    Ray2D ray2;
    RaycastHit2D hit2;

    private Button button;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay { get; private set; }
    private void Awake()
    {
        ClearSlot();

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();

    }
    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot(slot);

    }
    
    public void UpdateUISlot(InventorySlot slot)
    {
        if(slot.ItemData != null)
        {
            gameObject.layer = 7; //should be set to Resource Layer aka Num 7 
            itemSprite.sprite = slot.ItemData.Icon;
            itemSprite.color = Color.white;

            Vector2 worldpoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit2 = Physics2D.Raycast(worldpoint, Vector2.zero);
            if (hit2.collider != null)
            {
                Debug.Log(hit2.collider.name);
                Debug.Log("Maybe this works");
            }


            if (slot.StackSize > 1) itemCount.text = slot.StackSize.ToString();
            else itemCount.text = "";
        }
        else
        {
            ClearSlot();
        }
               
    }
    
    public void UpdateUISlot()
    {
        if (assignedInventorySlot != null) UpdateUISlot(assignedInventorySlot);      
    }
    
    public void ClearSlot()
    {
        assignedInventorySlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }
    public void OnUISlotClick()
    {
        ParentDisplay.SlotClicked(this);
    }
}
