using UnityEngine;
using UnityEngine.InputSystem;

public class RaisedItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        _controller = controller;
        _controller.RaiseItem(_controller.Cursor.Item);
    }

    public void Exit() {
        _controller.DropPutItem();
    }

    public void Update() {
        _controller.Cursor.FollowTheMouse();
        _controller.Cursor.StartRaycast();

        if (Mouse.current.leftButton.wasPressedThisFrame) {

            if (_controller.Cursor.RaycastHit2D.transform == null) {
                _controller.ChangeState(_controller.DropItemState);
                Debug.Log("object null");
                return;
            }
            
            Debug.Log("name obj = " + _controller.Cursor.RaycastHit2D.transform);
             _controller.cell = _controller.Cursor.RaycastHit2D.transform.GetComponent<Cell>();

            if (_controller.cell == null) {
                Debug.Log("cell is null");
                return;
            }

            if (!_controller.cell.IsAvailableForInteraction) {
                Debug.Log("cell not avaible for iteraction");
                return;
            }

            if (_controller.cell.HasItem) {
                Debug.Log("cell not empty");
                _controller.ChangeState(_controller.SwapItemState);
                return;
            }

            _controller.ChangeState(_controller.PutItemState);
        }
    }
}