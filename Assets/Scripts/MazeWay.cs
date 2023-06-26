using UnityEngine;

namespace TowerDefense
{
    public class MazeWay : MonoBehaviour
    {
        [SerializeField] public GameObject[] mazeWay;

        private void Start()
        {
            mazeWay = new GameObject[transform.childCount];
            for (int i = 0; i <= transform.childCount-1; i++)
            {
                mazeWay[i] = transform.GetChild(i).gameObject;
            }
        }
    }
}
