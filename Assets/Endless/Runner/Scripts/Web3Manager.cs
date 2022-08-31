using System.Linq;
// using Endless;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Web3Api.Models;
using UnityEngine.SceneManagement;
using MoralisUnity;
using UnityEngine.UI;
using UnityEngine;
using WalletConnectSharp.Unity;
using System;
using System.Collections;

namespace Endless.Runner
{
    public class StartSessionEvent : MoralisObject
    {
        public string result { get; set; }
        public StartSessionEvent() : base("StartSessionEvent") { }
    }
    public class Web3Manager : MonoBehaviour
    {

        // [SerializeField]
        // WalletConnect wallet;

        [SerializeField]
        Text addressText;

        [SerializeField]
        [Header("Web3 Interactive Buttons")]
        GameObject approvalButton;
        [SerializeField]
        GameObject startButton;
        [SerializeField]
        GameObject saveButton;

        [SerializeField]
        [Tooltip("The text is used to display debugging response information from the contract.")]
        [Header("Debugging Texts")]
        Text totalPotText;
        [SerializeField]
        [Tooltip("The text is used to display debugging response information from the contract.")]
        Text startPriceText;

        private bool listening;
        private bool isPressed;
        private int sceneIndex;

        // Only for Editor using

        private string editorResponseResult;
        private bool editorResponseReceived;


        void Awake()
        {
            InitMoralis();
        }

        void Start()
        {
            startButton.SetActive(false);
            saveButton.SetActive(false);
            exposedMethods();
        }

        private void Update()
        {
            // Only do this in Editor because of single threading and UI elements issues
            if (!Application.isEditor) return;

            if (editorResponseReceived)
            {
                StartCoroutine(DoSomething(true));
                editorResponseReceived = false;
            }
            // #if UNITY_ANDROID
            //             //For android
            //             if (isPressed)
            //             {
            //                 StartCoroutine(DelayWalletOpen(true));
            //                 isPressed = false;
            //             }
            // #endif
        }

        async void InitMoralis()
        {
            if (MoralisState.Initialized.Equals(Moralis.State))
            {
                MoralisUser player = await Moralis.GetUserAsync();

                if (player == null) SceneManager.LoadScene(0);

                //Display player's address
                if (addressText != null)
                    addressText.text = "Player ID: " + ShortHandAddress(player.ethAddress);
            }
        }

        void exposedMethods()
        {
            // getStartPrice();//✅
            getLeaderboard();//❌
            // getPrizes();//✅
            // getTotalPot();//✅
            // payStartCoin();//✅
            // paySaveSession(200);//❌

        }

        public async void getStartPrice()
        {
            try
            {
                SubscribeToDatabaseEvents();
                string price = await ContractFunctions.startGamePrice();

                if (price.Length != 0)
                {
                    approvalButton.SetActive(false);
                    startButton.SetActive(true);
                    //Enable save button after a game pause/over
                    Debug.Log("Your available coins " + price);
                }
                else
                {

                    Debug.Log("startGamePrice():: price = " + price);
                }
            }
            catch (Exception e)
            {
                //Show an error notification failed to approve token.
                Debug.Log("startGamePrice():: " + e.Message);
            }

            if (startPriceText != null)
                startPriceText.text = "Start Price: " + ContractFunctions.startGamePrice();
        }

        public async void getLeaderboard()
        {
            Debug.Log("leaderBoardData():: " + await ContractFunctions.leaderBoardData(1));
        }

        public async void getPrizes()
        {
            int prizeIndex = 1;
            Debug.Log("prizes():: " + await ContractFunctions.prizes(prizeIndex));
        }

        public async void getTotalPot()
        {
            Debug.Log("totalPot():: " + await ContractFunctions.totalPot());
            if (totalPotText != null)
                totalPotText.text = "Total Pot: " + ContractFunctions.totalPot();
        }

        public async void payStartCoin(int sceneBuildIndex)
        {
            sceneIndex = sceneBuildIndex;
            try
            {
                string results = await ContractFunctions.payToPlay();
                listening = true;

                if (results.Length != 0)
                {
                    Debug.Log("Your Transaction:: " + results);
                }
                else
                {

                    Debug.Log("startGamePrice():: price = " + results);
                }
            }
            catch (Exception e)
            {
                Debug.Log("payToPlay():: " + e.Message);
            }
        }

        public async void paySaveSession(int finalScore)
        {
            Debug.Log("addScore():: " + await ContractFunctions.addScore(finalScore));
            // Debug.Log("signMessage():: " + System.Text.Encoding.UTF8.GetString((await ContractFunctions.signMessageDebug(finalScore))).Length);
            // getPrizes();
        }

        string ShortHandAddress(string address)
        {
            string shortAddress = address;

            if (shortAddress.Length > 13)
            {
                shortAddress = string.Format("{0}...{1}", shortAddress.Substring(0, 6), shortAddress.Substring(shortAddress.Length - 4, 4));
            }

            return shortAddress;
        }

        public async void SubscribeToDatabaseEvents()
        {
            var getEventQuery = await Moralis.GetClient().Query<StartSessionEvent>();
            var queryCallbacks = new MoralisLiveQueryCallbacks<StartSessionEvent>();

            queryCallbacks.OnUpdateEvent += HandleContractEventResponse;
            MoralisLiveQueryController.AddSubscription<StartSessionEvent>("StartSessionEvent", getEventQuery, queryCallbacks);
        }

        private void HandleContractEventResponse(StartSessionEvent newEvent, int requrestId)
        {
            if (!listening) return;

            //INFO: For editor purpose
            if (Application.isEditor)
            {
                editorResponseResult = newEvent.result;
                editorResponseReceived = true;

                return;
            }

            if (newEvent.result != null)
            {
                listening = false;
                //Load the main scene
                StartCoroutine(DoSomething(true));
            }
        }

        private IEnumerator DoSomething(bool result)
        {
            yield return new WaitForSeconds(3f);

            if (result)
            {
                startButton.SetActive(false);
                //We could load another game scene here
                SceneManager.LoadScene(sceneIndex);

            }
            else
            {
                //Let player know opening another scene was not successful
            }
        }
        // private IEnumerator DelayWalletOpen(bool result)
        // {
        //     yield return new WaitForSeconds(10f);

        //     if (result)
        //     {
        //         // startButton.SetActive(false);
        //         //We could load another game scene here
        //         // SceneManager.LoadScene(sceneIndex);
        //         wallet.OpenDeepLink();
        //         wallet.OpenMobileWallet();

        //     }
        //     else
        //     {
        //         //Let player know opening another scene was not successful
        //     }
        // }

    }
}