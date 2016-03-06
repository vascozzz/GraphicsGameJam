using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDManager : MonoBehaviour 
{
    public static HUDManager instance = null;

    [SerializeField] private Slider p1Slider;
    [SerializeField] private Slider p2Slider;
    [SerializeField] private Slider p3Slider;
    [SerializeField] private Slider p4Slider;

    [SerializeField] private Image p1SliderColor;
    [SerializeField] private Image p2SliderColor;
    [SerializeField] private Image p3SliderColor;
    [SerializeField] private Image p4SliderColor;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
    }

	void Start () 
    {
	
	}

    public void UpdatePlayerJuice(float currentJuice)
    {
        p1Slider.value = currentJuice;
    }

    public void UpdatePlayerJuice(float currentJuice, string juiceType)
    {
        if (juiceType == "JuiceGravity")
        {
            p1SliderColor.color = Color.magenta;
        }
        else if (juiceType == "JuicePortal")
        {
            p1SliderColor.color = Color.green;
        }
        else if (juiceType == "JuiceRewind")
        {
            p1SliderColor.color = Color.blue;
        }

        p1Slider.value = currentJuice;
    }
}
