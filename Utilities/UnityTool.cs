using System;
using UnityEngine;
using UnityEditor;

public static class UnityTool {

	#if UNITY_EDITOR
		public static Action<PlayModeStateChange> OnExit (Action action) {
			Action<PlayModeStateChange> wrapper = (PlayModeStateChange state) => {};
			wrapper = (PlayModeStateChange state) => {
				switch (state) {
					case PlayModeStateChange.ExitingPlayMode:
						action();
						EditorApplication.playModeStateChanged -= wrapper;
						break;
				}
			};
			EditorApplication.playModeStateChanged += wrapper;
			return wrapper;
		}
	#elif UNITY_STANDALONE
		public static Action OnExit (Action action) {
			Action wrapper = () => {};
			wrapper = () => {
				action();
				Application.wantsToQuit -= wrapper;
			}
			Application.wantsToQuit += wrapper;
			return wrapper;
		}
	#endif



	#if UNITY_EDITOR
		public static void RemoveOnExit (Action<PlayModeStateChange> wrapper) {
			EditorApplication.playModeStateChanged -= wrapper;
		}
	#elif UNITY_STANDALONE
		public static void RemoveOnExit (Action action) {
			Application.wantsToQuit -= wrapper;
		}
	#endif


	// NOTE return action so you can later pass it into RemoveEditorQuit if you want to
	public static Action OnEditorQuit (Action action) {
		EditorApplication.quitting += action;
		return action;
	}

	public static void RemoveEditorQuit (Action action) {
		EditorApplication.quitting -= action;
	}
}
