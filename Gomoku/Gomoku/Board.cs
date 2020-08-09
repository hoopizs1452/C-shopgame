using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Gomoku
{
    class Board
    {
        private static readonly int offset = 75; //設定視窗邊框至棋盤邊框的長度
        private static readonly int node_radius = 10; //設定棋子的寬度範圍
        private static readonly int node_distance = 75; //設定棋盤線與線的距離
        private static readonly Point no_match_node = new Point(-1, -1); //設定鼠標所至位置如果不再範圍內則回傳(-1,-1)
        public static readonly int node_count = 9;
        private Piece[,] pieces = new Piece[node_count, node_count];

        private Point lastPlacedNode = no_match_node; //設定最後一顆棋子的變數
        public Point LastPlacedNode { get { return lastPlacedNode; } } //這個變數只能拿不能設定

        public PieceType GetPieceType(int nodeIdX, int nodeIdY) //判斷是黑棋還是白棋
        {
            if (pieces[nodeIdX, nodeIdY] == null)
                return PieceType.None;
            else
                return pieces[nodeIdX, nodeIdY].GetPieceType();
        }

        public bool CanBePlaced(int x, int y)
        {
            //TODO: 找出最近的節點(Node)
            Point nodeId = FindTheClosetNode(x, y);
            //TODO: 如果沒有的話回傳false
            if (nodeId == no_match_node)
                return false;
            //TODO: 如果有的話，檢查是否已經有棋子存在
            if (pieces[nodeId.X, nodeId.Y] != null)
                return false;

            return true;
        }

        public Piece PlaceAPiece(int x, int y, PieceType type)
        {
            //TODO: 找出最近的節點(Node)
            Point nodeId = FindTheClosetNode(x, y);
            //TODO: 如果沒有的話回傳false
            if (nodeId == no_match_node)
                return null; //null為reference type
            //TODO: 如果有的話，檢查是否已經有棋子存在
            if (pieces[nodeId.X, nodeId.Y] != null)
                return null;
            //TODO: 根據type產生相對應的棋子
            Point formPos = ConvertToFormPosition(nodeId);
            if (type == PieceType.Black)
                pieces[nodeId.X, nodeId.Y] = new BlackPiece(formPos.X, formPos.Y); //絕對不能使用nodeId的x,y坐標，因為nodeId的坐標視節點的坐標，所以最大也才9而已，如果使用的話棋子顯示的位置都會集中在左上角
            else if (type == PieceType.White)
                pieces[nodeId.X, nodeId.Y] = new WhitePiece(formPos.X, formPos.Y); //但如果使用x和y的話位置又會不夠精確有些點可能會稍稍偏移

            // 紀錄棋子最後位置
            lastPlacedNode = nodeId;

            return pieces[nodeId.X, nodeId.Y];
        }

        private Point ConvertToFormPosition(Point nodeId) //計算其盤上的節點在視窗中實際的位置
        {
            Point formPosition = new Point();
            formPosition.X = nodeId.X * node_distance + offset;
            formPosition.Y = nodeId.Y * node_distance + offset;
            return formPosition;
        }

        private Point FindTheClosetNode(int x, int y)
        {
            int nodeIdX = FindTheClosetNode(x); //判斷x座標的位置
            if (nodeIdX == -1 || nodeIdX >= node_count)
                return no_match_node;

            int nodeIdY = FindTheClosetNode(y);//判斷y座標的位置
            if (nodeIdY == -1 || nodeIdX >= node_count)
                return no_match_node;

            return new Point(nodeIdX, nodeIdY);
        }

        private int FindTheClosetNode(int pos)
        {
            if (pos < offset - node_radius) //如果座標落在棋盤外的話直接回傳-1
                return -1;

            pos -= offset; //坐標的質-視窗邊至棋盤邊的長度等於我們所需的長度
            int quotient = pos / node_distance; //商數 => 左邊點的編號
            int remainder = pos % node_distance; //餘數 => 與左邊點的距離

            //因為座標是從左邊開始計算，所以
            if (remainder <= node_radius) //如果與左邊點的距離小於等於棋子預設的寬度
                return quotient; //則回傳左邊點的編號
            else if (remainder >= node_distance - node_radius) //如果與左邊的點的距離大於等於棋盤線與線之距離
                return quotient + 1; //則回傳左邊點的編號+1，及右邊一個點的編號
            else
                return -1; //如果都不是的話，則回傳-1
        }
    }
}
