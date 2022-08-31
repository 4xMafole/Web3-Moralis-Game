# **Endless Runner Web3**
It an endless game having addictive experience powered by Web3 technology. The following are the game functionalities.

- [x] Approve game tokens
- [x] Start game play
- [x] Balance of the game tokens
- [x] Prizes
- [ ] Leaderboard
- [ ] Saving the scores

### Functions Description
- Pay-To-Play: Player has to pay first before he/she can play. (✅Working)
- Start-Price: Player is given a starting price of tokens for the game. (✅Working)
- Total-Pot: Player is a able to see the available balance of coins for the game. (✅Working)
- Prizes: Player is able to win prizes ranging from 0 to 5. (✅Working)
- Leaderboard: Player is able to see the leaderboard of top 5 players. (🐞Bug)
- Pay-Save-Session: Player is able to save his/her scores into the blockchain.(🐞Bug)

### Supportive Platforms
- [x] Desktop(windows, mac and linux)
- [x] Mobile (android)
    - A player can have unsual experience in playing the game since the transaction signing and verifying delays. Hence, needs to switch up between game and provider(MetaMask) for contract signing. (Moralis' outdated feature)

### Main Project Folder Structure
📦Endless  
 ┣ 📂Runner  
 ┃ ┣ 📂Prefabs  
 ┃ ┃ ┣ 📜Auth UI.prefab  
 ┃ ┃ ┗ 📜Auth UI.prefab.meta  
 ┃ ┣ 📂Scripts  
 ┃ ┃ ┣ 📜AuthManager.cs  
 ┃ ┃ ┣ 📜AuthManager.cs.meta  
 ┃ ┃ ┣ 📜ContractFunctions.cs  
 ┃ ┃ ┣ 📜ContractFunctions.cs.meta  
 ┃ ┃ ┣ 📜LeaderBoardContractData.cs  
 ┃ ┃ ┣ 📜LeaderBoardContractData.cs.meta  
 ┃ ┃ ┣ 📜Root.cs  
 ┃ ┃ ┣ 📜Root.cs.meta  
 ┃ ┃ ┣ 📜Web3Manager.cs  
 ┃ ┃ ┗ 📜Web3Manager.cs.meta  
 ┃ ┣ 📜Prefabs.meta  
 ┃ ┗ 📜Scripts.meta  
 ┣ 📂Scenes  
 ┃ ┣ 📜Web3Debugging Scene.unity  
 ┃ ┗ 📜Web3Debugging Scene.unity.meta  
 ┣ 📂SmartContract  
 ┃ ┗ 📜smartAbi.json  
 ┣ 📜Runner.meta  
 ┗ 📜Scenes.meta

 ### Adding Moralis
 1. Login/Register to [Moralis](https://admin.moralis.io/register).
 2. Grab app's url and id which will be added to unity project.
 3. Add smart contract event sync known as `StartSessionEvent` from `StartSession()` event from smart contract abi.

 NB: Watch this youtube [tutorial](https://www.youtube.com/watch?v=fSKCF_tSKQc&t=1319s) to see how to add events. 

 ### Web3 Integration
 1. Drag `Auth UI prefab` into the new scene (***Endless > Runner > Prefabs***).
 2. Change the scene to open when successful player authenticated and has valid tokens.
    
    - Auth UI prefab > Welcome > ... > Buttons > StartButton 
    - On StartButton's `OnClick()` event there is a `payStartCoin(index)` function which accepts **Scene Build Index**
    - **NB:** Make sure the scene to be open has been added in build settings
 3. That's it enjoy

 ### Sample
 There is a sample game which is used to test the web3 functionalities in an **EndlessTurns folder**. You can get rid of it once the integration has been successful. Because it eats up space.
 #### Getting rid of sample game
 - Remove all the scenes from the build settings associated with the game.
 - Delete the folder which contains the sample game.
    