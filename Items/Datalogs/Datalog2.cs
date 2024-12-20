using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class Datalog2 : ModItem
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
			base.DisplayName.SetDefault("Data Log #2");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'If I ever forget why I'm doing this, I will write it down here.]\n[c/aee6f3:When a Great Era ends, all life dies and the world resets. I am from a previous era]\n[c/aee6f3:and successfully escaped into space, as the reset won't affect me outside of the world.]\n[c/aee6f3:A reset apparently takes a million years, so I must travel through space during that time period,]\n[c/aee6f3:and with luck, I should come back here a million years from now, and see the new world in all it's beauty.]\n[c/aee6f3:As far as I know, I am the sole survivor, and the first living thing to ever escape.]\n[c/aee6f3:I transferred my human mind into a robotic body so I can save an infinite number of memories with ease,]\n[c/aee6f3:and I won't need to worry about thirst, hunger, or sleep.']");
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
