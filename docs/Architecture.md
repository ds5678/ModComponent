ModComponent consist of the two modules API and Mapper.

* ModComponentAPI provides the API for modders to create new items with the Unity Editor. This module is completely independent from Hinterland's code and thus unaffected by any changes done with future updates to the game.
* ModComponentMapper integrates new items into the game and ensures that they will be handled properly. This module depends on Hinterland's code and maps (as in "translates") items from ModComponentAPI to Hinterland's API

This separation is necessary because
* the Unity Editor will not properly load Hinterland's API (in fact the DLL from any other Unity game will not work)
* I will not extract and publish Hinterland's code - it's theirs and not mine

![architecture](https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/architecture.png)