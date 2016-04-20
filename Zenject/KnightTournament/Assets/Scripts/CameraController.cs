using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {



    private Vector3 startPosition;
    private Vector2 topLeftTargetPadding;
	private Transform targetTransform;
	private Vector3 targetStartPosition;
    private Vector2 targetDelta;

    public enum TargerAlign {Center,TopLeft,TopRight};


    public GameObject target;
	public bool followX = true;
	public bool followY = true;
    public TargerAlign targetAlign = TargerAlign.Center;

    void Awake()
    {
		targetTransform = target.transform;
		targetStartPosition = targetTransform.position;
        startPosition = transform.position;

        topLeftTargetPadding = new Vector2(targetStartPosition.x - TopLeftPoint.x, targetStartPosition.y - TopLeftPoint.y);
	}
	
	private void FixedUpdate(){
        UpdateTargetDelta();
        if (targetAlign == TargerAlign.Center)
            UpdateCenterAlign();
        else if(targetAlign == TargerAlign.TopLeft)
            UpdateTopLeftAlign();
	}

    private void UpdateTargetDelta()
    {
        float targetDeltaX = targetTransform.position.x - targetStartPosition.x;
        float targetDeltaY = targetTransform.position.y - targetStartPosition.y;
        targetDelta = new Vector2(targetDeltaX, targetDeltaY);
    }
    
    private void UpdateCenterAlign()
    {
        float curPositionX = followX ? targetDelta.x + startPosition.x : transform.position.x;
        float curPositionY = followY ? targetDelta.y + startPosition.y : transform.position.y;
        transform.position = new Vector3(curPositionX, curPositionY, transform.position.z);
    }

    private void UpdateTopLeftAlign()
    {
        if (!followX && !followY)
            return;
        float curPositionX = followX ? targetTransform.position.x : transform.position.x;
        float curPositionY = followY ? targetTransform.position.y : transform.position.y;
        
        curPositionX += (WorldWidth/2 - topLeftTargetPadding.x);
        transform.position = new Vector3(curPositionX, curPositionY, transform.position.z);
    }

    public void Maximize()
    {
        GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
        FixedUpdate();
        followX = false;
    }

    public Vector2 TopLeftPoint
    {
        get { return GetComponent<Camera>().ViewportToWorldPoint(new Vector2(GetComponent<Camera>().rect.x, GetComponent<Camera>().rect.y)); }
    }

    public float WorldWidth
    {
        get { return ((GetComponent<Camera>().orthographicSize * 2) * Screen.width / Screen.height) * GetComponent<Camera>().rect.width; }
    }

    public Camera Camera { get { return GetComponent<Camera>(); } }
}
