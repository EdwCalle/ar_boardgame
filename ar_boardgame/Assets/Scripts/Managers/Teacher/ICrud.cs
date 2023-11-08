using UnityEngine;

namespace Models
{
    public interface ICrud
    {
        public void Save()
        {

        }
        public void PrepareModal(EditMode editMode, int code);
        public void ClearModal();
        void List(GameObject table, GameObject row);

        //    public int GetSizeList();


    }
}