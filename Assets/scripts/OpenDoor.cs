using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator doorAnimator; // Ссылка на аниматор двери

    private bool allItemsTaken = false; // Флаг, указывающий, что все предметы подобраны

    // Метод для проверки, подобраны ли все предметы
    void CheckItemsTaken()
    {
        if (allItemsTaken)
        {
            // Если все предметы подобраны, устанавливаем триггер "OpenDoor" в true
            doorAnimator.SetTrigger("OpenDoor");
        }
    }
}
