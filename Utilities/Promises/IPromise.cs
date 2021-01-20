using System;

namespace Promises {

public interface IPromise {

	PromiseState State { get; }

	bool Resolved { get; }

	IPromise Then (Action action);

	IPromise<T> Then<T> (Func<T> func);

	IPromise Then (Func<IPromise> func);

	IPromise<T> Then<T> (Func<IPromise<T>> func);

	IPromise Catch (Action action);

	IPromise Always (Action action);

	void Resolve ();

	void Reject ();

	void Clear ();
}

public interface IPromise<T> {

	T Value { get; }

	PromiseState State { get; }

	bool Resolved { get; }

	IPromise<T> Then (Action action);

	IPromise<T> Then (Action<T> action);

	IPromise<U> Then<U> (Func<U> func);

	IPromise<U> Then<U> (Func<T, U> func);

	IPromise Then (Func<IPromise> func);

	IPromise Then (Func<T, IPromise> func);

	IPromise<U> Then<U> (Func<IPromise<U>> func);

	IPromise<U> Then<U> (Func<T, IPromise<U>> func);

	IPromise<T> Catch (Action action);

	IPromise<T> Always (Action action);

	bool Ready (out T t);

	void Resolve (T t);

	void Reject ();

	void Clear ();
}

}
