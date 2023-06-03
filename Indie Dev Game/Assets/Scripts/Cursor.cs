using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cursor : MonoBehaviour
{
    public GameObject crosshair;
    private InputActions inputActions;
    private Vector3 target;

    private void Start()
    {
        UnityEngine.Cursor.visible = false;
    }

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPosition = inputActions.Player.Aim.ReadValue<Vector2>();
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, transform.position.z));
        crosshair.transform.position = new Vector2(target.x, target.y);
    }
}
