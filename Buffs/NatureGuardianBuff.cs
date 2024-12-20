using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardianBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Nature Pixie");
			base.Description.SetDefault("\"A Nature Pixie is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			modPlayer.rapidStave = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian1")] > 0)
			{
				modPlayer2.natureGuardian1 = true;
			}
			if (!modPlayer2.natureGuardian1)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
