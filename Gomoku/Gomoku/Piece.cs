using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; //picturebox所需的函式庫
using System.Drawing; //調整色彩color所需的函示庫

namespace Gomoku
{
    abstract class Piece:PictureBox //設定成虛擬物件，讓後面需要修改的人不要輕易更改到Piece物件
    {
        private static readonly int IMAGE_WIDTH = 50; //圖片預設大小，點選圖片右鍵內容可以查看圖片大小
        public Piece(int x, int y)
        {
            this.BackColor = Color.Transparent; //把圖片的背景設成透明
            this.Location = new Point(x - IMAGE_WIDTH / 2, y - IMAGE_WIDTH / 2); //減掉預設長寬的一半使得棋子圖片的位置顯示在鼠標所點的位置上
            this.Size = new Size(IMAGE_WIDTH, IMAGE_WIDTH); //把圖片大小設成50,50
        }
        public abstract PieceType GetPieceType();
    }
}
