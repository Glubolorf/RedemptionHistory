using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
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
			player.GetModPlayer<RedePlayer>(base.mod).examplePet = true;
			bool flag = player.ownedProjectileCounts[base.mod.ProjectileType("ChickenPet")] <= 0;
			if (flag && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, base.mod.ProjectileType("ChickenPet"), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
