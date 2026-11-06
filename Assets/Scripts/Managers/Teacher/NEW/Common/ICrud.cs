
namespace Models
{
    public interface ICrud
    {
        public void Save();
        public void PrepareModal(EditMode editMode, int code = -1);
        public void ClearModal();
        public Row[] List();

        //    public int GetSizeList();


    }
}