using MoralisUnity.Kits.AuthenticationKit;
using UnityEngine;

namespace Endless.Runner
{
    public class AuthManager : MonoBehaviour
    {
        [SerializeField]
        GameObject authPrefab, welcomePrefab;
        AuthenticationKit authKit;

        void Start()
        {
            authKit = authPrefab.GetComponent<AuthenticationKit>();
        }

        public void OnAuthorized()
        {
            //Do something when user is allowed!
            welcomePrefab.SetActive(true);
        }

        public void Logout()
        {
            //Logout player
            authKit.Disconnect();
            welcomePrefab.SetActive(false);
        }
    }
}