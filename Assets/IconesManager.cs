using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Player;
using Seller;

public class IconesManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject wallet;
    public GameObject walletSprite;
    public GameObject warning;
    public GameObject warningSprite;

    public Vector3 rotationVectorWallet = Vector3.zero;
   public Quaternion orientationQuaternionWallet;

    public Vector3 rotationVectorWarning = Vector3.zero;
    public Quaternion orientationQuaternionWarning;

    void Start()
    {
        warning.SetActive(false);
        wallet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPods || GameManager.Instance.underAttack)
        {
            warning.SetActive(true);
        }
        else
        {
            warning.SetActive(false);
        }


        if (GameManager.Instance.isMarchand)
        {
            wallet.SetActive(true);
        }
        else
        {
            wallet.SetActive(false);
        }


        //Wallet
        float WalletVertical = PlayerManager.Instance.transform.position.y - GameManager.Instance.Seller.transform.position.y;
        float Wallethorizontal = PlayerManager.Instance.transform.position.x - GameManager.Instance.Seller.transform.position.x;

        rotationVectorWallet = new Vector3(0, 0, Mathf.Atan2(WalletVertical, Wallethorizontal) * 180 / Mathf.PI);
        //Obligé d'utiliser les Quaternions pour les rotations
        orientationQuaternionWallet = Quaternion.Euler(rotationVectorWallet);
        //Oriente le sprite selon la position définie.
        wallet.transform.rotation = orientationQuaternionWallet;

        walletSprite.transform.rotation = Quaternion.Inverse(wallet.transform.rotation);

        //Warning

        if (GameManager.Instance.isPods && GameManager.Instance.underAttack == false)
        {
            float WarningVertical = PlayerManager.Instance.transform.position.y - GameManager.Instance.podsPositions.transform.position.y;
            float Warninghorizontal = PlayerManager.Instance.transform.position.x - GameManager.Instance.podsPositions.transform.position.x;

            rotationVectorWarning = new Vector3(0, 0, Mathf.Atan2(WarningVertical, Warninghorizontal) * 180 / Mathf.PI);
            //Obligé d'utiliser les Quaternions pour les rotations
            orientationQuaternionWarning = Quaternion.Euler(rotationVectorWarning);
            //Oriente le sprite selon la position définie.
            warning.transform.rotation = orientationQuaternionWarning;

        }

        else
        {
            float WarningVertical = PlayerManager.Instance.transform.position.y - GameManager.Instance.siloPoint.transform.position.y;
            float Warninghorizontal = PlayerManager.Instance.transform.position.x - GameManager.Instance.siloPoint.transform.position.x;

            rotationVectorWarning = new Vector3(0, 0, Mathf.Atan2(WarningVertical, Warninghorizontal) * 180 / Mathf.PI);
            //Obligé d'utiliser les Quaternions pour les rotations
            orientationQuaternionWarning = Quaternion.Euler(rotationVectorWarning);
            //Oriente le sprite selon la position définie.
            warning.transform.rotation = orientationQuaternionWarning;
        }

        
        
    }
}
