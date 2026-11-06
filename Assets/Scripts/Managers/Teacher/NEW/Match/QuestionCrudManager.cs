using System;
using System.Collections.Generic;
using Models;
using UnityEngine;

public class QuestionCrudManager : MonoBehaviour
{
    public GameObject questionItemPrefab;
    public Transform container;
    public List<Question> SelectedQuestions = new();
    public void ListQuestions(List<Question> questions, List<Question> SelectedQestion)
    {

        SelectedQuestions = SelectedQestion;

        foreach (Question item in SelectedQestion)
        {
            GameObject instance = Instantiate(questionItemPrefab, container);
            instance.GetComponent<QuestionItemManager>().LoadData(item, true, this);
        }
        foreach (Question item in questions)
        {
            GameObject instance = Instantiate(questionItemPrefab, container);
            instance.GetComponent<QuestionItemManager>().LoadData(item, false, this);
        }

    }
    public void ClearQuestions()
    {
        for (int i = 0; i < container.childCount; i++)
        {
            Destroy(container.transform.GetChild(i).gameObject);
        }
        SelectedQuestions.Clear();
    }

}
