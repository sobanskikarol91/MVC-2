using System;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class SlotView : MonoBehaviour, ISlotView
{
    private Button button;
    private GameObject content;

    public event EventHandler<OnSlotClickedEventArgs> Clicked;
    public GameObject selector;


    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    public GameObject Content
    {
        get => content;
        set
        {
            content = value;

            if (content)
                content.transform.SetParent(transform, false);
        }
    }

    private void OnButtonClicked()
    {
        Clicked?.Invoke(this, new OnSlotClickedEventArgs());
    }

    public void Select()
    {
        selector.SetActive(true);
    }

    public void Deselect()
    {
        selector.SetActive(false);
    }

    public void DisableInteraction()
    {
        button.interactable = false;
    }

    public void EnableInteraction()
    {
        button.interactable = true;
    }
}

public class OnSlotClickedEventArgs : EventArgs { }

public interface ISlotView
{
    event EventHandler<OnSlotClickedEventArgs> Clicked;
    GameObject Content { get; set; }
    void Select();
    void Deselect();
    void DisableInteraction();
    void EnableInteraction();
}

public class SlotViewFactory : ISlotViewFactory
{
    public ISlotView Create(Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>("View/Slot");
        GameObject instance = UnityEngine.Object.Instantiate(prefab);
        instance.name = prefab.name;
        instance.transform.SetParent(parent, false);
        return instance.GetComponent<ISlotView>();
    }
}

public interface ISlotViewFactory
{
    ISlotView Create(Transform parent);
}