using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Donator.Lantard
{
	public class LantardPetBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Fluffy Boi");
			base.Description.SetDefault("\"Fluff.\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.vanityPet[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			player.GetModPlayer<RedePlayer>().lantardPet = true;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<LantardPatreon_Pet>()] <= 0 && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<LantardPatreon_Pet>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
