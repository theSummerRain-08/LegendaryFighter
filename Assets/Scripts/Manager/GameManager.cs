using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public Transform[] limitedPoint;
    public float LimitedLeft() { return limitedPoint[0].position.x; }
    public float LimitedRight() { return limitedPoint[1].position.x; }
    public float LimitedTop() { return limitedPoint[0].position.y; }
    public float GroundPosition() { return limitedPoint[1].position.y; }
    public Vector3 LimitPosition(Vector3 position) {
        float limitedX = Mathf.Clamp(position.x, LimitedLeft(), LimitedRight());
        float limitedY = Mathf.Clamp(position.y, GroundPosition(), LimitedTop());
        return new Vector3(limitedX, limitedY, position.z);
    }

}