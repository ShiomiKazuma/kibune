using UnityEngine;

public class PlayerUIPointer : MonoBehaviour
{
    /// <summary>UIPointerを投影するCanvasの参照</summary>
    [SerializeField, Header("UIPointerを投影するCanvas")] private Canvas _canvas;
    /// <summary>UIPointerを投影するCanvasのRectTransformの参照</summary>
    [SerializeField, Header("UIPointerを投影するInventoryのRectTransform")] private RectTransform _canvasTransform;
    /// <summary>UIPointerのRectTransformの参照</summary>
    [SerializeField, Header("UIPointerのRectTransform")] private RectTransform _cursorTransform;

    void Update()
    {
        // CanvasのRectTransform内にあるマウスの座標をローカル座標に変換する
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasTransform,
            Input.mousePosition,
            _canvas.worldCamera,
            out var mousePosition);

        // ポインターをマウスの座標に移動する
        _cursorTransform.anchoredPosition = new Vector2(mousePosition.x, mousePosition.y);
    }
}
