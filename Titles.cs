using System;
using Terraria.ModLoader;

namespace Redemption
{
	public class Titles : ModPlayer
	{
		public override void ResetEffects()
		{
			this.text = false;
		}

		public override void PreUpdate()
		{
			if (!RedeProjectile.AnyProjectiles(ModContent.ProjectileType<Title>()))
			{
				this.alphaText = 255f;
				this.alphaText2 = 255f;
			}
		}

		public bool text;

		public float alphaText = 255f;

		public float alphaText2 = 255f;

		public int BossID;
	}
}
