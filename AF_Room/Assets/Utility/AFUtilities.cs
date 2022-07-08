using UnityEngine;

public class AFUtilities
{
    // Useful Utilities
    // place one game object in the same position and rotation of another
    public static void alignTransforms(GameObject src, GameObject dst)
    {
        alignTransforms(src.transform, dst.transform);
    }
    public static void alignTransforms(Transform src, Transform dst)
    {
        dst.SetPositionAndRotation(src.position, src.rotation);
    }
}
