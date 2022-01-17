using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{

    public static InputManager instance;

    public delegate void InputEvent();
    public static event InputEvent OnPressDown;
    public static event InputEvent OnPressUp;
    public static event InputEvent OnTap;
    public static event InputEvent KeyPressDown;

    // Use this for initialization
    void Start()
    {
        if (instance == null) instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (OnPressUp != null) OnPressUp();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Check if we are clicking on UI
                if (IsPointerOverUIObject())
                {
                    Debug.Log("Clicked over UI");
                }
                //Check if is IClickable
                else if (hit.transform.GetComponent<IClickable>() != null)
                {
                    IClickable clickable = hit.transform.GetComponent<IClickable>();
                    clickable.OnLeftClick();
                }
                else
                {
                    PlayerController.main.Attack();
                }
            }

        }
        if (Input.anyKeyDown)
        {
            if (KeyPressDown != null) KeyPressDown();
            print("Input Key: " + Input.inputString);
        }


        if (Input.GetKeyDown(KeyCode.I))
        {
            UIManager.instance.ToggleInventory();
        }



    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
