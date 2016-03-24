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

    public void UpdatePlayerJuice(float currentJuice, int playerId)
    {
        Slider slider = null;

        switch (playerId)
        {
            case 1:
                slider = p1Slider;
                break;

            case 2:
                slider = p2Slider;
                break;

            case 3:
                slider = p3Slider;
                break;

            case 4:
                slider = p4Slider;
                break;
        }

        slider.value = currentJuice;
    }

  
    public void UpdatePlayerJuice(float currentJuice, string juiceType, int playerId)
    {
        Slider slider = null;
        Image color = null;

        switch(playerId)
        {
            case 1:
                slider = p1Slider;
                color = p1SliderColor;
                break;

            case 2:
                slider = p2Slider;
                color = p2SliderColor;
                break;

            case 3:
                slider = p3Slider;
                color = p3SliderColor;
                break;

            case 4:
                slider = p4Slider;
                color = p4SliderColor;
                break;
        }

        if (juiceType == "JuiceGravity")
        {
            color.color = new Color(211 / 255f, 19 / 255f, 90 / 255f);
        }
        else if (juiceType == "JuicePortal")
        {
            color.color = new Color(139 / 255f, 197 / 255f, 62 / 255f);
        }
        else if (juiceType == "JuiceRewind")
        {
            color.color = new Color(0, 255 / 255f, 255 / 255f);
        }

        slider.value = currentJuice;
    }
}
