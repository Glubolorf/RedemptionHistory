using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class Datalog919 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Lore/Datalog1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Data Log #919");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'Alright. I have constructed a temporary base on Nabu III.]\n[c/aee6f3:The amount of iron and sulfur here has come in handy.]\n[c/aee6f3:I mean, if I was a human I'd be dead with the lack of proper air.]\n[c/aee6f3:My blueprint for a country sized spaceship is also finished, now begins the long construction.]\n[c/aee6f3:The design will be a crescent moon shape, not sure why...]\n[c/aee6f3:Probably because I used to look up at the moon of my world a lot]\n[c/aee6f3:when I was human... I wish I still was.']");
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
