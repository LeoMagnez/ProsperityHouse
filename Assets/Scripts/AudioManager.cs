using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource coins;
    public AudioSource selection;
    public AudioSource notification;
    public AudioSource upgradeSound;
    // Start is called before the first frame update
    public void CoinSound()
    {
        coins.Play();
    }

    public void SelectionSound()
    {
        selection.Play();
    }

    public void NotificationSound()
    {
        notification.Play();
    }

    public void UpgradeSound()
    {
        upgradeSound.Play();
    }
}
