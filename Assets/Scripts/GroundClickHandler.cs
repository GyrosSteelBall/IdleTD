using UnityEngine;
using UnityEngine.EventSystems;

public class GroundClickHandler : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check for left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0) && (EventSystem.current.IsPointerOverGameObject() ||
            (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))))
            {
                // If we're over a UI element, return and don't process clicks on game objects.
                return;
            }
            // Convert mouse position to world position for 2D Raycast
            Vector2 mousePos2D = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // Perform the Raycast towards the mouse position
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // Check if the raycast hit the ground layer (set this layer accordingly in your Unity Editor)
            if (!hit || hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                // If nothing is hit, or it hits the "Ground" layer, then deselect the unit
                UIManager.Instance.DeselectUnit();
            }
        }
    }
}