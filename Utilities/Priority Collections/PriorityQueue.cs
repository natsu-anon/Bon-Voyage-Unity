using System;
﻿using System.Collections;
using System.Collections.Generic;

namespace PriorityCollections {

// Priority FIFO (lower comes out first)
public class PriorityQueue<T> : PriorityCollection<T> {

	public PriorityQueue () : base() {}

	public PriorityQueue (ICollection<T> items, Func<T, float> priority) : base(items, priority) {}

	public T Dequeue () {
		return getAndRemoveFirst();
	}

	public void Enqueue (float priority, T item) {
		add(priority, item);
	}

	protected override void add (float priority, T item) {
		addAfter(priority, item);
	}
}

}
