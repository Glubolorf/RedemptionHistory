using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian19Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Terra Guardian");
			base.Description.SetDefault("\"A Terra Guardian is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			druidDamagePlayer.druidDamage += 0.4f;
			druidDamagePlayer.druidCrit += 40;
			modPlayer.rapidStave = true;
			modPlayer.staveScatterShot = true;
			modPlayer.fasterSeedbags = true;
			modPlayer.fasterSpirits = true;
			modPlayer.fasterStaves = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian19")] > 0)
			{
				modPlayer2.natureGuardian9 = true;
			}
			if (!modPlayer2.natureGuardian9)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
