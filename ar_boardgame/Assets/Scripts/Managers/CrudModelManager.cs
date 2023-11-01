using Models;
using UnityEngine;

public class CrudModelManager : MonoBehaviour
{
    private Item currentModel = new Item();

    public VectorValues hitboxScale;
    public VectorValues hitboxPosition;
    public VectorValues modelScale;
    public VectorValues modelPosition;

    public GameObject model;

    public GameObject resume;

    public GameObject hitbox;
    public GameObject box;




    // Start is called before the first frame update
    void Start()
    {
        currentModel.Collider = new Models.Collider(new Vector3(0, 0, 0), new Vector3(0.2f, 0.2f, 0.2f));
        currentModel.Position = new Vector3(0, 0, 0);
        currentModel.Scale = new Vector3(0.2f, 0.2f, 0.2f);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateColliderSize()
    {


        float x = (hitboxScale.getVectorValue().x / 2) + 0.1f; //(0.2-0,0.117/2)=0.0585-0.2=0.1415-0.1=0.0415+0.0585=1;
        resume.transform.position = new Vector3(x, resume.transform.position.y, resume.transform.position.z);
        // collider size
        currentModel.Collider.Size = hitboxScale.getVectorValue();
        box.transform.localScale = currentModel.Collider.Size;
        // change pos postion accordting to x size


    }
    public void UpdateColliderCenter()
    {
        // hitbox position
        currentModel.Collider.Center = hitboxPosition.getVectorValue();
        hitbox.transform.position = currentModel.Collider.Center;
    }
    public void UpdateModelPosition()
    {
        // model position
        currentModel.Position = modelPosition.getVectorValue();
        model.transform.position = currentModel.Position;
    }
    public void UpdateModelScale()
    {
        // model position
        currentModel.Scale = modelScale.getVectorValue();
        model.transform.localScale = currentModel.Scale;
    }

}
