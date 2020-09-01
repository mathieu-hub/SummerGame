
using UnityEngine;
using Management;
using Player;
using Seller;
using GameCanvas;


namespace SuperManagement
{
    public class SuperGameManager : Singleton<SuperGameManager>
    {
     
        private void Awake()
        {
            MakeSingleton(true);
        }

        public void Reset()
        {
            Destroy(PlayerManager.Instance.gameObject);
            Destroy(SellerBehaviour.Instance.gameObject);
            Destroy(GameMaster.Instance.gameObject);
            Destroy(GameManager.Instance.gameObject);
            Destroy(GameCanvasManager.Instance.gameObject);


        }

    }

}
