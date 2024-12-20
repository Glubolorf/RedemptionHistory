using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class KSShieldBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Shielded");
			base.Description.SetDefault("You are protected by a shield");
			Main.debuff[base.Type] = true;
			Main.pvpBuff[base.Type] = true;
			Main.buffNoSave[base.Type] = true;
			this.canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statLifeMax2 = 200;
			if (player.lifeRegen > 0)
			{
				player.lifeRegen = 0;
			}
			player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[base.mod.ProjectileType("KSShieldPro")] == 0)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
