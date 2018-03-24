using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Button Button;
    public SceneSwitcher SceneSwitcher;

    private void Start()
    {
        Button.onClick.AddListener(ClickAnimation);
    }
    public void ClickAnimation()
    {     
        transform.DOPunchScale(Vector3.one * 0.3f, 0.1f).SetEase(Ease.InOutBounce)
            .OnComplete(() =>
            {
                StartCoroutine(SwitchAnimation());
            });
    }

    private IEnumerator SwitchAnimation()
    {
        SceneSwitcher.Animate();

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(1, LoadSceneMode.Single);      
    }
}
