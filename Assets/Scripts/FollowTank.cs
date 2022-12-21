using UnityEngine;

public class FollowTank : MonoBehaviour
{
    [SerializeField]
    private Transform pivot;
    // Update is called once per frame
    void Update()
    {
        Vector3 sliderPosition = pivot.transform.position;
        transform.position = sliderPosition;
    }
}
