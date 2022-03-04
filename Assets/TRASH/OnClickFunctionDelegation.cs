using UnityEngine;

namespace UserInterface
{
    public class OnClickFunctionDelegation : MonoBehaviour
    {
        public void ExecuteFunctionality(GameObject clickResponseGameObject)
        {
            var clickResponse = clickResponseGameObject.GetComponent<IClickResponse>();
            clickResponse.ExecuteFunctionality();
        }
    }
}
