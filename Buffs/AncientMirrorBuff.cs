using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class AncientMirrorBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Ancient Mirror Shield");
			base.Description.SetDefault("\"Reflects projectiles!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("AncientMirrorShieldPro")] > 0)
			{
				modPlayer.ancientMirror = true;
			}
			if (!modPlayer.ancientMirror)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
