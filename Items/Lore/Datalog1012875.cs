using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class Datalog1012875 : ModItem
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
			base.DisplayName.SetDefault("Data Log #1012875");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'About damn time. I've finally found a green planet. But am I happy about this?]\n[c/aee6f3:Not really. I thought this would make me feel something, but it's hopeless.]\n[c/aee6f3:I can't remember the last time I felt happy, the memory of my home is starting to get foggy.]\n[c/aee6f3:But anyway, it looks like this planet has intelligent life, so I'll land and see if they're friendly.]\n[c/aee6f3:If they're not, I'll just shoot them. Simple.']");
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
