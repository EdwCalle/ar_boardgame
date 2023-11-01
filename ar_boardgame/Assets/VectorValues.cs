
using UnityEngine;
using TMPro;
public class VectorValues : MonoBehaviour
{
    public GameObject x;
    public GameObject y;
    public GameObject z;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public Vector3 getVectorValue()
    {
        //controlar errores
        float a = float.Parse(x.GetComponent<TMP_InputField>().text);
        float b = float.Parse(y.GetComponent<TMP_InputField>().text);
        float c = float.Parse(z.GetComponent<TMP_InputField>().text);
        return new Vector3(a, b, c);

    }
}
