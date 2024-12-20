using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Gores.Misc
{
	public class DeadTreeFX : ModGore
	{
		public override void OnSpawn(Gore gore)
		{
			gore.velocity = new Vector2(Utils.NextFloat(Main.rand) - 0.5f, Utils.NextFloat(Main.rand) * 6.2831855f);
			gore.numFrames = 8;
			gore.frame = (byte)Main.rand.Next(8);
			gore.frameCounter = (byte)Main.rand.Next(8);
			this.updateType = 910;
		}
	}
}
