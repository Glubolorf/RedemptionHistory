using System;
using Redemption.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Pets
{
	public class HalPetBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Ham Sandwich");
			base.Description.SetDefault("\"Fear me\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.vanityPet[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			player.GetModPlayer<RedePlayer>().halPet = true;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<HalPet>()] <= 0 && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<HalPet>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
