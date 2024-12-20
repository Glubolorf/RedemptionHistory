using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class UraniumRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Uranium Rod");
			base.Tooltip.SetDefault("Summons a giant radioactive cloud at cursor point\nRight-clicking will summon a smaller cloud that rains acid");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 90;
			base.item.magic = true;
			base.item.mana = 8;
			base.item.rare = 7;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 26;
			base.item.useAnimation = 26;
			base.item.useStyle = 5;
			base.item.knockBack = 2f;
			base.item.value = Item.sellPrice(0, 25, 50, 0);
			base.item.UseSound = SoundID.Item8;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.noMelee = true;
			base.item.shoot = base.mod.ProjectileType("RadioactiveCloud2");
			base.item.shootSpeed = 0f;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.shoot = base.mod.ProjectileType("RadioactiveCloud");
			}
			else
			{
				base.item.shoot = base.mod.ProjectileType("RadioactiveCloud2");
			}
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld;
			return true;
		}
	}
}
