using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace Redemption.Items.DruidDamageClass
{
	public class MartianShieldGenerator : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Portable Martian Shield Generator");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nConstructs a Martian Shield Generator at your cursor point\nThe generator emits a forcefield that reflects any projectiles\nCan only place one at a time");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 2));
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 26;
			base.item.height = 30;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.mana = 100;
			base.item.value = Item.buyPrice(0, 25, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/WorldTree1");
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("WorldTree4");
			base.item.shootSpeed = 0f;
			base.item.buffType = base.mod.BuffType("WorldStaveCooldownDebuff");
			base.item.buffTime = 1000;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(base.mod.BuffType("WorldStaveCooldownDebuff"));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld;
			return true;
		}
	}
}
