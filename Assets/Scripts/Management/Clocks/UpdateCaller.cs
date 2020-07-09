using UnityEngine;

namespace NCO.Clocks
{
    public class UpdateCaller : MonoBehaviour
    {
        public static UpdateCaller Instance = null;

        //Event used to run the clocks (NCO_Clocks package)
        public delegate void UpdateCall();
        public static event UpdateCall OnUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}