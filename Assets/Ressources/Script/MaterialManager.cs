using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MaterialManager : MonoBehaviour
{
    [SerializeField] private Material m_Material = default;


    [Range(0f, 0.5f)]
    public float maxOffsetRadius = 0f;
    [Range(0f, 1f)]
    public float maxIrisSizeShrinkOrExpanse = 0f;

    private bool isFocusing = false;
    private bool isTripping = false;

    private Vector2 m_screenCenter;


    private Vector2 m_offset = Vector2.zero;
    private float m_irisFactor = 1f;
    private Color m_irisColor = Color.green;

    public Color irisColor
    {
        get { return m_irisColor; }
    }

    private void Start()
    {
        InputManager.Instance.onPositionMove.AddListener(GetOffsetPosition);
        InputManager.Instance.onFocusMode.AddListener(StartFocusMode);
        InputManager.Instance.onTripMode.AddListener(StartTrippingMode);
        InputManager.Instance.onRelease.AddListener(StopMode);
        m_screenCenter = new Vector2(0.5f, 0.5f);
    }

    private void Update()
    {
        m_Material.SetVector("_displacement", m_offset);
        m_Material.SetFloat("_irisFactor", m_irisFactor);
        m_Material.SetColor("_eyeColor", m_irisColor);
    }

    private void GetOffsetPosition(Vector2 mousePosition01)
    {
        if (Vector2.Distance(mousePosition01, m_screenCenter) > maxOffsetRadius) m_offset = (mousePosition01 - m_screenCenter).normalized * maxOffsetRadius;
        else m_offset = mousePosition01 - new Vector2(0.5f, 0.5f);

        m_offset = -m_offset;
    }

    private void StartFocusMode()
    {
        isFocusing = true;
        isTripping = false;
        StartCoroutine(Focusing());
    }

    private void StartTrippingMode()
    {
        isTripping = true;
        isFocusing = false;
        StartCoroutine(Tripping());
    }

    private void StopMode()
    {
        isFocusing = false;
        isTripping = false;
        StartCoroutine(Releasing());
    }

    private IEnumerator Focusing()
    {
        while (isFocusing)
        {
            if (m_irisFactor > 1 - maxIrisSizeShrinkOrExpanse)
            {
                m_irisFactor -= 0.02f;
                yield return true;
            }
            else { yield return true; }
        }
        yield return null;
    }

    private IEnumerator Tripping()
    {
        while (isTripping)
        {
            if (m_irisFactor < 1 + maxIrisSizeShrinkOrExpanse)
            {
                m_irisFactor += 0.02f;
                yield return true;
            }
            else { yield return true; }
        }
        yield return null;
    }

    private IEnumerator Releasing()
    {
         while (!isFocusing && !isTripping)
         {
            if (m_irisFactor < 0.98f)
            {
                m_irisFactor += 0.02f;
                yield return true;
            }

            else if (m_irisFactor > 1.02f)
            {
                m_irisFactor -= 0.02f;
                yield return true;
            }

            else yield return true;
         }
         yield return null;
    }

    public void ChangeColor(Color color)
    {
        m_irisColor = color;
    }
}
