using UnityEngine;

public class SingletonMonoBehaviour<SingletonType> : MonoBehaviour where SingletonType : MonoBehaviour
{
    private static SingletonType mInstance;

    public static SingletonType Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<SingletonType>();
            }
            return mInstance;
        }
    }
}