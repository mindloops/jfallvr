using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GazeButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color normalHighLight;

    public Color maxHighlight;

    private Button button;

    private bool gazing;

    private bool gazePressed;

    private float startGaze = -1;

    public void OnPointerEnter(PointerEventData eventData)
    {
        gazing = true;
        startGaze = Time.time;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gazePressed = false;
        gazing = false;
        startGaze = -1;
        SetHightlightColor(normalHighLight);
    }

    private void Update()
    {
        if (gazing)
        {
            float timeGazing = Time.time - startGaze;
            if (!gazePressed && timeGazing >= 2.0f)
            {
                gazePressed = true;
                button.onClick.Invoke();
            }
            if (gazePressed)
            {
                SetHightlightColor(normalHighLight);
            }
            else
            {
                Color lerped = Color.Lerp(normalHighLight, maxHighlight, timeGazing / 2.0f);
                SetHightlightColor(lerped);
            }
        }
    }

    private void SetHightlightColor(Color color)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.highlightedColor = color;
        button.colors = colorBlock;
    }

    private void OnEnable()
    {
        button = GetComponent<Button>();
    }
}
