using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacles : MonoBehaviour
{
    [SerializeField] private GameObject tentacle1;
    [SerializeField] private GameObject tentacle2;
    [SerializeField] private GameObject tentacle3;
    [SerializeField] private GameObject tentacle4;
    
    public void showTentacles()
    {
        tentacle1.gameObject.SetActive(true);
        tentacle2.gameObject.SetActive(true);
        tentacle3.gameObject.SetActive(true);
        tentacle4.gameObject.SetActive(true);
    }
    public void hideTentacles()
    {
        tentacle1.gameObject.SetActive(false);
        tentacle2.gameObject.SetActive(false);
        tentacle3.gameObject.SetActive(false);
        tentacle4.gameObject.SetActive(false);
    }
}
