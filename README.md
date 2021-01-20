# Bon-Voyage-Unity
Collection of things I've made for Unity. Better documentation coming __SOON__:tm:


Why so many Interfaces?  I think I had my reasons at the time.

## Utilities
Code for easier:
1. [Promises](http://www.what-could-possibly-go-wrong.com/promises-for-game-development/).
2. multi-threading/async via `Dispatcher.cs`
3. Priority Collections: `PriorityQueue.cs` and `PriorityStack.cs`.  Exactly what you think it is.
4. (asynchronous) text parsing & regex help with promise-like behavior.
5. general unity things
	- `Provider.cs` allows for less conflicting initializations using Promises
	- `UnityTools.cs` allows adding behavior to when the editor/standalone is exited

## Unity.Mathematics
Extends Unity's [new mathematics package](https://docs.unity3d.com/Packages/com.unity.mathematics@1.1/manual/index.html).  If you're not using it don't worry about this.
