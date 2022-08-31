using MoralisUnity.Web3Api.Models;

namespace Endless.Runner
{
    public static class LeaderBoardContractData
    {
        public const ChainList requiredChain = ChainList.bsc_testnet;
        //SC CALLS (Getters - No need to send wallet tx and pay gas fee)
        public const string START_GAME_PRICE_CALL = "startGamePrice";
        public const string LEADERBOARD_CALL = "readStructFromMapping";
        public const string PRIZES_CALL = "getPrizes";
        public const string TOTAL_POT_CALL = "getCoinsBalance";

        //SC TRANSACTIONS (Setters - Send wallet transactions and pay gas fee)
        public const string PAY_TO_PLAY_TRANSACTION = "payToPlay";
        public const string PAY_SAVE_SESSION_TRANSACTION = "addScore";

        public const string Address = "0xA9E7a2de75EE743e2c2981E599d0C07dC917AD68";
        public const string Abi = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"_gameAddress\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"_reflectionKeeper\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"_coinInstance\",\"type\":\"address\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"coin\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint8\",\"name\":\"decimals\",\"type\":\"uint8\"}],\"name\":\"CoinInstanceUpdate\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"components\":[{\"internalType\":\"address\",\"name\":\"user\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"indexed\":false,\"internalType\":\"struct MavatrixLeaderboard.User\",\"name\":\"leader1\",\"type\":\"tuple\"},{\"components\":[{\"internalType\":\"address\",\"name\":\"user\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"indexed\":false,\"internalType\":\"struct MavatrixLeaderboard.User\",\"name\":\"leader2\",\"type\":\"tuple\"},{\"components\":[{\"internalType\":\"address\",\"name\":\"user\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"indexed\":false,\"internalType\":\"struct MavatrixLeaderboard.User\",\"name\":\"leader3\",\"type\":\"tuple\"},{\"components\":[{\"internalType\":\"address\",\"name\":\"user\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"indexed\":false,\"internalType\":\"struct MavatrixLeaderboard.User\",\"name\":\"leader4\",\"type\":\"tuple\"},{\"components\":[{\"internalType\":\"address\",\"name\":\"user\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"indexed\":false,\"internalType\":\"struct MavatrixLeaderboard.User\",\"name\":\"leader5\",\"type\":\"tuple\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"keeper\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"currentOrder\",\"type\":\"uint256\"}],\"name\":\"PayoutAndReset\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"string\",\"name\":\"kindof\",\"type\":\"string\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"price\",\"type\":\"uint256\"}],\"name\":\"PriceUpdate\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[],\"name\":\"PrizesUpdate\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"signer\",\"type\":\"address\"}],\"name\":\"SignerUpdate\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"user\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"price\",\"type\":\"uint256\"}],\"name\":\"StartSession\",\"type\":\"event\"},{\"stateMutability\":\"nonpayable\",\"type\":\"fallback\"},{\"inputs\":[],\"name\":\"coinInstance\",\"outputs\":[{\"internalType\":\"contract IERC20\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"prizes\",\"outputs\":[{\"internalType\":\"uint8\",\"name\":\"\",\"type\":\"uint8\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"reflectionKeeper\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"renounceOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"startGamePrice\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"stateMutability\":\"payable\",\"type\":\"receive\"},{\"inputs\":[],\"name\":\"payToPlay\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"new_price\",\"type\":\"uint256\"}],\"name\":\"updateStartPrice\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"new_signer\",\"type\":\"address\"}],\"name\":\"updateSigner\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"index\",\"type\":\"uint256\"}],\"name\":\"getPrizes\",\"outputs\":[{\"internalType\":\"uint8\",\"name\":\"prize\",\"type\":\"uint8\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint8[]\",\"name\":\"new_prizes\",\"type\":\"uint8[]\"}],\"name\":\"updatePrizes\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"new_coin\",\"type\":\"address\"},{\"internalType\":\"uint8\",\"name\":\"_decimals\",\"type\":\"uint8\"}],\"name\":\"updateCoinInstance\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"nonce\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"signature\",\"type\":\"bytes\"}],\"name\":\"addScore\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"status\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"payoutReset\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"success\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getCoinsBalance\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getSigner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint8\",\"name\":\"position\",\"type\":\"uint8\"}],\"name\":\"readStructFromMapping\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"refUser\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"refScore\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint8\",\"name\":\"position\",\"type\":\"uint8\"},{\"internalType\":\"uint256\",\"name\":\"order\",\"type\":\"uint256\"}],\"name\":\"readStructFromMappingByOrder\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"refUser\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"refScore\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";

        public static object[] GetAbiObject()
        {
            object[] newAbi = new object[7];

            //constructor
            object[] constructorInputParams = new object[3];
            constructorInputParams[0] = new { internalType = "address", name = "_gameAddress", type = "address" };
            constructorInputParams[1] = new { internalType = "address", name = "_reflectionKeeper", type = "address" };
            constructorInputParams[2] = new { internalType = "address", name = "_coinInstance", type = "address" };

            newAbi[0] = new { inputs = constructorInputParams, name = "", stateMutability = "nonpayable", type = "constructor" };

            //startGamePrice
            object[] startGamePriceInputParams = new object[0];
            object[] startGamePriceOutputParams = new object[1];
            startGamePriceOutputParams[0] = new { internalType = "uint256", name = "", type = "uint256" };

            newAbi[1] = new { inputs = startGamePriceInputParams, outputs = startGamePriceOutputParams, name = START_GAME_PRICE_CALL, stateMutability = "view", type = "function" };

            //readStructFromMapping
            object[] readStructFromMappingInputParams = new object[1];
            readStructFromMappingInputParams[0] = new { internalType = "uint8", name = "position", type = "uint8" };

            object[] readStructFromMappingOutputParams = new object[2];
            readStructFromMappingOutputParams[0] = new { internalType = "address", name = "refUser", type = "address" };
            readStructFromMappingOutputParams[1] = new { internalType = "uint256", name = "refScore", type = "uint256" };

            newAbi[2] = new { inputs = readStructFromMappingInputParams, outputs = readStructFromMappingOutputParams, name = LEADERBOARD_CALL, stateMutability = "view", type = "function" };

            //prizes
            object[] getPrizesInputParams = new object[1];
            getPrizesInputParams[0] = new { internalType = "uint256", name = "index", type = "uint256" };
            object[] getPrizesOutputParams = new object[1];
            getPrizesOutputParams[0] = new { internalType = "uint8", name = "prize", type = "uint8" };

            newAbi[3] = new { inputs = getPrizesInputParams, outputs = getPrizesOutputParams, name = PRIZES_CALL, stateMutability = "view", type = "function" };

            //getCoinsBalance
            object[] getCoinsBalanceInputParams = new object[0];
            object[] getCoinsBalanceOutputParams = new object[1];
            getCoinsBalanceOutputParams[0] = new { internalType = "uint256", name = "", type = "uint256" };

            newAbi[4] = new { inputs = getCoinsBalanceInputParams, outputs = getCoinsBalanceOutputParams, name = TOTAL_POT_CALL, stateMutability = "view", type = "function" };

            //payToPlay  
            object[] payToPlayInputParams = new object[0];
            object[] payToPlayOutputParams = new object[0];

            newAbi[5] = new { inputs = payToPlayInputParams, outputs = payToPlayOutputParams, name = PAY_TO_PLAY_TRANSACTION, stateMutability = "nonpayable", type = "function" };

            //addScore
            object[] addScoreInputParams = new object[3];
            object[] addScoreOutputParams = new object[1];
            addScoreInputParams[0] = new { internalType = "uint256", name = "score", type = "uint256" };
            addScoreInputParams[1] = new { internalType = "uint256", name = "nonce", type = "uint256" };
            addScoreInputParams[2] = new { internalType = "bytes", name = "signature", type = "bytes" };

            addScoreOutputParams[0] = new { internalType = "bool", name = "status", type = "bool" };

            newAbi[6] = new { inputs = addScoreInputParams, name = PAY_SAVE_SESSION_TRANSACTION, stateMutability = "nonpayable", type = "function" };


            return newAbi;

        }


    }
}