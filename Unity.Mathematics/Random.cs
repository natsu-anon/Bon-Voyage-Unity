using System;
using System.Runtime.CompilerServices;

namespace Unity.Mathematics {

	public partial struct Random {

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2 NextInUnitCirlce () {
			return NextOnUnitCircle() * NextFloat();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2 NextInUnitCirlce (float2 x) {
			return math.reject(NextOnUnitCircle() * NextFloat(), x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2 NextInUnitCirlce (double2 x) {
			return math.reject((double2)NextOnUnitCircle() * NextDouble(), x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 NextInUnitSphere () {
			return NextOnUnitSphere() * NextFloat();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 NextInUnitSphere (float3 x) {
			return math.reject(NextOnUnitSphere() * NextFloat(), x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3 NextInUnitSphere (double3 x) {
			return math.reject((double3)NextOnUnitSphere() * NextDouble(), x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2 NextOnUnitCircle () {
			float theta = NextFloat(math.TWO_PI);
			return new float2 {
				x = math.cos(theta),
				y = math.sin(theta)
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2 NextOnUnitCirlce (float2 x) {
			return math.reject(NextOnUnitCircle(), x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2 NextOnUnitCirlce (double2 x) {
			return math.reject((double2)NextOnUnitCircle(), x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 NextOnUnitSphere () {
			float theta = NextFloat(math.TWO_PI);
			float phi = NextFloat(math.PI);
			return new float3 {
				x = math.sin(theta) * math.cos(phi),
				y = math.cos(theta),
				z = math.sin(theta) * math.sin(phi)
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 NextOnUnitSphere (float3 x) {
			return math.reject(NextOnUnitSphere(), x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3 NextOnUnitSphere (double3 x) {
			return math.reject((double3)NextOnUnitSphere(), x);
		}
	}

}
