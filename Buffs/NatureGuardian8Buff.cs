using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian8Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Corrupt Fairy");
			base.Description.SetDefault("\"A Corrupt Fairy is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			modPlayer.rapidStave = true;
			if (player.ZoneCorrupt)
			{
				player.moveSpeed += 5f;
				player.jumpBoost = true;
			}
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian8")] > 0)
			{
				modPlayer2.natureGuardian8 = true;
			}
			if (!modPlayer2.natureGuardian8)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
