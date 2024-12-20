using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class VlitchCoreBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Core");
			base.Description.SetDefault("\"A Mini-Vlitch Core to protect you!\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.vanityPet[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.endurance += 0.2f;
			player.allDamage *= 0.4f;
			player.buffTime[buffIndex] = 18000;
			player.GetModPlayer<RedePlayer>().vlitchCoreAcc = true;
			if (player.ownedProjectileCounts[base.mod.ProjectileType("MiniVlitchCore")] <= 0 && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, base.mod.ProjectileType("MiniVlitchCore"), 120, 3f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
