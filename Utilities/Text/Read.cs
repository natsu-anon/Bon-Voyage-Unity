using System;
using System.IO;
using System.Threading;
﻿using System.Collections;
using System.Collections.Generic;
using Dispatch;
using Promises;


namespace TextUtil {

public static class Read {

	public static void Async (byte[] bytes, IPromise<ReadStatus> promise, Func<string, LineStatus> parse) {
		Dispatcher.Run(promise, () => {
			ReadStatus res;
			using(Stream stream = new MemoryStream(bytes, false)) {
				res = read(stream, parse);
			}
			return res;
		});
	}

	public static void Async (byte[] bytes, IPromise<ReadStatus> promise, Func<string, LineStatus> parse, CancellationToken token) {
		Dispatcher.Short(token).Async(promise, () => {
			ReadStatus res;
			using(Stream stream = new MemoryStream(bytes, false)) {
				res = read(stream, parse, token);
			}
			return res;
		});
	}

	public static void Async (string path, IPromise<ReadStatus> promise, Func<string, LineStatus> parse) {
		if (File.Exists(path)) {
			Dispatcher.Run(promise, () => {
				ReadStatus res;
				using (Stream stream = File.OpenRead(path)) {
					res = read(stream, parse);
				}
				return res;
			});
		}
		else {
			throw new FileNotFoundException(String.Format("No file with path: {0}", path));
		}
	}

	public static void Async (string path, IPromise<ReadStatus> promise, Func<string, LineStatus> parse, CancellationToken token) {
		if (File.Exists(path)) {
			Dispatcher.Short(token).Async(promise, () => {
				ReadStatus res;
				using (Stream stream = File.OpenRead(path)) {
					res = read(stream, parse, token);
				}
				return res;
			});
		}
		else {
			throw new FileNotFoundException(String.Format("No file with path: {0}", path));
		}
	}

	public static void Regex (string path, RegexHelp regex) {
		if (File.Exists(path)) {
			regex.Match(File.ReadAllText(path));
		}
	}

	static ReadStatus read (Stream stream, Func<string, LineStatus> parse) {
		LineStatus status = new LineStatus { success = true, message = "" };
		uint line = 0;
		using (StreamReader reader = new StreamReader(stream)) {
			while (!reader.EndOfStream && status.success) {
				status = parse(reader.ReadLine());
				line++;
			}
		}
		if (status.success) {
			return new ReadStatus { success = true, line = 0, message = "" };
		}
		else {
			return new ReadStatus{ success = false, line = line, message = status.message };
		}
	}

	static ReadStatus read (Stream stream, Func<string, LineStatus> parse, CancellationToken token) {
		LineStatus status = new LineStatus { success = true, message = "" };
		uint line = 0;
		using (StreamReader reader = new StreamReader(stream)) {
			while (!token.IsCancellationRequested && !reader.EndOfStream && status.success) {
				status = parse(reader.ReadLine());
				line++;
			}
		}
		if (status.success) {
			return new ReadStatus { success = true, line = 0, message = "" };
		}
		else {
			return new ReadStatus{ success = false, line = line, message = status.message };
		}
	}

}

}
