using System;
using Redemption.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Pets
{
	public class ChickenBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Chicken Pet");
			base.Description.SetDefault("\"It will never leave you...\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.vanityPet[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			player.GetModPlayer<RedePlayer>().examplePet = true;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<ChickenPet>()] <= 0 && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<ChickenPet>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
