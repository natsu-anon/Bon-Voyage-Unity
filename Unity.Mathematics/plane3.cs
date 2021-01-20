using System;
using System.Runtime.CompilerServices;

namespace Unity.Mathematics {

	public partial struct plane3 {
		public readonly float3 normal;
		public readonly float3 point;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public plane3 (float3 normal, float3 point) {
			this.normal = normal;
			this.point = point;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public plane3 (float3 normal, float distance) {
			this.normal = normal;
			point = normal * distance;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public plane3 (float3 a, float3 b, float3 c) {
			point = a;
			normal = math.cross(b - a, c - a);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float absdistance (float3 x) {
			return math.abs(distance(x));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 project (float3 x) {
			return x + normal * distance(x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 reject (float3 x) {
			return normal * distance(x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float distance (float3 x) {
			return math.dot(normal, point) - math.dot(normal, x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public plane3 flip () {
			return new plane3(-normal, point);
		}
	}

}
