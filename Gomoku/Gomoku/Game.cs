using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class Game //重構，遊戲管理
    {
        private PieceType currentPlayer = PieceType.Black;
        private Board board = new Board();
        private PieceType winner = PieceType.None;
        public PieceType Winner { get { return winner; } }

        public bool CanBePlaced(int x, int y)
        {
            return board.CanBePlaced(x, y);
        }
        public Piece PlaceAPiece(int x, int y)
        {
            Piece pieces = board.PlaceAPiece(x, y, currentPlayer);
            
            if (pieces != null)
            {
                // 判斷正在下棋的人是否獲勝
                CheckWinner();

                // 交換選手
                if (currentPlayer == PieceType.Black)
                    currentPlayer = PieceType.White;
                else
                    currentPlayer = PieceType.Black;
                return pieces;
            }
            return null;
        }
        private void CheckWinner()
        {
            int centerX = board.LastPlacedNode.X;
            int centerY = board.LastPlacedNode.Y;

            // 判斷八個不同方向
            for(int xDir = -1; xDir <= 1; xDir++){
                for(int yDir = -1; yDir <= 1; yDir++){

                    // 排除中間的情況
                    if (xDir == 0 && yDir == 0)
                        continue; // 如果上方的條件成立的話，則直接跳過下面的程式

                    //紀錄目前有幾顆相同的旗子
                    int count = 1;
                    while (count < 5)
                    {
                        int targetX = centerX + count * xDir;
                        int targetY = centerY + count * yDir;

                        // 判斷顏色是否相同
                        if (targetX < 0 || targetX >= Board.node_count ||
                            targetY < 0 || targetY >= Board.node_count ||
                            board.GetPieceType(targetX, targetY) != currentPlayer) // 判斷是否超出視窗邊界
                            break;

                        count++;
                    }

                    //判斷是否有五顆棋子
                    if (count == 5)
                        winner = currentPlayer;
                }
            }
        }
    }
}
