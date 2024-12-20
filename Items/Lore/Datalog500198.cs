using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class Datalog500198 : ModItem
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
			base.DisplayName.SetDefault("Data Log #500198");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'I'm done with Asherah, the aliens there attacked me so I had to make some weaponry.]\n[c/aee6f3:I decided to make a new android, one for military purposes, I've named it the Prototype Silver.]\n[c/aee6f3:Despite its name, it's mainly composed of the spare titanium from Alkonost.]\n[c/aee6f3:I did find a metric ton of coal from Asherah's caverns, so that's nice.]\n[c/aee6f3:Well... Onto the next planet, I just hope THIS one will be lush and green.]\n[c/aee6f3:All the planets I've been to were either frozen or barren.']");
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
