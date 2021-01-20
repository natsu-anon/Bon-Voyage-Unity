using System;
using System.Runtime.CompilerServices;

namespace Unity.Mathematics {

	public partial class math {

		public const float TWO_PI = 2f * PI;
		public const double TWO_PI_DBL = 2d * PI_DBL;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float angle (float3 a, float3 b) {
			return acos(dot(a, b) / (length(a) * length(b)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double angle (double3 a, double3 b) {
			return acos(dot(a, b) / (length(a) * length(b)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float signedangle (float3 a, float3 b, float3 c) {
			return -sign(dot(cross(normalize(a), normalize(b)), c)) * angle(a, b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double signedangle (double3 a, double3 b, double3 c) {
			return -sign(dot(cross(normalize(a), normalize(b)), c)) * angle(a, b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double saturatedlerp(double x, double y, double s) {
			return lerp(x, y, saturate(s));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single saturatedlerp(Single x, Single y, Single s) {
			return lerp(x, y, saturate(s));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 saturatedlerp(double2 x, double2 y, double s) {
			return lerp(x, y, saturate(s));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 saturatedlerp(double2 x, double2 y, double2 s) {
			return lerp(x, y, saturate(s));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 saturatedlerp(double3 x, double3 y, double s) {
			return lerp(x, y, saturate(s));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 saturatedlerp(double3 x, double3 y, double3 s) {
			return lerp(x, y, saturate(s));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 saturatedlerp(float2 x, float2 y, Single s) {
			return lerp(x, y, saturate(s));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 saturatedlerp(float2 x, float2 y, float2 s) {
			return lerp(x, y, saturate(s));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 saturatedlerp(float3 x, float3 y, Single s) {
			return lerp(x, y, saturate(s));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 saturatedlerp(float3 x, float3 y, float3 s) {
			return lerp(x, y, saturate(s));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion saturatedslerp(quaternion a, quaternion b, float s) {
			return slerp(a, b, saturate(s));
		}


		/* OMEDETOU, UNITY.  PROJECT ADDED TO MATH PACKAGE

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 project (float2 a, float2 b) {
			return b * dot(a, b) / dot(b, b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 project (double2 a, double2 b) {
			return b * dot(a, b) / dot(b, b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 project (float3 a, float3 b) {
			return b * dot(a, b) / dot(b, b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 project (double3 a, double3 b) {
			return b * dot(a, b) / dot(b, b);
		}
		*/

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 reject (float2 a, float2 b) {
			return a - project(a, b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 reject (double2 a, double2 b) {
			return a - project(a, b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 reject (float3 a, float3 b) {
			return a - project(a, b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 reject (double3 a, double3 b) {
			return a - project(a, b);
		}
	}
}
