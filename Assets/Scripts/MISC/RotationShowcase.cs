using UnityEngine;

namespace MISC
{
    public class RotationShowcase : MonoBehaviour
    {
        public float x, y, z;
        private void Update()
        {
            gameObject.transform.Rotate(new Vector3(x, y, z) * Time.deltaTime);
        }
    }
}
