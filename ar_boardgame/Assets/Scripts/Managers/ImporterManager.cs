
using System.IO;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.Events;
using UnityGLTF;
using UnityGLTF.Loader;

public class ImporterManager : MonoBehaviour
{
    public string GLTFUri = null;
    public bool Multithreaded = true;
    public bool AppendStreamingAssets = true;
    public bool PlayAnimationOnLoad = true;
    public ImporterFactory Factory = null;
    public UnityAction onLoadComplete;

#if UNITY_ANIMATION
        public IEnumerable<Animation> Animations { get; private set; }
#endif

    [SerializeField]
    private bool loadOnStart = true;

    [SerializeField] private int RetryCount = 10;
    [SerializeField] private float RetryTimeout = 2.0f;
    private int numRetries = 0;


    public int MaximumLod = 300;
    public int Timeout = 8;
    public GLTFSceneImporter.ColliderType Collider = GLTFSceneImporter.ColliderType.None;
    public GameObject LastLoadedScene { get; private set; } = null;

    [SerializeField]
    private Shader shaderOverride = null;



    // Start is called before the first frame update


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    async public void LoadModel()
    {
        var importOptions = new ImportOptions
        {
            AsyncCoroutineHelper = gameObject.GetComponent<AsyncCoroutineHelper>() ?? gameObject.AddComponent<AsyncCoroutineHelper>()
        };

        GLTFSceneImporter sceneImporter = null;
        try
        {
            // nose pára que es el factory
            Factory = Factory ?? ScriptableObject.CreateInstance<DefaultImporterFactory>();

            // define si es un uri local u online
            string fullPath;
            if (AppendStreamingAssets)
                fullPath = Path.Combine(Application.streamingAssetsPath, GLTFUri.TrimStart(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }));
            else
                fullPath = GLTFUri;

            string dir = URIHelper.GetDirectoryName(fullPath);
            // recupera el modelo en memoria
            importOptions.DataLoader = new UnityWebRequestLoader(dir);

            //establece el factory en la escena, que tiene las opciones de importacion y la nombre  del objeto
            sceneImporter = Factory.CreateSceneImporter(
                Path.GetFileName(fullPath),
                importOptions
            );

            sceneImporter.SceneParent = gameObject.transform;
            sceneImporter.Collider = Collider;
            sceneImporter.MaximumLod = MaximumLod;
            sceneImporter.Timeout = Timeout;
            sceneImporter.IsMultithreaded = Multithreaded;
            sceneImporter.CustomShaderName = shaderOverride ? shaderOverride.name : null;

            // for logging progress
            // carga el modelo
            await sceneImporter.LoadSceneAsync(
                onLoadComplete: LoadCompleteAction
            // ,progress: new Progress<ImportProgress>(
            // 	p =>
            // 	{
            // 		Debug.Log("Progress: " + p);
            // 	})
            );

            // Override the shaders on all materials if a shader is provided
            if (shaderOverride != null)
            {
                Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    renderer.sharedMaterial.shader = shaderOverride;
                }
            }

            LastLoadedScene = sceneImporter.LastLoadedScene;

#if UNITY_ANIMATION
				Animations = sceneImporter.LastLoadedScene.GetComponents<Animation>();

				if (PlayAnimationOnLoad && Animations.Any())
				{
					Animations.First().Play();
				}
#endif
        }
        finally
        {
            if (importOptions.DataLoader != null)
            {
                sceneImporter?.Dispose();
                sceneImporter = null;
                importOptions.DataLoader = null;
            }
        }
    }
    private void LoadCompleteAction(GameObject obj, ExceptionDispatchInfo exceptionDispatchInfo)
    {
        print("objeto cargado completamente");
        onLoadComplete?.Invoke();
    }
}
