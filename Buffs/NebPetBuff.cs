using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NebPetBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Nebby Pet");
			base.Description.SetDefault("\"Nebbity Nobbity Nibbity Nuu, this small star gril be followin' you\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.vanityPet[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			player.GetModPlayer<RedePlayer>().nebPet = true;
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NebPet")] <= 0 && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, base.mod.ProjectileType("NebPet"), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
