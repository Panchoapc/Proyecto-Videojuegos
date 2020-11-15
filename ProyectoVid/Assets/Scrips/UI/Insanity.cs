using UnityEngine;
using UnityEngine.UI;

public class Insanity : MonoBehaviour {
    public Slider slider = null;

    public void SetSanity(int sanity) {
        slider.value = sanity;
    }
}
