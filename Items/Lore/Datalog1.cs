using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class Datalog1 : ModItem
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
			base.DisplayName.SetDefault("Data Log #1");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'I have successfully reached the outer atmosphere and escaped my world's Reset.]\n[c/aee6f3:I realise there is no going back now, a normal human's lifespan sounds miniscule]\n[c/aee6f3:compared to the time I must travel through space, but it is my goal to withstand this infinite voyage.]\n[c/aee6f3:I just hope it'll all be worth it when I return to the new world. I've decided to write these]\n[c/aee6f3:logs every day until I return, and preserve my encounters for when I get back.]\n[c/aee6f3:But that's a million years from now. I just hope I won't regret it.']");
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
