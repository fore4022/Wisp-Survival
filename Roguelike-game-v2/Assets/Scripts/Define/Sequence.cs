using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 하나의 객체가 가지는 Tween 정보를 저장 및 관리하는 타입
/// </summary>
public class Sequence
{
    private Queue<List<TweenData>> _tweenQueue = new();

    public List<TweenData>[] Values()
    {
        return _tweenQueue.ToArray();
    }
    public List<TweenData> PeekLast()
    {
        return _tweenQueue.ToArray()[_tweenQueue.Count - 1];
    }
    public List<TweenData> Peek()
    {
        if(_tweenQueue.Count == 0)
        {
            _tweenQueue.Enqueue(new());
        }

        return _tweenQueue.Peek();
    }
    public int Count()
    {
        return _tweenQueue.Count;
    }
    public void Enqueue(List<TweenData> list)
    {
        _tweenQueue.Enqueue(list);
    }
    public void Dequeue(Transform transform, TweenData data)
    {
        _tweenQueue.Peek().Remove(data);

        if(_tweenQueue.Peek().Count == 0)
        {
            _tweenQueue.Dequeue();

            if(_tweenQueue.Count == 0)
            {
                Tween_Manage.Clear(transform);
            }
            else
            {
                foreach(TweenData _data in _tweenQueue.Peek())
                {
                    _data.Set(CoroutineHelper.Start(Tweening.OverTime(_data.type, _data, _data.trans, _data.easeDel, _data.targetValue, _data.duration), CoroutineType.Tween));
                }
            }
        }
    }
}