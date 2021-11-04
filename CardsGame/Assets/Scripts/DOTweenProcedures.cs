using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class DOTweenProcedures : MonoBehaviour
{
    [SerializeField]
    private static TweenerCore<float, float, FloatOptions> _fadeTween; //отслеживание состояние Tween'а

    /// <summary>
    /// Эффект Bounce
    /// </summary>
    /// <param name="bounceTransform">Transform для которого необходимо применить эффект</param>
    /// <returns>Tween последовательности</returns>
    public static Tween BounceTransform(Transform bounceTransform)
    {
        var seq = DOTween.Sequence();
        seq.Append(bounceTransform.DOScale(1.1f, .2f));
        seq.Append(bounceTransform.DOScale(.95f, .2f));
        seq.Append(bounceTransform.DOScale(1f, .5f));
        return seq;
    }

    /// <summary>
    /// Эффект дергания в стороны
    /// </summary>
    /// <param name="shakeTransform">Transform для которого необходимо применить эффект</param>
    /// <returns>Tween последовательности</returns>
    public static Tween ShakeTransform(Transform shakeTransform)
    {
        var seq = DOTween.Sequence();
        seq.Append(shakeTransform.DOShakePosition(1f,5f).SetEase(Ease.InBounce));
        return seq;
    }

    /// <summary>
    /// Эффект FadeIn
    /// </summary>
    /// <param name="fadeInGroup">CanvasGroup для которого необходимо применить эффект</param>
    /// <returns>Tween</returns>
    public static Tween FadeInGroup(CanvasGroup fadeInGroup)
    {
        return Fade(fadeInGroup, 1f, 1f);
    }

    /// <summary>
    /// Эффект FadeOut
    /// </summary>
    /// <param name="fadeOutGroup">CanvasGroup для которого необходимо применить эффект</param>
    /// <returns>Tween</returns>
    public static Tween FadeOutGroup(CanvasGroup fadeOutGroup)
    {
        return Fade(fadeOutGroup, 0f, 1f);
    }

    /// <summary>
    /// Настраиваемый эффект Fade
    /// </summary>
    /// <param name="fadeOutGroup">CanvasGroup для которого необходимо применить эффект</param>
    /// <param name="value">значение</param>
    /// <param name="duration">продолжительность</param>
    /// <returns>Tween</returns>
    public static Tween CustomFadeGroup(CanvasGroup fadeOutGroup, float value, float duration)
    {
        return Fade(fadeOutGroup, value, duration);
    }

    /// <summary>
    /// Обработчик эффекта Fade
    /// </summary>
    /// <param name="fadeGroup">CanvasGroup для которого необходимо применить эффект</param>
    /// <param name="value">значение</param>
    /// <param name="duration">продолжительность</param>
    /// <returns>Tween</returns>
    private static Tween Fade(CanvasGroup fadeGroup, float value, float duration)
    {
        _fadeTween?.Kill();
        _fadeTween = fadeGroup.DOFade(value, duration);
        return _fadeTween;
    }
}
