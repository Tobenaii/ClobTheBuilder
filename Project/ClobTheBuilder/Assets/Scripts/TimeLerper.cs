using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLerper
{
    private float m_timer;
    private float m_newUpdateCheck;

    public void Reset()
    {
        m_timer = 0;
    }

    private float GetPercent(float timeToFinish)
    {
        //Get the percent of the finish time with the current time
        if (m_newUpdateCheck != Time.time)
            m_timer += Time.deltaTime;
        m_newUpdateCheck = Time.time;
        return m_timer / timeToFinish;
    }

    public void AddTime(float time)
    {
        m_timer += time;
    }

    //Everytime this is called, the Lerp will get a percentage closer towards the end
    public Vector3 Lerp(Vector3 init, Vector3 end, float timeToFinish)
    {
        float lerpPercent = GetPercent(timeToFinish);
        return Vector3.Lerp(init, end, lerpPercent);
    }

    public float Lerp(float init, float end, float timeToFinish)
    {
        float lerpPercent = GetPercent(timeToFinish);
        return Mathf.Lerp(init, end, lerpPercent);
    }

    public void Lerp(Material init, Material end, float timeToFinish, ref Renderer rend)
    {
        float lerpPercent = GetPercent(timeToFinish);
        rend.material.Lerp(init, end, lerpPercent);
    }
}