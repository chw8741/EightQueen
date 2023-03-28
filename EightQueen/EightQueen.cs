using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eightQueen
{
    public class EightQueen
    {
        const int Size = 8;
        public HashSet<int> PosList { get; set; }
        public List<int> QueenPos { get; set; }
        public EightQueen()
        {
            PosList = new HashSet<int>(
                    Enumerable.Range(1, Size)
                        .SelectMany(x => Enumerable.Range(1, Size)
                        .Select(y => x * 10 + y))
                    );
            QueenPos = new List<int>();
        }

        public EightQueen(EightQueen map, int pos)
        {
            PosList = new HashSet<int>(map.PosList.Except(new int[] { pos }));//排除放置皇后座標
            QueenPos = new List<int>(map.QueenPos.Concat(new int[] { pos }));//加入放置皇后座標
            var xShift = pos / 10;
            var yShift = pos % 10;
            Action<int, int> removePos = (px, py) =>
            {
                //超出範圍時忽略
                if (px < 1 || px > Size || py < 1 || py > Size) return;
                var v = px * 10 + py;
                if (PosList.Contains(v)) PosList.Remove(v);//移除座標
            };
            for (var x = 1; x <= Size; x++)
            {
                for (var y = 1; y <= Size; y++)
                {
                    removePos(x, yShift); //水平線
                    removePos(xShift, y); //垂直線
                }
                var tx = x + (xShift - yShift);
                removePos(tx, x); //左上右下斜線
                tx = xShift + yShift - x;
                removePos(tx, x); //右上左下斜線
            }
        }

        public bool notEnough => PosList.Count < Size - QueenPos.Count;

        public bool enough => Size == QueenPos.Count;

        public void Print()
        {
            for (var y = 1; y <= Size; y++)
            {
                for (var x = 1; x <= Size; x++)
                {
                    var pos = x * 10 + y;
                    var output="";
                    if (QueenPos.Contains(pos))
                    {
                        output = "Q ";
                    }
                    else
                    {
                        output = ". ";
                    }
                    Console.Write(output);
                }
                Console.WriteLine();
            }
        }
    }
}
