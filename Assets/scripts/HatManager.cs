using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatManager : MonoBehaviour
{
    public GameObject characterHat; // Ўл€па на персонаже

    // ћетод дл€ подбора шл€пы с земли
    public void TakeHat()
    {
        characterHat.SetActive(true); // јктивируем шл€пу на персонаже
    }
}
