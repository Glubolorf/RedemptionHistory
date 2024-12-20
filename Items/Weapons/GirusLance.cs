using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class GirusLance : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Lance");
			base.Tooltip.SetDefault("Spins the lance like a glaive\nRight-click to throw the lance");
		}

		public override void SetDefaults()
		{
			base.item.damage = 75;
			base.item.melee = true;
			base.item.width = 102;
			base.item.height = 100;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.channel = true;
			base.item.useStyle = 100;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 40, 0, 0);
			base.item.rare = 10;
			base.item.shoot = base.mod.ProjectileType("GirusLancePro1");
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.damage = 110;
				base.item.useStyle = 1;
				base.item.shoot = base.mod.ProjectileType("GirusLancePro2");
				base.item.shootSpeed = 27f;
				base.item.noMelee = true;
			}
			else
			{
				base.item.damage = 75;
				base.item.useStyle = 100;
				base.item.shoot = base.mod.ProjectileType("GirusLancePro1");
				base.item.shootSpeed = 0f;
				base.item.noMelee = true;
			}
			return base.CanUseItem(player);
		}

		public override bool UseItemFrame(Player player)
		{
			player.bodyFrame.Y = 3 * player.bodyFrame.Height;
			return true;
		}
	}
}
