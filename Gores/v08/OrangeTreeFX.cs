using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Gores.v08
{
	public class OrangeTreeFX : ModGore
	{
		public override void OnSpawn(Gore gore)
		{
			gore.velocity = new Vector2(Utils.NextFloat(Main.rand) - 0.5f, Utils.NextFloat(Main.rand) * 6.2831855f);
			gore.numFrames = 1;
			this.updateType = 910;
		}
	}
}
