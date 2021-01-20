using System;
using Promises;

namespace Dispatch {

public interface IDispatch {

    void Async (Action action);

    void Async (IPromise promise, Action action);

    void Async<T> (IPromise<T> promise, Func<T> func);

    void Sync (Action action);

    void Sync (IPromise promise, Action action);

    void Sync<T> (IPromise<T> promise, Func<T> func);
}

}
