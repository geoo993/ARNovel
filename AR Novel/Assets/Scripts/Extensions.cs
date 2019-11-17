using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


//It is common to create a class to contain all of your
//extension methods. This class must be static.
public static class Extensions
{
    //Even though they are used like normal methods, extension
    //methods must be declared static. Notice that the first
    //parameter has the 'this' keyword followed by a Transform
    //variable. This variable denotes which class the extension
    //method becomes a part of.
    public static void ResetTransformation(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }

    public static void StringToEnum<T>(this string value, out T output) where T : struct
    {
        output = (T) Enum.Parse(typeof(T), value, true);
    }
    
    public static Color RandomColor() {
        return new Color (UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1.0f);
    }

    public static void MaterialColorToRandom( this Material MaterialToChange)
    {
        MaterialToChange.color = new Color (UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1.0f);
    }
    
    public static float PercentageBetween(float current, float minValue, float maxValue) {
        float difference = (minValue < 0) ? maxValue : maxValue - minValue;
        return 100.0f * ((current - minValue) / difference);
    }
   
    public static float ValueUsingPercentage(float percentage, float minValue, float maxValue) {
        float max = (maxValue > minValue) ? maxValue : minValue;
        float min = (maxValue > minValue) ? minValue : maxValue;
        return (((max - min) * percentage) / 100.0f) + min;
    }
    
    public static void Shuffle(List<int> arr)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int i = 0; i < arr.Count; i++ )
        {
            int r = UnityEngine.Random.Range(i, arr.Count);
            int tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }
    
    public static void Shuffle<T>(T[] arr) {
        for (int i = arr.Length - 1; i > 0; i--) {
            int r = UnityEngine.Random.Range(0, i);
            T tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }

    public static void SetDefaultScale(this RectTransform trans) {
        trans.localScale = new Vector3(1, 1, 1);
    }
    
    public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec) {
        trans.pivot = aVec;
        trans.anchorMin = aVec;
        trans.anchorMax = aVec;
    }

    public static Vector2 GetSize(this RectTransform trans) {
        return trans.rect.size;
    }
    public static float GetWidth(this RectTransform trans) {
        return trans.rect.width;
    }
    public static float GetHeight(this RectTransform trans) {
        return trans.rect.height;
    }

    public static void SetPositionOfPivot(this RectTransform trans, Vector2 newPos) {
        trans.localPosition = new Vector3(newPos.x, newPos.y, trans.localPosition.z);
    }

    public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos) {
        trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
    }
    public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos) {
        trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
    }
    public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos) {
        trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
    }
    public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos) {
        trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
    }

    public static void SetSize(this RectTransform trans, Vector2 newSize) {
        Vector2 oldSize = trans.rect.size;
        Vector2 deltaSize = newSize - oldSize;
        trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
        trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
    }
    public static void SetWidth(this RectTransform trans, float newSize) {
        SetSize(trans, new Vector2(newSize, trans.rect.size.y));
    }
    public static void SetHeight(this RectTransform trans, float newSize) {
        SetSize(trans, new Vector2(trans.rect.size.x, newSize));
    }
    
    public static void SetRect(RectTransform trans, float left, float top, float right, float bottom)
    {
        trans.offsetMin = new Vector2(left, bottom);
        trans.offsetMax = new Vector2(-right, -top);
    }
}
