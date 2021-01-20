using System;
﻿using System.Collections;
using System.Collections.Generic;

namespace Promises {

public class Promise<T> : IPromise<T> {
	public T Value { get { return value; }}
	public PromiseState State { get { return state; }}
	public bool Resolved { get { return state == PromiseState.Resolved; }}
	T value;
	Queue<Action<T>> pending;
	Queue<Action> catchers;
	PromiseState state;


	/*  CONSTRUCTORS  */


	public Promise () {
		state = PromiseState.Pending;
		pending = new Queue<Action<T>>();
		catchers = new Queue<Action>();
	}

	public Promise (T value) {
		state = PromiseState.Resolved;
		this.value = value;
	}

	public Promise (Action<Action<T>, Action> resolver) : this() {
		resolver(Resolve, Reject);
	}


	/*  METHODS  */


	public IPromise<T> Then (Action<T> action) {
		switch (state) {
			case PromiseState.Pending:
				pending.Enqueue(action);
				break;
			case PromiseState.Resolved:
				action(value);
				break;
		}
		return this;
	}

	public IPromise<T> Then (Action action) {
		switch (state) {
			case PromiseState.Pending:
				pending.Enqueue((T t) => { action(); });
				break;
			case PromiseState.Resolved:
				action();
				break;
		}
		return this;
	}

	public IPromise<U> Then<U> (Func<U> func) {
		switch (state) {
			case PromiseState.Pending:
				IPromise<U> promise = new Promise<U>();
				pending.Enqueue((T t) => { promise.Resolve(func()); });
				return promise;
				break;
			case PromiseState.Resolved:
				return new Promise<U>(func());
				break;
			default:
				IPromise<U> res = new Promise<U>();
				res.Reject();
				return res;
				break;
		}
	}

	public IPromise<U> Then<U> (Func<T, U> func) {
		switch (state) {
			case PromiseState.Pending:
				IPromise<U> promise = new Promise<U>();
				pending.Enqueue((T t) => { promise.Resolve(func(t)); });
				return promise;
				break;
			case PromiseState.Resolved:
				return new Promise<U>(func(value));
				break;
			default:
				IPromise<U> res = new Promise<U>();
				res.Reject();
				return res;
				break;
		}
	}

	public IPromise Then (Func<IPromise> func) {
		switch (state) {
			case PromiseState.Pending:
				IPromise promise = new Promise();
				pending.Enqueue((T t) => { func().Then(promise.Resolve).Catch(promise.Reject); });
				return promise;
				break;
			case PromiseState.Resolved:
				return func();
				break;
			default:
				IPromise res = new Promise();
				res.Reject();
				return res;
				break;
		}
	}

	public IPromise Then (Func<T, IPromise> func) {
		switch (state) {
			case PromiseState.Pending:
				IPromise promise = new Promise();
				pending.Enqueue((T t) => { func(t).Then(promise.Resolve).Catch(promise.Reject); });
				return promise;
				break;
			case PromiseState.Resolved:
				return func(value);
				break;
			default:
				IPromise res = new Promise();
				res.Reject();
				return res;
				break;
		}
	}

	public IPromise<U> Then<U> (Func<IPromise<U>> func) {
		switch (state) {
			case PromiseState.Pending:
				IPromise<U> promise = new Promise<U>();
				pending.Enqueue((T t) => { func().Then(promise.Resolve).Catch(promise.Reject); });
				return promise;
				break;
			case PromiseState.Resolved:
				return func();
				break;
			default:
				IPromise<U> res = new Promise<U>();
				res.Reject();
				return res;
				break;
		}
	}

	public IPromise<U> Then<U> (Func<T, IPromise<U>> func) {
		switch (state) {
			case PromiseState.Pending:
				IPromise<U> promise = new Promise<U>();
				pending.Enqueue((T t) => { func(t).Then(promise.Resolve).Catch(promise.Reject); });
				return promise;
				break;
			case PromiseState.Resolved:
				return func(value);
				break;
			default:
				IPromise<U> res = new Promise<U>();
				res.Reject();
				return res;
				break;
		}
	}

	public IPromise<T> Catch (Action action) {
		switch (state) {
			case PromiseState.Pending:
				catchers.Enqueue(action);
				break;
			case PromiseState.Rejected:
				action();
				break;
		}
		return this;
	}

	public IPromise<T> Always (Action action) {
		switch (state) {
			case PromiseState.Pending:
				pending.Enqueue((T t) => { action(); });
				catchers.Enqueue(action);
				break;
			default:
				action();
				break;
		}
		return this;
	}

	public bool Ready (out T result) {
		result = value;
		return state == PromiseState.Resolved;
	}

	public void Resolve (T value) {
		switch (state) {
			case PromiseState.Pending:
				this.value = value;
				state = PromiseState.Resolved;
				while (pending.Count > 0) {
					pending.Dequeue()(value);
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

	/*  This is silly
		public void Update (T value) {
			switch (state) {
				case PromiseState.Pending:
					while (pending.Count > 0) {
						pending.Dequeue()(value);
					}
					catchers.Clear();
					goto case PromiseState.Resolved;
				case PromiseState.Resolved:
					this.value = value;
					break;
				default:
					throw new PromiseStateException (
						String.Format(
							"Attempt to update a promise that is in state: {0}, a promise can only be updated in state {1} or {2}",
							state, PromiseState.Resolved, PromiseState.Pending
					));
			}
		}
	*/

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
