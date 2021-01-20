using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Promises;

public abstract class Provider<T> : MonoBehaviour {
	protected T instance;
	protected IPromise<T> promise;

	public T Instance {
		get { return instance; }
	}

	protected virtual void Awake () {
		instance = Initialize();
		if (promise != null) {
			promise.Resolve(instance);
		}
		else {
			promise = new Promise<T>(instance);
		}
	}

	protected abstract T Initialize ();

	public IPromise<T> Provide () {
		if (promise == null) {
			promise = new Promise<T>();
		}
		return promise;
	}
}
