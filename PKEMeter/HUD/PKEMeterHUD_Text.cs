﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HUDElementsLib;


namespace PKEMeter.HUD {
	public partial class PKEMeterHUD : HUDElement {
		public Rectangle DrawHUDText_If( SpriteBatch sb, Vector2 pos, string text, Color color, int offset ) {
			pos.X += 22;
			pos.Y += 16;
			var area = new Rectangle( (int)pos.X, (int)pos.Y, 42, 12 );
			
			//

			if( text == "" || offset > (text.Length * 8) ) {
				return area;
			}

			//

			text = text.ToUpper();

			for( int i=0; i<text.Length; i++ ) {
				int charOffset = (i * 8) - offset;

				if( !this.DrawHUDTextCharacter(sb, pos, text[i], color, charOffset) ) {
					break;
				}
			}

			return area;
		}


		public bool DrawHUDTextCharacter( SpriteBatch sb, Vector2 pos, char textChar, Color color, int offset ) {
			if( offset <= -8 || textChar == ' ' ) {
				return true;
			}

			if( offset > 38 ) {
				return false;
			}

			bool isNumber = textChar >= '0' && textChar <= '9';
			int x = isNumber
				? textChar - '0'
				: textChar - 'A';
			x *= 8;
			int y = isNumber ? 12 : 0;

			switch( textChar ) {
			case '.':
				x = 21 * 8;
				y = 12;
				break;
			case '?':
				x = 22 * 8;
				y = 12;
				break;
			case '!':
				x = 23 * 8;
				y = 12;
				break;
			case '+':
				x = 24 * 8;
				y = 12;
				break;
			case '-':
				x = 25 * 8;
				y = 12;
				break;
			}

			Rectangle srcFrame = new Rectangle( x, y, 8, 12 );
			if( offset < 0 ) {
				srcFrame.X -= offset;
				srcFrame.Width += offset;
			} else {
				pos.X += offset;
			}

			sb.Draw(
				texture: this.MinFont,
				position: pos,
				sourceRectangle: srcFrame,
				color: color
			);
			return true;
		}
	}
}
