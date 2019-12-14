using UnityEngine;

public interface IUnityService
{
    float GetDeltaTime();
    bool GetKeyDown(KeyCode code);
}

public class UnityService : IUnityService
{
    public float GetDeltaTime()
    {
        return Time.deltaTime;
    }

    public bool GetKeyDown(KeyCode code)
    {
        return Input.GetKeyDown(code);
    }
}
