using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class Datalog466476 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Datalogs/Datalog1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Data Log #466476");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'I have named this planet Asherah, it appears to be iron/silicate-based.]\n[c/aee6f3:A big radius of 8845.27 km, 40.8% iron, 32.9% oxygen, 15.6% silicon, 4.2% carbon, 2.9% magnesium...]\n[c/aee6f3:Quite strong gravity, 34.70 hour cycle, an axis tilt of 53.09°...]\n[c/aee6f3:Only 1% is water, the rest looks like... boring stone and sand... Damn.]\n[c/aee6f3:Oh! The scanner has found life! Microbes, fungi, sentient animals... What is that?]\n[c/aee6f3:Well I've found life here, only problem is they look ugly as hell.]\n[c/aee6f3:2.01 million of these intelligent creatures have been scanned, so they've been around for a while.']");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 2));
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 30;
			base.item.maxStack = 1;
			base.item.value = 0;
			base.item.rare = 9;
		}
	}
}
