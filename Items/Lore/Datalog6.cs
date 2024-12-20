using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class Datalog6 : ModItem
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
			base.DisplayName.SetDefault("Data Log #6");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'My worst fear is coming true.]\n[c/aee6f3:I have a strong feeling of tiredness and thirst, and my hunger has begun to take hold.]\n[c/aee6f3:It's only been 6 days damn it! Roughly 359,000,000 days to go...]\n[c/aee6f3:I've tried all I can, but these painful feelings can't go away.]\n[c/aee6f3:The human mind is more complicated than I imagined, and combined with all this technical stuff]\n[c/aee6f3:only makes it harder for me to look into it!]\n[c/aee6f3:If only I had more time back then! I could've looked through this body's code and easily discovered the error!]\n[c/aee6f3:Guess I'll just have to deal with it.']");
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
