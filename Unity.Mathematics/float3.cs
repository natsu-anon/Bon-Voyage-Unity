using System;
using System.Runtime.CompilerServices;

namespace Unity.Mathematics {

	public partial struct float3 {

		public static readonly float3 one = new float3(1f, 1f, 1f);

		public static readonly float3 right = new float3(1f, 0f, 0f);

		public static readonly float3 up = new float3(0f, 1f, 0f);

		public static readonly float3 forward = new float3(0f, 0f, 1f);

		public static readonly float3 left = new float3(-1f, 0f, 0f);

		public static readonly float3 down = new float3(0f, -1f, 0f);

		public static readonly float3 back = new float3(0f, 0f, -1f);

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public float3 x0z {
			get { return new float3(x, 0, z); }
		}
	}

}
