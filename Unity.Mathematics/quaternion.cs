using System;
using System.Runtime.CompilerServices;

namespace Unity.Mathematics {

	public partial struct quaternion {
		const float DOT_EPSILON = 0.999999f;
		const double DOT_EPSILON_DBL = 0.999999d;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public quaternion (float3 v, float w) { value.x = v.x; value.y = v.y; value.z = v.z; value.w = w; }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerXY (float x, float y) {
			return EulerXYZ(math.float3(x, y, 0f));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerXY (float2 xy) {
			return EulerXYZ(math.float3(xy, 0f));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion FromTo (float3 a, float3 b) {
			float3 u = math.normalize(a);
			float3 v = math.normalize(b);
			float cos_theta = math.dot(u, v);
			if (cos_theta > DOT_EPSILON) {
				return quaternion.identity;
			}
			else if (cos_theta < -DOT_EPSILON) {
				return new quaternion(0f, 0f, 0f, -1f);
			}
			else {
				return AxisAngle(math.cross(u,v), math.acos(cos_theta));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion FromTo (double3 a, double3 b) {
			double3 u = math.normalize(a);
			double3 v = math.normalize(b);
			double cos_theta = math.dot(u, v);
			if (cos_theta > DOT_EPSILON_DBL) {
				return quaternion.identity;
			}
			else if (cos_theta < -DOT_EPSILON_DBL) {
				return new quaternion(0f, 0f, 0f, -1f);
			}
			else {
				return AxisAngle((float3)math.cross(u,v), (float)math.acos(cos_theta));
			}
		}
	}

}
