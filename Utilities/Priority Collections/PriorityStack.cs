using System;
﻿using System.Collections;
using System.Collections.Generic;

namespace PriorityCollections {

// Priority FILO (lower comes out first)
public class PriorityStack<T> : PriorityCollection<T> {

	public PriorityStack () : base() {}

	public PriorityStack (ICollection<T> items, Func<T, float> priority) : base(items, priority) {}

	public T Pop () {
		return getAndRemoveFirst();
	}

	public void Push (float priority, T item) {
		add(priority, item);
	}

	protected override void add (float priority, T item) {
		addBefore(priority, item);
	}

}

}
