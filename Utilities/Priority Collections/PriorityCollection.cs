using System;
﻿using System.Collections;
using System.Collections.Generic;

namespace PriorityCollections {

public abstract class PriorityCollection<T> {
	protected float min;

	public float Minimum { get { return min; } }

	public LinkedList<PriorityItem<T>> list;

	public int Count {
		get { return list.Count; }
	}

	public PriorityCollection () {
		list = new LinkedList<PriorityItem<T>>();
	}

	public PriorityCollection (ICollection<T> items, Func<T, float> func) {
		list = new LinkedList<PriorityItem<T>>();
		foreach (T item in items) {
			add(func(item), item);
		}
	}

	public void Clear () {
		list.Clear();
	}

	public T Peek () {
		return list.First.Value.item;
	}

	protected abstract void add (float priority, T item);

	protected void addBefore (float priority, T item) {
		if (list.Count == 0 || priority < min) {
			addMin(priority, item);
		}
		else {
			LinkedListNode<PriorityItem<T>> node = list.First;
			while (node != null) {
				if (priority <= node.Value.priority) {
					list.AddBefore(node, new PriorityItem<T>{
						priority = priority,
						item = item
					});
					return;
				}
				node = node.Next;
			}
			list.AddLast(new PriorityItem<T> {
				priority = priority,
				item = item
			});
		}
	}

	protected void addAfter  (float priority, T item) {
		if (list.Count == 0 || priority < min) {
			addMin(priority, item);
		}
		else {
			LinkedListNode<PriorityItem<T>> node = list.First;
			while (node != null) {
				// if less than the next one add it after current
				if (node.Next != null && priority < node.Next.Value.priority) {
					list.AddAfter(node, new PriorityItem<T>{
						priority = priority,
						item = item
					});
					return;
				}
				node = node.Next;
			}
			list.AddLast(new PriorityItem<T> {
				priority = priority,
				item = item
			});
		}
	}

	protected T getAndRemoveFirst () {
		T item = list.First.Value.item;
		list.RemoveFirst();
		if (list.Count > 0) {
			min = list.First.Value.priority;
		}
		return item;
	}

	void addMin (float priority, T item) {
		min = priority;
		list.AddFirst(new PriorityItem<T> {
			priority = priority,
			item = item
		});
	}
}

}
