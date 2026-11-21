using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class SwipeDetector : MonoBehaviour

{
private bool swipehandled = true;


void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        Touch.onFingerMove += CheckSwipe;
        Touch.onFingerDown += ResetSwipeHandled;
    }

    void OnDisable()
    {
        Touch.onFingerMove -= CheckSwipe;
        Touch.onFingerDown -= ResetSwipeHandled;
        EnhancedTouchSupport.Disable();
    }

    private void ResetSwipeHandled(Finger finger)
    {
        swipehandled = false;
    }

    private void CheckSwipe(Finger finger)
    {
        if(swipehandled) return;
            var touch = finger.currentTouch;
            Vector2 swipeDelta = touch.screenPosition - touch.startScreenPosition;
            if (swipeDelta.magnitude > 50) // Minimum swipe distance
            {
                float x = swipeDelta.x;
                float y = swipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                     swipehandled = true;
                    if (x > 0)
                    {
                        // Right swipe
                        Debug.Log("Right Swipe");
                        this.gameObject.GetComponent<PlayerMove>().OnSwipeRight();
                    }
                    else
                    {
                        // Left swipe
                        Debug.Log("Left Swipe");
                        this.gameObject.GetComponent<PlayerMove>().OnSwipeLeft();
                    }
                    
                }

            }
           
        }
    }
