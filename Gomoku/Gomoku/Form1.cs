using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    public partial class Form1 : Form
    {
        //private bool isBlack = true;
        private Game game = new Game();
        public Form1()
        {
            InitializeComponent();
            //this.Controls.Add(new BlackPiece(75, 75));
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)//e所代表的是當此事件發生時，相關的一些參數
        {
            Piece pieces = game.PlaceAPiece(e.X, e.Y);
            if (pieces != null)
            {
                this.Controls.Add(pieces);

                // 判斷是否有人獲勝
                if (game.Winner == PieceType.Black)
                    MessageBox.Show("黑棋獲勝!!");
                else if (game.Winner == PieceType.White)
                    MessageBox.Show("白棋獲勝!!");
            }
            //if (isBlack)
            //{
            //    Piece pieces =  board.PlaceAPiece(e.X, e.Y, PieceType.Black);
            //    if (pieces != null)
            //    {
            //        this.Controls.Add(pieces);
            //        isBlack = false;
            //    }
            //    //this.Controls.Add(new BlackPiece(e.X, e.Y));//一開始設定的時候，棋子並不會剛好顯示在你所點的位置上，因為picturebox位置的設定是以你的滑鼠所點的位置為左上角的位置
            //} else
            //{
            //    Piece pieces = board.PlaceAPiece(e.X, e.Y, PieceType.White);
            //    if (pieces != null)
            //    {
            //        this.Controls.Add(pieces);
            //        isBlack = true;
            //    }
            //    //this.Controls.Add(new WhitePiece(e.X, e.Y));
            //}
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(game.CanBePlaced(e.X, e.Y)) //如果鼠標所移到的位置可放置棋子的話，則鼠標變手的鼠標
            {
                this.Cursor = Cursors.Hand;
            }else //如果不行的話，則鼠標變回預設鼠標
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
