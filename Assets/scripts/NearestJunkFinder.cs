using UnityEngine;

public class NearestJunkFinder : MonoBehaviour
{
    public GameObject[] junkObjects; // ������ �������� ������
    public float searchRadius = 10f; // ������ ������, ������������� � ����������

    public GameObject FindNearestJunk(Vector3 currentPosition)
    {
        GameObject nearestJunk = null;
        float minDistance = searchRadius;

        foreach (GameObject junk in junkObjects)
        {
            if (junk != null) // ���������, ��� ������ ����������
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
