using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public Image TopImage;
    public Image BotImage;

    public Image LoadingCircle;
    public Image LoadingCircleFill;
    public Image LoadingGlider;


    private bool _open = false;
	void Start ()
	{
	    TopImage.fillAmount = 1;
	    BotImage.fillAmount = 1;

	    StartCoroutine(InitAnimation());
	}
	

    private IEnumerator InitAnimation()
    {
        yield return new WaitForSeconds(1);
        Animate();
        _open = true;
    }

    public void Animate()
    {
        if (_open)
        {
            TopImage.DOFillAmount(1, 0.5f);
            BotImage.DOFillAmount(1, 0.5f);

        }
        else
        {
            TopImage.DOFillAmount(0, 0.5f);
            BotImage.DOFillAmount(0, 0.5f);
        }
    }
}
