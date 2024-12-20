using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class Datalog164550614 : ModItem
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
			base.DisplayName.SetDefault("Data Log #164550614");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'I haven't felt like this in forever.]\n[c/aee6f3:I upgraded my robotic self again, this time with more attack power. Xehito let me test it on him,]\n[c/aee6f3:so we had a fight. The intensity of it was almost exhilarating, lasers firing everywhere,]\n[c/aee6f3:explosions all around, it was generally a fun time. But something tells me he only let me]\n[c/aee6f3:fight him to cheer me up, and I'm sorry Xehito, but that moment didn't last, I still feel empty.']");
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
