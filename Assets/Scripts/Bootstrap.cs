
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(
        RuntimeInitializeLoadType.AfterSceneLoad
    )]
    static void Initialize()
    {
        if (FindObjectOfType<Bootstrap>() != null)
            return;

        new GameObject("Bootstrap")
            .AddComponent<Bootstrap>();
    }

    void Awake()
    {
        if (FindObjectOfType<GameManager>() == null)
            new GameObject("GameManager")
                .AddComponent<GameManager>();

        CreatePlayer();
        CreateCamera();
        CreateEnvironment();
    }

    void CreatePlayer()
    {
        GameObject player =
            GameObject.CreatePrimitive(
                PrimitiveType.Capsule
            );

        player.name = "Player";
        player.tag = "Player";
        player.transform.position =
            new Vector3(0, 1, 2);

        Destroy(player.GetComponent<CapsuleCollider>());

        CharacterController controller =
            player.AddComponent<CharacterController>();

        controller.height = 1.8f;
        controller.radius = 0.38f;
        controller.center =
            new Vector3(0, 0.9f, 0);

        player.AddComponent<PlayerController>();
    }

    void CreateCamera()
    {
        GameObject cameraObject =
            new GameObject("Main Camera");

        cameraObject.tag = "MainCamera";

        cameraObject.AddComponent<Camera>();
        cameraObject.AddComponent<AudioListener>();

        CameraFollow follow =
            cameraObject.AddComponent<CameraFollow>();

        GameObject player =
            GameObject.FindWithTag("Player");

        follow.target = player.transform;

        cameraObject.transform.position =
            player.transform.position +
            new Vector3(0, 5, -7);
    }

    void CreateEnvironment()
    {
        GameObject lightObject =
            new GameObject("Directional Light");

        Light light =
            lightObject.AddComponent<Light>();

        light.type = LightType.Directional;
        light.intensity = 1.2f;

        lightObject.transform.rotation =
            Quaternion.Euler(50, -30, 0);

        GameObject player =
            GameObject.FindWithTag("Player");

        CourseGenerator generator =
            new GameObject("CourseGenerator")
                .AddComponent<CourseGenerator>();

        generator.player = player.transform;
    }
}
