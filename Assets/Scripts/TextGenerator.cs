﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGenerator : MonoBehaviour
{
    public static TextGenerator instance;

    [SerializeField]
    private RectTransform worldCanvas;
    [SerializeField]
    private GameObject damageTextTemplate;

    private ObjectPooler op;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        op = ObjectPooler.instance;
    }

    public void CreateDamageText(string damage, Vector2 position)
    {
        GameObject txt = op.Create("DamageText", position, Quaternion.identity);
        txt.GetComponent<Text>().text = damage;

        float xPos = Random.Range(-0.5f, 0.5f);
        float yPos = Random.Range(-0.6f, -0.8f);

        Vector2 viewportPos = Camera.main.WorldToViewportPoint(position + new Vector2(xPos, yPos));
        Vector2 canvasPos = new Vector2(
        ((viewportPos.x * worldCanvas.sizeDelta.x) - (worldCanvas.sizeDelta.x * 0.5f)),
        ((viewportPos.y * worldCanvas.sizeDelta.y) - (worldCanvas.sizeDelta.y * 0.5f)));

        LeanTween.moveLocal(txt, canvasPos, 0.5f).setEaseOutExpo();
        LeanTween.alphaText(txt.GetComponent<RectTransform>(), 0, 0.7f).setEaseInExpo();

        StartCoroutine(ResetText(txt, 0.7f));
    }

    // After some time, resets opacity of text, then disables it
    private IEnumerator ResetText(GameObject text, float time)
    {
        yield return new WaitForSeconds(time);
        text.GetComponent<Text>().color = new Color(1, 1, 1, 1);
        text.SetActive(false);
    }
}
