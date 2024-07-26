using System.Collections;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private TMP_Text _popupText;
    [SerializeField] private bool _autoHide;

    private const float POPUP_LIFETIME = 3f;

    private void Start()
    {
        if (_autoHide)
        {
            StartCoroutine(Hide());
        }
    }

    public void SetText(string text)
    {
        _popupText.text = text;
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(POPUP_LIFETIME);
        Destroy(gameObject);
    }
}
