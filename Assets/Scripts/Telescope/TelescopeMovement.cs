using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelescopeMovement : MonoBehaviour
{

    IEnumerator movementCoroutine;

    [SerializeField] RectTransform telescopeView;


    [HideInInspector] public static bool moving;
    int currentArrow;
    [SerializeField] float telescopeSpeed = 5f;
    [SerializeField] float maxX = 1000f;
    [SerializeField] float maxY = 1000f;

    [SerializeField] TextMeshProUGUI xCoordinateText;
    [SerializeField] TextMeshProUGUI yCoordinateText;

    [SerializeField] AudioSource buttonClick;
    [SerializeField] AudioSource cameraMove;
    [SerializeField] AudioSource buttonUnclick;

    [SerializeField] NavArrow[] navArrows;

    enum Arrow
    {
        down,
        up,
        left,
        right,
        stop
    }

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        currentArrow = 4;

        xCoordinateText.text = ((int) -telescopeView.anchoredPosition.x).ToString();
        yCoordinateText.text = ((int) -telescopeView.anchoredPosition.y).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnArrowPress(int arrow)
    {
        if(!moving)
        {
            if(arrow == (int) Arrow.down && currentArrow != arrow)
            {
                movementCoroutine = MoveTelescope(Vector2.up);
                foreach(NavArrow navArrow in navArrows) navArrow.ResetArrow();
                navArrows[(int) Arrow.down].ChangeArrowPress();
                currentArrow = arrow;
            }
            else if(arrow == (int) Arrow.up && currentArrow != arrow)
            {
                movementCoroutine = MoveTelescope(Vector2.down);
                foreach(NavArrow navArrow in navArrows) navArrow.ResetArrow();
                navArrows[(int) Arrow.up].ChangeArrowPress();
                currentArrow = arrow;
            }
            else if(arrow == (int) Arrow.right && currentArrow != arrow)
            {
                movementCoroutine = MoveTelescope(Vector2.left);
                foreach(NavArrow navArrow in navArrows) navArrow.ResetArrow();
                navArrows[(int) Arrow.right].ChangeArrowPress();
                currentArrow = arrow;
            }
            else if(arrow == (int) Arrow.left && currentArrow != arrow)
            {
                movementCoroutine = MoveTelescope(Vector2.right);
                foreach(NavArrow navArrow in navArrows) navArrow.ResetArrow();
                navArrows[(int) Arrow.left].ChangeArrowPress();
                currentArrow = arrow;
            }
            if(arrow != (int) Arrow.stop)
            {
                moving = true;
                buttonClick.Play();
                cameraMove.Play();
                StartCoroutine(movementCoroutine);
            }
        }
        else
        {
            if(telescopeView.anchoredPosition.x >= maxX) telescopeView.anchoredPosition = new Vector2(maxX - 1, telescopeView.anchoredPosition.y);
            if(telescopeView.anchoredPosition.y >= maxY) telescopeView.anchoredPosition = new Vector2(telescopeView.anchoredPosition.x, maxY - 1);
            if(telescopeView.anchoredPosition.x <= -maxX) telescopeView.anchoredPosition = new Vector2(-maxX + 1, telescopeView.anchoredPosition.y);
            if(telescopeView.anchoredPosition.y <= -maxY) telescopeView.anchoredPosition = new Vector2(telescopeView.anchoredPosition.x, -maxY + 1);
            
            moving = false;
            if(movementCoroutine != null) 
            {
                StopCoroutine(movementCoroutine);
                buttonUnclick.Play();
            }

            if(currentArrow != arrow) OnArrowPress(arrow);
            else {
                cameraMove.Stop();
                foreach(NavArrow navArrow in navArrows) navArrow.ResetArrow();
                currentArrow = 4;
            }
        }
    }

    private IEnumerator MoveTelescope(Vector2 movementVector)
    {
        while(telescopeView.anchoredPosition.x < maxX 
        && telescopeView.anchoredPosition.x > -maxX
        && telescopeView.anchoredPosition.y < maxY 
        && telescopeView.anchoredPosition.y > -maxY)
        {
            telescopeView.anchoredPosition += movementVector * Time.deltaTime * telescopeSpeed;
            yield return null;
            xCoordinateText.text = ((int) -telescopeView.anchoredPosition.x).ToString();
            yCoordinateText.text = ((int) -telescopeView.anchoredPosition.y).ToString();
        }
        
        if(telescopeView.anchoredPosition.x >= maxX) telescopeView.anchoredPosition = new Vector2(maxX - 1, telescopeView.anchoredPosition.y);
        if(telescopeView.anchoredPosition.y >= maxY) telescopeView.anchoredPosition = new Vector2(telescopeView.anchoredPosition.x, maxY - 1);
        if(telescopeView.anchoredPosition.x <= -maxX) telescopeView.anchoredPosition = new Vector2(-maxX + 1, telescopeView.anchoredPosition.y);
        if(telescopeView.anchoredPosition.y <= -maxY) telescopeView.anchoredPosition = new Vector2(telescopeView.anchoredPosition.x, -maxY + 1);

        xCoordinateText.text = ((int) -telescopeView.anchoredPosition.x).ToString();
        yCoordinateText.text = ((int) -telescopeView.anchoredPosition.y).ToString();

        moving = true;
        OnArrowPress((int) Arrow.stop);

        cameraMove.Stop();
        foreach(NavArrow navArrow in navArrows) navArrow.ResetArrow();
        currentArrow = 4;
    }
}
