using UnityEngine;

public class Tentacles : MonoBehaviour {
    [SerializeField] private GameObject tentacle1 = null;
    [SerializeField] private GameObject tentacle2 = null;
    [SerializeField] private GameObject tentacle3 = null;
    [SerializeField] private GameObject tentacle4 = null;

    public void ShowTentacles() {
        tentacle1.gameObject.SetActive(true);
        tentacle2.gameObject.SetActive(true);
        tentacle3.gameObject.SetActive(true);
        tentacle4.gameObject.SetActive(true);
    }
    public void HideTentacles() {
        tentacle1.gameObject.SetActive(false);
        tentacle2.gameObject.SetActive(false);
        tentacle3.gameObject.SetActive(false);
        tentacle4.gameObject.SetActive(false);
    }
}
