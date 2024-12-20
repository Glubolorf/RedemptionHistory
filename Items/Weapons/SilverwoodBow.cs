using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class SilverwoodBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Daerel's Silverwood Bow");
			base.Tooltip.SetDefault("'A bow made of Silverwood'\n20% chance not to consume ammo\nReplaces wooden arrows with Silverwood Arrows\nOnly usable after Eye of Cthulhu has been defeated\n[c/1c4dff:Rare]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 11;
			base.item.ranged = true;
			base.item.width = 30;
			base.item.height = 46;
			base.item.useTime = 14;
			base.item.useAnimation = 14;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 1f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 30f;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 5;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-4f, 0f));
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Utils.NextFloat(Main.rand) >= 0.2f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == 1)
			{
				type = base.mod.ProjectileType("SilverwoodArrowPro");
				base.item.shootSpeed = 30f;
			}
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			return NPC.downedBoss1;
		}
	}
}
