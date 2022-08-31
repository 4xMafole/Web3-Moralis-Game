using System.Text;
using System;
using Cysharp.Threading.Tasks;
using MoralisUnity.Web3Api.Models;
using MoralisUnity;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI;
using Nethereum.Hex.HexConvertors.Extensions;
using WalletConnectSharp.Unity;
using MoralisUnity.Platform.Objects;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Endless.Runner
{
    public class ContractFunctions
    {
        //✅startGamePrice
        public static async UniTask<string> startGamePrice()
        {
            //Define function name
            string functionName = LeaderBoardContractData.START_GAME_PRICE_CALL;

            //Define contract request
            object[] parameters = null;

            string results = await OnRunContractFunction(functionName, parameters);
            return results;
        }

        //❌Leaderboard
        public static async UniTask<string> leaderBoardData(int i)
        {
            //Define function name
            string functionName = LeaderBoardContractData.LEADERBOARD_CALL;
            //Define contract request
            //Tried this:
            // object[] parameters = { i.ToString()};
            // 
            //Also this:
            // object[] parameters = new object[1];
            // parameters[0] = i;
            // 
            //Then this:
            // object[] parameters = { "5" };

            string results = await OnRunContractFunctionLeaderBoard(functionName, i);
            return results;
        }

        //✅prizes
        public static async UniTask<string> prizes(int index)
        {
            if (index >= 0 && index <= 5)
            {

                //The function call should return 6 elements array
                string functionName = LeaderBoardContractData.PRIZES_CALL;

                string results = await OnRunContractFunctionPrizes(functionName, index);
                return results;
            }
            else
            {
                throw new Exception("The prizes' index should start from 0 to 5");
            }
        }

        //✅totalPot
        public static async UniTask<string> totalPot()
        {
            //Define function name
            string functionName = LeaderBoardContractData.TOTAL_POT_CALL;
            //Define contract request
            object[] parameters = null;

            string results = await OnRunContractFunction(functionName, parameters);
            return results;
        }

        //✅payToPlay
        public static async UniTask<string> payToPlay()
        {

            //Define function name
            string functionName = LeaderBoardContractData.PAY_TO_PLAY_TRANSACTION;
            //Define contract request
            object[] parameters = { };

            string results = await OnExecuteContractFunction(functionName, parameters);
            return results;
        }

        //❌addScore
        public static async UniTask<string> addScore(int finalScore)
        {
            try
            {
                MoralisUser player = await Moralis.GetUserAsync();
                string functionName = LeaderBoardContractData.PAY_SAVE_SESSION_TRANSACTION;
                int nonce = await getCurrentTxNonce(player.ethAddress);
                //BUG:: invalid signature which reverts the transaction in the blockchain.
                var signature = await signMessage1(player.ethAddress, nonce, finalScore);

                //Define contract request
                object[] parameters = { finalScore, nonce, signature };

                string results = await OnSendTransaction(functionName, parameters);

                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Applied the solution suggested by the Developer of Nethereum but signature is not accepted within the smart contract.
        /// </summary>
        /// <param name="ethAddress"></param>
        /// <param name="nonce"></param>
        /// <param name="finalScore"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static Task<byte[]> signMessage(string ethAddress, int nonce, int finalScore)
        {
            try
            {


                Nethereum.Signer.EthereumMessageSigner signer = new Nethereum.Signer.EthereumMessageSigner();

                string SIGNER_KEY = "255bc30c6e5df430f8d18108846c9b77634aeed51216329fc42b1e61b188abf8";

                string message = ethAddress + nonce + finalScore;

                var abiEncode = new Nethereum.ABI.ABIEncode();
                var result = abiEncode.GetSha3ABIEncoded(ethAddress,
                                nonce, finalScore);

                var signature = signer.Sign(result, new Nethereum.Signer.EthECKey(SIGNER_KEY));



                return Task.FromResult(Encoding.UTF8.GetBytes(signature));
            }
            catch (Exception e)
            {

                // await WalletConnect.ActiveSession.Disconnect();
                throw new Exception(e.Message);
            }
        }
       
        /// <summary>
        /// Applied the solution suggested by the Developer of Nethereum but signature is not accepted within the smart contract.
        /// </summary>
        /// <param name="ethAddress"></param>
        /// <param name="nonceNew"></param>
        /// <param name="finalScore"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static Task<byte[]> signMessage1(string ethAddress, int nonceNew, int finalScore)
        {
            try
            {


                Nethereum.Signer.EthereumMessageSigner signer = new Nethereum.Signer.EthereumMessageSigner();

                string SIGNER_KEY = "255bc30c6e5df430f8d18108846c9b77634aeed51216329fc42b1e61b188abf8";

                string message = ethAddress + nonceNew + finalScore;


                Nethereum.ABI.Encoders.AddressTypeEncoder abiAddrEnc = new Nethereum.ABI.Encoders.AddressTypeEncoder();
                Nethereum.ABI.Encoders.IntTypeEncoder abiUintEnc = new Nethereum.ABI.Encoders.IntTypeEncoder();

                byte[] addr = abiAddrEnc.Encode(ethAddress);
                byte[] nonce = abiUintEnc.Encode(nonceNew);
                byte[] score = abiUintEnc.Encode(finalScore);

                var sha3 = new Nethereum.Util.Sha3Keccack();
                var hash = "0x" + sha3.CalculateHash(addr).ToHex() + sha3.CalculateHash(nonce).ToHex() + sha3.CalculateHash(score).ToHex();

                var abiEncode = new Nethereum.ABI.ABIEncode();
                var result = abiEncode.GetSha3ABIEncoded(ethAddress,
                                nonce, finalScore);

                var signature = signer.Sign(result, new Nethereum.Signer.EthECKey(SIGNER_KEY));



                return Task.FromResult(Encoding.UTF8.GetBytes(hash.ToHexUTF8()));
            }
            catch (Exception e)
            {

                // await WalletConnect.ActiveSession.Disconnect();
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Using Wallet Connect to sign a message but the signature is not accepted within the smart contract.
        /// </summary>
        /// <param name="finalScore"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<byte[]> signMessageDebug(int finalScore)
        {
            try
            {
                MoralisUser player = await Moralis.GetUserAsync();

                int nonce = await getCurrentTxNonce(player.ethAddress);

                string message = "" + player.ethAddress + nonce + finalScore + "";
                string signature = await WalletConnect.ActiveSession.EthPersonalSign(player.ethAddress, message);



                return Encoding.UTF8.GetBytes(signature);
            }
            catch (Exception e)
            {

                // await WalletConnect.ActiveSession.Disconnect();
                throw new Exception(e.Message);
            }
        }

        static async Task<int> getCurrentTxNonce(string ethAddress)
        {
            TransactionCollection transactions = await Moralis.Web3Api.Account.GetTransactions(ethAddress.ToLower(), LeaderBoardContractData.requiredChain);

            Root transCollection = JsonConvert.DeserializeObject<Root>(transactions.ToJson());


            return int.Parse(transCollection.result[0].nonce);
        }

        //executeContract
        static async UniTask<string> OnExecuteContractFunction(string functionName, object[] args)
        {
            //Defining contract data
            string address = LeaderBoardContractData.Address;
            string abi = LeaderBoardContractData.Abi;

            //Ensure WalletConnect
            if (WalletConnect.Instance == null)
            {
                throw new Exception("Method failed. WalletConnect.Instance must not be null. Add the WalletConnect.prefab to your scene.");
            }

            //Setup Web3
            await Moralis.SetupWeb3();

            //Estimate the gas
            HexBigInteger value = new HexBigInteger(0);
            HexBigInteger gas = new HexBigInteger(0);
            HexBigInteger gasPrice = new HexBigInteger(0);
            try
            {
                string result = await Moralis.ExecuteContractFunction(address, abi, functionName, args, value, gas, gasPrice);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        static async UniTask<string> OnSendTransaction(string functionName, object[] args)
        {
            //Defining contract data
            string address = LeaderBoardContractData.Address;
            string abi = "[{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"nonce\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"signature\",\"type\":\"bytes\"}],\"name\":\"addScore\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"status\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";

            //Ensure WalletConnect
            if (WalletConnect.Instance == null)
            {
                throw new Exception("Method failed. WalletConnect.Instance must not be null. Add the WalletConnect.prefab to your scene.");
            }

            //Setup Web3
            await Moralis.SetupWeb3();

            //Estimate the gas
            HexBigInteger value = new HexBigInteger("0x0");
            //FIXME: Without gas Wallet i.e Meta mask returns an error JSON-RPC
            HexBigInteger gas = new HexBigInteger("800000");
            HexBigInteger gasPrice = new HexBigInteger("0");
            try
            {

                string result = await Moralis.ExecuteContractFunction(address, abi, functionName, args, value, gas, gasPrice);


                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }


        static async UniTask<string> OnRunContractFunction(string functionName, object[] args)
        {
            //Defining contract data
            string address = LeaderBoardContractData.Address;
            ChainList chain = LeaderBoardContractData.requiredChain;
            object[] abi = LeaderBoardContractData.GetAbiObject();

            //Ensure WalletConnect
            if (WalletConnect.Instance == null)
            {
                throw new Exception("Method failed. WalletConnect.Instance must not be null. Add the WalletConnect.prefab to your scene.");
            }

            //Setup Web3
            await Moralis.SetupWeb3();

            RunContractDto runContractDto = new RunContractDto()
            {
                Abi = abi,
                Params = args
            };

            MoralisClient moralisClient = Moralis.GetClient();

            string result = await moralisClient.Web3Api.Native.RunContractFunction<String>(address, functionName, runContractDto, chain);

            return result;

        }

        //!Special for LeaderBoard
        static async UniTask<string> OnRunContractFunctionLeaderBoard(string functionName, int index)
        {
            //Defining contract data
            string address = LeaderBoardContractData.Address;
            ChainList chain = LeaderBoardContractData.requiredChain;
            object[] abi = new object[1];

            //readStructFromMapping
            object[] readStructFromMappingInputParams = new object[1];
            readStructFromMappingInputParams[0] = new { internalType = "uint8", name = "position", type = "uint8" };

            object[] readStructFromMappingOutputParams = new object[2];
            readStructFromMappingOutputParams[0] = new { internalType = "address", name = "refUser", type = "address" };
            readStructFromMappingOutputParams[1] = new { internalType = "uint256", name = "refScore", type = "uint256" };

            abi[0] = new { inputs = readStructFromMappingInputParams, outputs = readStructFromMappingOutputParams, name = LeaderBoardContractData.LEADERBOARD_CALL, stateMutability = "view", type = "function" };


            RunContractDto runContractDto = new RunContractDto()
            {
                Abi = abi,
                Params = new { position = index }
            };

            MoralisClient moralisClient = Moralis.GetClient();

            string result = await moralisClient.Web3Api.Native.RunContractFunction<String>(address, functionName, runContractDto, chain);

            return result;

        }

        //!Special for prizes
        static async UniTask<string> OnRunContractFunctionPrizes(string functionName, int index)
        {
            //Defining contract data
            string address = LeaderBoardContractData.Address;
            ChainList chain = LeaderBoardContractData.requiredChain;
            object[] abi = new object[1];

            //prizes
            object[] getPrizesInputParams = new object[1];
            getPrizesInputParams[0] = new { internalType = "uint256", name = "index", type = "uint256" };
            object[] getPrizesOutputParams = new object[1];
            getPrizesOutputParams[0] = new { internalType = "uint8", name = "prize", type = "uint8" };

            abi[0] = new { inputs = getPrizesInputParams, outputs = getPrizesOutputParams, name = LeaderBoardContractData.PRIZES_CALL, stateMutability = "view", type = "function" };


            // //Ensure WalletConnect
            // if (WalletConnect.Instance == null)
            // {
            //     throw new Exception("Method failed. WalletConnect.Instance must not be null. Add the WalletConnect.prefab to your scene.");
            // }

            // //Setup Web3
            // await Moralis.SetupWeb3();

            RunContractDto runContractDto = new RunContractDto()
            {
                Abi = abi,
                Params = new { index = index }
            };

            MoralisClient moralisClient = Moralis.GetClient();

            string result = await moralisClient.Web3Api.Native.RunContractFunction<String>(address, functionName, runContractDto, chain);

            return result;

        }

    }
}