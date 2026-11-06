using Models;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrudModelManager : MonoBehaviour
{
    private Model3D currentModel = new();

    public VectorValues hitboxScale;
    public VectorValues hitboxPosition;
    public VectorValues modelScale;
    public VectorValues modelPosition;

    public GameObject model;
    public GameObject modelGLTF;

    public GameObject resume;

    //public GameObject hitbox;
    public GameObject colliderBox;

    public TextMeshPro[] title;
    public TextMeshPro[] description;
    public TextMeshPro[] copyright;


    // Start is called before the first frame update
    void Start()
    {
        currentModel.Collider = new Models.Collider(new Vector3(0, 1.5f, 0), new Vector3(0.2f, 1f, 0.2f));
        currentModel.Position = new Vector3(0, 1.5f, 0);
        currentModel.Scale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    public void UpdateColliderSize()
    {
        float x = (hitboxScale.getVectorValue().x / 2) + 0.1f; //(0.2-0,0.117/2)=0.0585-0.2=0.1415-0.1=0.0415+0.0585=1;
        resume.transform.position = new Vector3(x, resume.transform.position.y, resume.transform.position.z);
        // collider size
        currentModel.Collider.Size = hitboxScale.getVectorValue();
        colliderBox.transform.localScale = currentModel.Collider.Size;
        // change pos postion accordting to x size
    }
    // public void UpdateColliderCenter()
    // {
    //     // hitbox position
    //     currentModel.Collider.Center = hitboxPosition.getVectorValue();
    //     //  hitbox.transform.position = currentModel.Collider.Center;
    // }
    public void UpdateModelPosition()
    {
        // model position
        currentModel.Position = modelPosition.getVectorValue();
        // model.transform.position = currentModel.Position;
        modelGLTF.transform.position = currentModel.Position;
    }
    public void UpdateModelScale()
    {
        // model position
        currentModel.Scale = modelScale.getVectorValue();
        //  model.transform.localScale = currentModel.Scale;
        modelGLTF.transform.localScale = currentModel.Scale;
    }
    public void SaveModel()
    {
        SceneManager.LoadScene("mainPageTeacher");
    }
    // public void ResetRotation(GameObject gameObject)
    // {
    //     // gameObject.transform.Rotate(0f, 0f, 0f);
    //     gameObject.transform.rotation = Quaternion.identity;
    // }
    public void UpdateTittle(TMP_InputField input)
    {
        title[0].text = input.text;
        title[1].text = input.text;
    }
    public void UpdateDescription(TMP_InputField input)
    {
        description[0].text = input.text;
        description[1].text = input.text;
    }
    public void UpdateCopyright(TMP_InputField input)
    {
        copyright[0].text = input.text;
        copyright[1].text = input.text;
    }


}
