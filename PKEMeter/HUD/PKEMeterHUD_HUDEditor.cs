using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;


namespace PKEMeter.HUD {
	partial class PKEMeterHUD : ILoadable {
		private Vector2? BaseDragOffset = null;
		private Vector2 PreviousDragMousePos = default;



		////////////////

		private bool RunHUDEditorIf( out bool isHovering ) {
			Vector2 basePos = PKEMeterHUD.GetHUDPosition();
			var area = new Rectangle(
				(int)basePos.X,
				(int)basePos.Y,
				this.MeterBody.Width,
				this.MeterBody.Height
			);

			isHovering = area.Contains( Main.MouseScreen.ToPoint() );
			bool isAlt = Main.keyState.IsKeyDown( Keys.LeftAlt )
				|| Main.keyState.IsKeyDown( Keys.RightAlt );

			if( Main.mouseLeft && isAlt ) {
				if( this.BaseDragOffset.HasValue || isHovering ) {
					this.RunHUDEditor_Drag( basePos );
				}
			} else {
				this.BaseDragOffset = null;
			}

			return this.BaseDragOffset.HasValue;
		}


		private void RunHUDEditor_Drag( Vector2 basePos ) {
			if( !this.BaseDragOffset.HasValue ) {
				this.BaseDragOffset = basePos - Main.MouseScreen;
				this.PreviousDragMousePos = Main.MouseScreen;

				return;
			}
			
			Vector2 movedSince = Main.MouseScreen - this.PreviousDragMousePos;
			this.PreviousDragMousePos = Main.MouseScreen;

			var myplayer = Main.LocalPlayer.GetModPlayer<PKEMeterPlayer>();
			myplayer.PKEDisplayOffset += movedSince;
		}
	}
}
