using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{

    public Button Button;

    void Start()
    {
        Button.onClick.AddListener(ClickAnimation);
    }


    public void ClickAnimation()
    {
        transform.DOPunchScale(Vector3.one * 0.3f, 0.1f).SetEase(Ease.InOutBounce).OnComplete(() =>
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        });


    }
}


