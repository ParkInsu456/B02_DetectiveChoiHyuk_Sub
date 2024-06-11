using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechColor : MonoBehaviour
{
    // Mech의 색을 바꾸는 스크립트

    public MeshRenderer[] meshRenderers;

    public Color color1 = Color.white; // 첫 번째 색상
    public Color color2 = Color.red; // 두 번째 색상
    public float transitionDuration = 1.0f; // 색상 변환 시간

    public event Action onBlinkEvent;

    public IEnumerator coroutine;

    public bool isActive;

    private void Start()
    {       
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        coroutine = BlinkMaterial();
        // 디버그
        onBlinkEvent += StartBlinkColor;

    }

    private void Update()
    {
        if(isActive)
        CallBlinkEvent();
    }

    private IEnumerator BlinkMaterial()
    {
        while (true)
        {
            yield return StartCoroutine(TransitionColor(color1, color2, transitionDuration));
            yield return StartCoroutine(TransitionColor(color2, color1, transitionDuration));
        }
    }
    public void StartBlinkColor()
    {
        StartCoroutine(coroutine);
    }
    public void StopBlinkColor()
    {

        StopAllCoroutines();   // 코루틴 중지
        StartCoroutine(TransitionColor(color2, color1, transitionDuration * 2));
        isActive = false;
    }

    private IEnumerator TransitionColor(Color fromColor, Color toColor, float _transitionDuration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _transitionDuration)
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material.color = Color.Lerp(fromColor, toColor, elapsedTime / _transitionDuration);
                elapsedTime += Time.deltaTime;
                yield return null; // 다음 프레임까지 대기
            }
        }
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = toColor; // 정확한 도착 색상 설정
        }
    }      

    private void CallBlinkEvent()
    {
        onBlinkEvent?.Invoke();
    }

    public void ToggleBoolisActive()
    {
        isActive = !isActive;
    }
}
