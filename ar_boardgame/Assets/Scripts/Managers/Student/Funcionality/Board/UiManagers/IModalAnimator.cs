using System.Threading.Tasks;
using UnityEngine;

public interface IModalAnimator
{
    //public v
    public Task ShowModal(GameObject canvasModal);
    public Task HideModal(GameObject canvasModal);
}