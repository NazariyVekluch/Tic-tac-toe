using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGrid : MonoBehaviour
{
    public int Row, Column;

    private IEnumerator _corontine;

    IEnumerator ChangeCellSize()
    {
        yield return new WaitForEndOfFrame();

        RectTransform parent = gameObject.GetComponent<RectTransform>();
        GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();

        grid.cellSize = new Vector2((parent.rect.width - grid.padding.left - grid.padding.right - grid.spacing.x * (Column - 1)) / Column,
                                    (parent.rect.height - grid.padding.top - grid.padding.bottom - grid.spacing.y * (Row - 1)) / Row);

        _corontine = null;
    }

    private void OnRectTransformDimensionsChange()
    {
        if (_corontine == null)
        {
            _corontine = ChangeCellSize();

            if (gameObject.activeInHierarchy)
                StartCoroutine(_corontine);
        }
    }
}
