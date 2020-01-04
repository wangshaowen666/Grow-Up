using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public static class WTool
{
    //_____________________________________________________________________动画效果相关

    /// <summary>
    /// icon振动效果（UI下dic要传入大一点）
    /// </summary>
    /// <param name="trans">Trans.</param>
    /// <param name="duration">周期.</param>
    /// <param name="dic">力的大小与方向.</param>
    /// <param name="count">次数.</param>
    public static Tweener Vibrate(this Transform trans, float duration, Vector3 dic, int count)
    {
		DOTween.Kill(trans);
        var temp=trans.DOShakePosition(duration, dic, count, 90).SetLoops(-1);
        return temp;
    }

    /// <summary>
    /// icon呼吸效果
    /// </summary>
    /// <param name="trans">Trans.</param>
    /// <param name="addValue">增量，是增！！！.</param>
    /// <param name="duration">周期.</param>
    /// <param name="count">次数.</param>
    /// <param name="guanxing">惯性.</param>
	public static void Breathe(this Transform trans,Vector3 addValue,float duration=2,int count=1,float guanxing=0.2f,Vector3? defaultLocalScale = null)
    {
		DOTween.Kill(trans,true);
		Vector3 localScale = trans.localScale;
		if (defaultLocalScale != null)
			localScale = defaultLocalScale.Value;
		trans.DOPunchScale(addValue, duration, count, guanxing).SetLoops(-1).OnComplete(()=> trans.localScale = localScale);
    }
		
    /// <summary>
    /// icon成长
    /// </summary>
    /// <param name="trans">Trans.</param>
    /// <param name="target">目标位置.</param>
    public static void Growup(this Transform trans,Vector3 target)
    {
		DOTween.Kill(trans);
        trans.DOBlendableLocalMoveBy(target, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutQuint);
    }

    public static void GrowOnce(this Transform trans,Vector3 value,float startTime,float endTime,float delay=0)
    {
        var init = trans.localScale;
        trans.DOScale(value, startTime);
        trans.DOScale(init, endTime).SetDelay(startTime + delay);
    }

    //停止动画效果
    public static void StopDotween(this Transform trans)
    {
		DOTween.Kill(trans,true);
    }

    public static void RedTip(this Image image)
    {

    }

    static IEnumerator TT(Text text,float time)
    {
       
        while(time>0)
        {
            time -= Time.deltaTime;
            text.color = text.color.SetA(0.8f * time);
            yield return null;
        }
        GameObject.Destroy(text.gameObject);
    }



    //______________________________________________________________________UI相关

    /// <summary>
    /// 让图片大小去适应给定sizeDelta
    /// </summary>
    /// <param name="image">Image.</param>
    /// <param name="limitSize">给定范围.</param>
    public static void FitLimitSize(this Image image,Vector2 limitSize)
    {
        image.SetNativeSize();

        var scaleW = limitSize.x / image.rectTransform.sizeDelta.x;
        var scaleH = limitSize.y / image.rectTransform.sizeDelta.y;
        image.rectTransform.sizeDelta *= Mathf.Min(scaleW, scaleH);
    }

    /// <summary>
    /// 动态添加EventTrigger
    /// </summary>
    /// <param name="trans">套添加的组件.</param>
    /// <param name="type">类型.</param>
    /// <param name="call">注册方法.</param>
    public static void AddEventTrigger(this Transform trans,EventTriggerType type,UnityAction<BaseEventData> call)
    {
        var trigger = trans.gameObject.GetComponent<EventTrigger>() ?? trans.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener(call);
        trigger.triggers.Add(entry);

    }

    //______________________________________________________________其他
    /// <summary>
    /// 活动时间显示
    /// </summary>
    /// <returns>The remain time.</returns>
    /// <param name="startTime">Start time.</param>
    /// <param name="endTime">End time.</param>
    /// <param name="showCount">Show count.</param>
    public static string DealRemainTime(DateTime startTime,DateTime endTime,int showCount)
    {
        string str = "";
        int nowCount = 0;

        var span = endTime - startTime;
        Dictionary<string, int> dic = new Dictionary<string, int>
        {
            { "天", span.Days },
            { "小时", span.Hours },
            { "分", span.Minutes },
            { "秒", span.Seconds }
        };

        foreach (var key in dic.Keys)
        {
            if (dic[key] > 0)
            {
                str += dic[key] + key;
                nowCount++;
            }

            if (nowCount == showCount)
                break;
        }

        return str;
    }

    public static string DealRemainTime(TimeSpan span, int showCount)
    {
        string str = "";
        int nowCount = 0;
        Dictionary<string, int> dic = new Dictionary<string, int>
        {
            { "天", span.Days },
            { "时", span.Hours },
            { "分", span.Minutes },
            { "秒", span.Seconds }
        };

        foreach (var key in dic.Keys)
        {
            if (dic[key] > 0)
            {
                str += dic[key] + key;
                nowCount++;
            }

            if (nowCount == showCount)
                break;
        }

        return str;
    }
}


