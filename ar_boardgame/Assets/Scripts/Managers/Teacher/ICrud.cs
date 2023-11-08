using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public interface ICrud
    {
        public void Save()
        {

        }
        void List(GameObject table, GameObject row);

        //    public int GetSizeList();


    }
}