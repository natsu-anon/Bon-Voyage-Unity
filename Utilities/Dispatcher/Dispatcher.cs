using System;
﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Promises;

namespace Dispatch {

public class Dispatcher : IDispatch {

    TaskFactory factory;


    /*  STATICS  */


    public static Dispatcher Current () {
        return new Dispatcher(new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext()));
    }

    public static Dispatcher Current (CancellationToken token) {
        return new Dispatcher (new TaskFactory(
            token,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.FromCurrentSynchronizationContext()
        ));
    }

    public static Dispatcher LongCurrent () {
        return new Dispatcher (new TaskFactory(
            new CancellationToken {},
            TaskCreationOptions.LongRunning,
            TaskContinuationOptions.None,
            TaskScheduler.FromCurrentSynchronizationContext()
        ));
    }

    public static Dispatcher LongCurrent (CancellationToken token) {
        return new Dispatcher (new TaskFactory(
            token,
            TaskCreationOptions.LongRunning,
            TaskContinuationOptions.None,
            TaskScheduler.FromCurrentSynchronizationContext()
        ));
    }

    public static Dispatcher Short () {
        return new Dispatcher(new TaskFactory());
    }

    public static Dispatcher Short (TaskScheduler scheduler) {
        return new Dispatcher(new TaskFactory(scheduler));
    }

    public static Dispatcher Short (CancellationToken token) {
        return new Dispatcher(new TaskFactory(token));
    }

    public static Dispatcher Short (CancellationToken token, TaskScheduler scheduler) {
        return new Dispatcher(new TaskFactory(
            token,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            scheduler
        ));
    }

    public static Dispatcher Long (TaskScheduler scheduler, CancellationToken token) {
        return new Dispatcher(new TaskFactory(
            token,
            TaskCreationOptions.LongRunning,
            TaskContinuationOptions.None,
            scheduler
        ));
    }

    public static async void Run (Action action) {
        await Task.Run(action);
    }

    public static async void Run (Action action, CancellationToken token) {
        await Task.Run(action, token);
    }

    public static async void Run (IPromise promise, Action action) {
        await Task.Run(() => {
            action();
            promise.Resolve();
        });
    }

    public static async void Run (IPromise promise, Action action, CancellationToken token) {
        await Task.Run(() => {
            if (!token.IsCancellationRequested) {
                action();
                if (!token.IsCancellationRequested) {
                    promise.Resolve();
                }
            }
        }, token);
    }

    public static async void Run<T> (IPromise<T> promise, Func<T> func) {
        await Task.Run(() => { promise.Resolve(func()); });
    }

    public static async void Run<T> (IPromise<T> promise, Func<T> func, CancellationToken token) {
        await Task.Run(() => {
            if (!token.IsCancellationRequested) {
                T res = func();
                if (!token.IsCancellationRequested) {
                    promise.Resolve(res);
                }
            }
        }, token);
    }


    /*  CONSTRUCTOR  */


    Dispatcher (TaskFactory factory) {
        this.factory = factory;
    }


    /*  METHODS  */


    public async void Async (Action action) {
        await factory.StartNew(action);
    }

    public async void Async (IPromise promise, Action action) {
        await factory.StartNew(() => {
            action();
            if (!factory.CancellationToken.IsCancellationRequested) {
                promise.Resolve();
            }
        });
    }

    public async void Async<T> (IPromise<T> promise, Func<T> func) {
        await factory.StartNew(() => {
            T res = func();
            if (!factory.CancellationToken.IsCancellationRequested) {
                promise.Resolve(res);
            }
        });
    }

    public void Sync (Action action) {
        // Task task = factory.StartNew(action);
        factory.StartNew(action);
        // task.Wait();
    }

    public void Sync (IPromise promise, Action action) {
        // Task task = factory.StartNew(() => {
        factory.StartNew(() => {
            action();
            if (!factory.CancellationToken.IsCancellationRequested) {
                promise.Resolve();
            }
        });
        // task.Wait();
    }

    public void Sync<T> (IPromise<T> promise, Func<T> func) {
        // Task task = factory.StartNew(() => { promise.Resolve(func()); });
        factory.StartNew(() => {
            T res = func();
            if (!factory.CancellationToken.IsCancellationRequested) {
                promise.Resolve(res);
            }
        });
        // task.Wait();
    }
}

}
