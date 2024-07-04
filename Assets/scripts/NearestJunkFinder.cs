using UnityEngine;

public class NearestJunkFinder : MonoBehaviour
{
    public GameObject[] junkObjects; // Массив объектов мусора
    public float searchRadius = 10f; // Радиус поиска, настраиваемый в инспекторе

    public GameObject FindNearestJunk(Vector3 currentPosition)
    {
        GameObject nearestJunk = null;
        float minDistance = searchRadius;

        foreach (GameObject junk in junkObjects)
        {
            if (junk != null) // Убедитесь, что объект существует
            {
                float distance = Vector3.Distance(junk.transform.position, currentPosition);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestJunk = junk;
                }
            }
        }

        return nearestJunk;
    }
}
