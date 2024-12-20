using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class Datalog182501 : ModItem
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
			base.DisplayName.SetDefault("Data Log #182501");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'Alright. Alkonost. And of course, it's ANOTHER DAMN ICE PLANET!]\n[c/aee6f3:Radius: 6059.58km, Composition: 36.1% titanium, 35.6% iron, 17.5% oxygen,]\n[c/aee6f3:7.4% silicon, 3.4% other metals, trace other elements. High amounts of titanium, huh?]\n[c/aee6f3:That's gonna be useful, Nabu III had barely any titanium.]\n[c/aee6f3:Gravity is 11.13 m/s². A cycle is 32.65 hours, with an axis tilt of 11.58°.]\n[c/aee6f3:Oh god. 100% of the surface is just ice. The atmosphere is toxic, with a pressure of 91.63 kPa.]\n[c/aee6f3:The temperature is -223°C... I don't think even my robotic body can handle that! Oh whatever.']");
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
