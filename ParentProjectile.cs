using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Redemption
{
	public abstract class ParentProjectile : ModProjectile
	{
		public virtual void SetAI(float[] ai, int aiType)
		{
		}

		public virtual Vector4 GetFrameV4()
		{
			return new Vector4(0f, 0f, 1f, 1f);
		}
	}
}
