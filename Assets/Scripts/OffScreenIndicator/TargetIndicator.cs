using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private Image targetIndicatorImage;
    [SerializeField] private Image offScreenTargetIndicator;
    [SerializeField] private float outOfSightOffset = 20f;
    
    // ---- / Private Variables / ---- //
    private float outOfSightOffest { get { return outOfSightOffset /* canvasRect.localScale.x*/; } }
    private GameObject _target;
    private Camera _mainCamera;
    private RectTransform _canvasRect;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCamera = Camera.main;
    }


    public void InitialiseTargetIndicator(GameObject target, Camera mainCamera, Canvas canvas)
    {
        this._target = target;
        this._mainCamera = mainCamera;
        _canvasRect = canvas.GetComponent<RectTransform>();
    }

    public void UpdateTargetIndicator()
    {
        SetIndicatorPosition();

        //Adjust distance display
        //Turn on or off when in range/out of range
        //Do stuff if picked as main target
    }


    protected void SetIndicatorPosition()
    {

        //Get the position of the target in relation to the screenSpace 
        Vector3 indicatorPosition = _mainCamera.WorldToScreenPoint(_target.transform.position);
        //Debug.Log("GO: "+ gameObject.name + "; slPos: " + indicatorPosition + "; cvWidt: " + canvasRect.rect.width + "; cvHeight: " + canvasRect.rect.height);

        //In case the target is both in front of the camera and within the bounds of its frustrum
        if (indicatorPosition.z >= 0f & indicatorPosition.x <= _canvasRect.rect.width * _canvasRect.localScale.x
         & indicatorPosition.y <= _canvasRect.rect.height * _canvasRect.localScale.x & indicatorPosition.x >= 0f & indicatorPosition.y >= 0f)
        {

            //Set z to zero since it's not needed and only causes issues (too far away from Camera to be shown!)
            indicatorPosition.z = 0f;

            //Target is in sight, change indicator parts around accordingly
            TargetOutOfSight(false, indicatorPosition);
        }

        //In case the target is in front of the ship, but out of sight
        else if (indicatorPosition.z >= 0f)
        {
            //Set indicatorposition and set targetIndicator to outOfSight form.
            indicatorPosition = OutOfRangeIndicatorPositionB(indicatorPosition);
            TargetOutOfSight(true, indicatorPosition);
        }
        else
        {
            //Invert indicatorPosition! Otherwise the indicator's positioning will invert if the target is on the backside of the camera!
            indicatorPosition *= -1f;

            //Set indicatorposition and set targetIndicator to outOfSight form.
            indicatorPosition = OutOfRangeIndicatorPositionB(indicatorPosition);
            TargetOutOfSight(true, indicatorPosition);

        }

        //Set the position of the indicator
        _rectTransform.position = indicatorPosition;

    }

    private Vector3 OutOfRangeIndicatorPositionB(Vector3 indicatorPosition)
    {
        //Set indicatorPosition.z to 0f; We don't need that and it'll actually cause issues if it's outside the camera range (which easily happens in my case)
        indicatorPosition.z = 0f;

        var rectHeight = _canvasRect.rect.height;
        var rectWidth = _canvasRect.rect.width;
        //Calculate Center of Canvas and subtract from the indicator position to have indicatorCoordinates from the Canvas Center instead the bottom left!
        Vector3 canvasCenter = new Vector3(rectWidth / 2f, rectHeight / 2f, 0f) * _canvasRect.localScale.x;
        indicatorPosition -= canvasCenter;

        //Calculate if Vector to target intersects (first) with y border of canvas rect or if Vector intersects (first) with x border:
        //This is required to see which border needs to be set to the max value and at which border the indicator needs to be moved (up & down or left & right)
        float divX = (rectWidth / 2f - outOfSightOffest) / Mathf.Abs(indicatorPosition.x);
        float divY = (rectHeight / 2f - outOfSightOffest) / Mathf.Abs(indicatorPosition.y);

        //In case it intersects with x border first, put the x-one to the border and adjust the y-one accordingly (Trigonometry)
        if (divX < divY)
        {
            float angle = Vector3.SignedAngle(Vector3.right, indicatorPosition, Vector3.forward);
            indicatorPosition.x = Mathf.Sign(indicatorPosition.x) * (rectWidth * 0.5f - outOfSightOffest) * _canvasRect.localScale.x;
            indicatorPosition.y = Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPosition.x;
        }

        //In case it intersects with y border first, put the y-one to the border and adjust the x-one accordingly (Trigonometry)
        else
        {
            float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition, Vector3.forward);

            indicatorPosition.y = Mathf.Sign(indicatorPosition.y) * (rectHeight / 2f - outOfSightOffest) * _canvasRect.localScale.y;
            indicatorPosition.x = -Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPosition.y;
        }

        //Change the indicator Position back to the actual rectTransform coordinate system and return indicatorPosition
        indicatorPosition += canvasCenter;
        return indicatorPosition;
    }



    private void TargetOutOfSight(bool oos, Vector3 indicatorPosition)
    {
        //In Case the indicator is OutOfSight
        if (oos)
        {
            //Activate and Deactivate some stuff
            if (offScreenTargetIndicator.gameObject.activeSelf == false) offScreenTargetIndicator.gameObject.SetActive(true);
            if (targetIndicatorImage.isActiveAndEnabled == true) targetIndicatorImage.enabled = false;

            //Set the rotation of the OutOfSight direction indicator
            offScreenTargetIndicator.rectTransform.rotation = Quaternion.Euler(RotationOutOfSightTargetIndicator(indicatorPosition));

            //outOfSightArrow.rectTransform.rotation  = Quaternion.LookRotation(indicatorPosition- new Vector3(canvasRect.rect.width/2f,canvasRect.rect.height/2f,0f)) ;
            /*outOfSightArrow.rectTransform.rotation = Quaternion.LookRotation(indicatorPosition);
            viewVector = indicatorPosition- new Vector3(canvasRect.rect.width/2f,canvasRect.rect.height/2f,0f);
            
            //Debug.Log("CanvasRectCenter: " + canvasRect.rect.center);
            outOfSightArrow.rectTransform.rotation *= Quaternion.Euler(0f,90f,0f);*/
        }

        //In case that the indicator is InSight, turn on the inSight stuff and turn off the OOS stuff.
        else
        {
            if (offScreenTargetIndicator.gameObject.activeSelf == true) offScreenTargetIndicator.gameObject.SetActive(false);
            if (targetIndicatorImage.isActiveAndEnabled == false) targetIndicatorImage.enabled = true;
        }
    }


    private Vector3 RotationOutOfSightTargetIndicator(Vector3 indicatorPosition)
    {
        //Calculate the canvasCenter
        Vector3 canvasCenter = new Vector3(_canvasRect.rect.width / 2f, _canvasRect.rect.height / 2f, 0f) * _canvasRect.localScale.x;

        //Calculate the signedAngle between the position of the indicator and the Direction up.
        float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition - canvasCenter, Vector3.forward);

        //return the angle as a rotation Vector
        return new Vector3(0f, 0f, angle);
    }
}