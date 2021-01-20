using System;
﻿using System.Collections;
using System.Collections.Generic;

namespace Promises {

public class Promise : IPromise {
	public PromiseState State { get { return state; }}
	public bool Resolved { get { return state == PromiseState.Resolved; }}
	Queue<Action> pending;
	Queue<Action> catchers;
	PromiseState state;


	/*  STATICS  */


	public static IPromise All (params IPromise[] promises) {
		IPromise res = new Promise();
		for (int i = 0; i < promises.Length; i++) {
			promises[i].Then(() => { CheckAll(res, promises); });
		}
		return res;
	}

	public static IPromise Any (params IPromise[] promises) {
		IPromise res = new Promise();
		for (int i = 0; i < promises.Length; i++) {
			promises[i].Then(() => {
				if (!res.Resolved) {
					res.Resolve();
				}
			});
		}
		return res;
	}

	static void CheckAll (IPromise promise, params IPromise[] promises) {
		for (int i = 0; i < promises.Length; i++) {
			if (!promises[i].Resolved) {
				return;
			}
		}
		promise.Resolve();
	}


	/*  CONSTRUCTORS  */


	public Promise () {
		state = PromiseState.Pending;
		pending = new Queue<Action>();
		catchers = new Queue<Action>();
	}

	public Promise (Action<Action, Action> resolver) : this() {
		resolver(Resolve, Reject);
	}


	/*  METHODS  */


	public IPromise Then (Action action) {
		switch (state) {
			case PromiseState.Resolved:
				action();
				break;
			case PromiseState.Pending:
				pending.Enqueue(action);
				break;
		}
		return this;
	}

	public IPromise<T> Then<T> (Func<T> func) {
		switch (state) {
			case PromiseState.Resolved:
				return new Promise<T>(func());
				break;
			case PromiseState.Pending:
				IPromise<T> promise = new Promise<T>();
				pending.Enqueue(() => { promise.Resolve(func()); });
				return promise;
				break;
			default:
				IPromise<T> res = new Promise<T>();
				res.Reject();
				return res;
				break;
		}
	}

	public IPromise Then (Func<IPromise> func) {
		switch (state) {
			case PromiseState.Resolved:
				return func();
			case PromiseState.Pending:
				IPromise promise = new Promise();
				pending.Enqueue(() => { func().Then(promise.Resolve).Catch(promise.Reject); });
				return promise;
				break;
			default:
				IPromise res = new Promise();
				res.Reject();
				return res;
				break;
		}
	}

	public IPromise<T> Then<T> (Func<IPromise<T>> func) {
		switch (state) {
			case PromiseState.Resolved:
				return func();
			case PromiseState.Pending:
				IPromise<T> promise = new Promise<T>();
				pending.Enqueue(() => { func().Then(promise.Resolve).Catch(promise.Reject); });
				return promise;
				break;
			default:
				IPromise<T> res = new Promise<T>();
				res.Reject();
				return res;
				break;
		}
	}

	public IPromise Catch (Action action) {
		switch (state) {
			case PromiseState.Rejected:
				action();
				break;
			case PromiseState.Pending:
				catchers.Enqueue(action);
				break;
		}
		return this;
	}

	public IPromise Always (Action action) {
		switch (state) {
			case PromiseState.Pending:
				pending.Enqueue(action);
				catchers.Enqueue(action);
				break;
			default:
				action();
				break;
		}
		return this;
	}

	public void Resolve () {
		switch (state) {
			case PromiseState.Pending:
				state = PromiseState.Resolved;
				while (pending.Count > 0) {
					pending.Dequeue()();
				}
				catchers.Clear();
				break;
			default:
				throw new PromiseStateException(
					String.Format(
						"Attempt to resolve a promise that is already in state: {0}, a promise can only be resolved in state {1}",
						state, PromiseState.Pending
				));
		}
	}

	public void Reject () {
		switch (state) {
			case PromiseState.Pending:
				state = PromiseState.Rejected;
				while (catchers.Count > 0) {
					catchers.Dequeue()();
				}
				pending.Clear();
				break;
			default:
				throw new PromiseStateException(
					String.Format(
						"Attempt to reject a promise that is already in state: {0}, a promise can only be rejected  in state {1}",
						state, PromiseState.Pending
				));
		}
	}

	public void Clear () {
		state = PromiseState.Pending;
		pending.Clear();
		catchers.Clear();
	}

}

}
