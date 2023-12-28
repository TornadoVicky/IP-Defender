using UnityEngine;

public class FlipSprite : MonoBehaviour {

    private Vector3 lastPosition;

    private void Start () {
        lastPosition = transform.position;
    }

    private void Update () {
        Vector3 currentPosition = transform.position;

        if (currentPosition.x < lastPosition.x) {
            FlipSpriteX(true);
        } else if (currentPosition.x > lastPosition.x) {
            FlipSpriteX(false);
        }

        lastPosition = currentPosition;
    }

    private void FlipSpriteX(bool flip) {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        transform.localScale = scale;
    }
}
