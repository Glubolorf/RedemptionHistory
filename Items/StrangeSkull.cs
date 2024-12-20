using System;
using Redemption.Buffs;
using Redemption.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class StrangeSkull : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mysterious Skull");
			base.Tooltip.SetDefault("Summons a certain spooky skeleton");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(669);
			base.item.width = 20;
			base.item.height = 20;
			base.item.rare = -12;
			base.item.shoot = ModContent.ProjectileType<TiedPet>();
			base.item.buffType = ModContent.BuffType<TiedPetBuff>();
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(base.item.buffType, 3600, true);
			}
		}
	}
}
