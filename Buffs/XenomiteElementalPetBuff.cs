using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class XenomiteElementalPetBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Elemental Pet");
			base.Description.SetDefault("\"Cute...\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.vanityPet[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			player.GetModPlayer<RedePlayer>().xenoPet = true;
			if (player.ownedProjectileCounts[base.mod.ProjectileType("XenomiteElementalPet")] <= 0 && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, base.mod.ProjectileType("XenomiteElementalPet"), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
