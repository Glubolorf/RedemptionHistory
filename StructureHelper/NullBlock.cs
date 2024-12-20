using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.StructureHelper
{
	internal class NullBlock : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			TileID.Sets.DrawsWalls[(int)base.Type] = true;
		}
	}
}
