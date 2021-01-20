using System;

namespace Promises {

public class PromiseStateException : Exception {

	public PromiseStateException () : base() {}

	public PromiseStateException (string message) : base(message) {}

	public PromiseStateException (string message, Exception inner) : base(message, inner) {}
}

}
