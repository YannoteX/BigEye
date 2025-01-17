using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public UnityEvent<Vector2> onPositionMove = new UnityEvent<Vector2>();
    public UnityEvent onFocusMode = new UnityEvent();
    public UnityEvent onTripMode = new UnityEvent();
    public UnityEvent onRelease = new UnityEvent();

    private Vector2 m_Position = new Vector2();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.mousePresent)
        {
            if (Input.mousePositionDelta != Vector3.zero)
            {
                m_Position.x = Input.mousePosition.x / Screen.width;
                m_Position.y = Input.mousePosition.y / Screen.height;
                onPositionMove.Invoke(m_Position);
            }

            if (Input.GetKeyDown("space")) onFocusMode.Invoke();

            if (Input.GetKeyDown("return")) onTripMode.Invoke();

            if (Input.GetKeyUp("space") || Input.GetKeyUp("return")) onRelease.Invoke();
        }
    }
}
