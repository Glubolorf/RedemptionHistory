using System;
using Redemption.Projectiles.Summon;
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
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<AncientMirrorShieldPro>()] > 0)
			{
				modPlayer2.ancientMirror = true;
			}
			if (!modPlayer2.ancientMirror)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
