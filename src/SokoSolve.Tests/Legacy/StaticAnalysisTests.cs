﻿using SokoSolve.Core;
using SokoSolve.Core.Analytics;
using VectorInt;
using Xunit;

namespace SokoSolve.Tests.Legacy
{
    public class StaticAnalysisTests
    {
        [Xunit.Fact]
        public void CornerMap()
        {
            // Init
            var report = new TestReport();

            var stat = new StaticAnalysisMaps(Puzzle.Builder.DefaultTestPuzzle());

            Assert.NotNull(stat.CornerMap);
            report.WriteLine(stat.CornerMap);

            Assert.Equal(new TestReport(@"...........
....X......
...X....XX.
..X........
.X....X....
...........
....X.X..X.
........X..
..X....X...
..X...X....
..........."), report);
        }

        [Xunit.Fact]
        public void DoorMap()
        {
            // Init
            var report = new TestReport();

            var stat = new StaticAnalysisMaps(Puzzle.Builder.DefaultTestPuzzle());

            Assert.NotNull(stat.DoorMap);
            report.WriteLine(stat.DoorMap);

            Assert.Equal(new TestReport(@"...........
...........
...........
.......X...
...........
...........
.......X...
...X..X....
...........
...........
..........."), report);
        }


        [Xunit.Fact]
        public void Normalise()
        {
            var report = new TestReport();
            var p = Puzzle.Builder.DefaultTestPuzzle();

            Assert.True(p.Definition.Wall.Equals(p[0, 0]));
            Assert.True(p.Definition.Void == p[1, 1]);
            Assert.True(p.Definition.Player == p[4, 4]);
            Assert.True(p[4, 4].IsPlayer);
            
            Assert.Equal(new VectorInt2(4, 4), p.Player.Position);
            
            var norm = StaticAnalysis.Normalise(p);

            report.WriteLine(norm.ToString());
            Assert.Equal(new TestReport(@"###########
####.######
###..###..#
##.X......#
#...PX.#..#
###.X###..#
###..#OO..#
###.##O#.##
##......###
##.....####
###########
"), report);
        }

        [Xunit.Fact]
        public void Recesses()
        {
            // Init
            var report = new TestReport();

            var stat = new StaticAnalysisMaps(Puzzle.Builder.DefaultTestPuzzle());
            
            Assert.NotNull(stat.RecessMaps);
            foreach (var recess in stat.RecessMaps) report.WriteLine(recess);

            Assert.Equal(new TestReport(@"(8,2) => (9,2)
...........
...........
........XX.
...........
...........
...........
...........
...........
...........
...........
...........

(2,9) => (6,9)
...........
...........
...........
...........
...........
...........
...........
...........
...........
..XXXXX....
...........

(2,8) => (2,9)
...........
...........
...........
...........
...........
...........
...........
...........
..X........
..X........
...........

(9,2) => (9,6)
...........
...........
.........X.
.........X.
.........X.
.........X.
.........X.
...........
...........
...........
..........."), report);
        }


        [Xunit.Fact]
        public void SideMap()
        {
            // Init
            var report = new TestReport();

            
            var stat = new StaticAnalysisMaps(Puzzle.Builder.DefaultTestPuzzle());

            Assert.NotNull(stat.SideMap);
            report.WriteLine(stat.SideMap);

            Assert.Equal(new TestReport(@"...........
...........
...........
......X..X.
.........X.
.........X.
...X.......
...........
...........
...XXX.....
..........."), report);
        }


        [Xunit.Fact]
        public void Walls()
        {
            // Init
            var report = new TestReport();

            var stat = new StaticAnalysisMaps(Puzzle.Builder.DefaultTestPuzzle());

            Assert.NotNull(stat.IndividualWalls);
            foreach (var wall in stat.IndividualWalls) report.WriteLine(wall);

            Assert.Equal(new TestReport(@"(8,2) => (9,2)
...........
...........
........XX.
...........
...........
...........
...........
...........
...........
...........
...........

(3,6) => (4,6)
...........
...........
...........
...........
...........
...........
...XX......
...........
...........
...........
...........

(2,9) => (6,9)
...........
...........
...........
...........
...........
...........
...........
...........
...........
..XXXXX....
...........

(2,8) => (2,9)
...........
...........
...........
...........
...........
...........
...........
...........
..X........
..X........
...........

(6,3) => (6,4)
...........
...........
...........
......X....
......X....
...........
...........
...........
...........
...........
...........

(9,2) => (9,6)
...........
...........
.........X.
.........X.
.........X.
.........X.
.........X.
...........
...........
...........
...........
"), report);
        }

        [Xunit.Fact]
        public void Walls_Box_3v3()
        {
            // Init
            var report = new TestReport();

            var puz = Puzzle.Builder.FromLines(new[]
            {
                "#####",
                "#...#",
                "#...#",
                "#...#",
                "#####"
            });


            var stat = new StaticAnalysisMaps(puz);

            Assert.NotNull(stat.IndividualWalls);


            foreach (var wall in stat.IndividualWalls) report.WriteLine(wall);

            Assert.Equal(new TestReport(@"(1,1) => (3,1)
.....
.XXX.
.....
.....
.....

(1,3) => (3,3)
.....
.....
.....
.XXX.
.....

(1,1) => (1,3)
.....
.X...
.X...
.X...
.....

(3,1) => (3,3)
.....
...X.
...X.
...X.
....."), report);
        }


        [Xunit.Fact]
        public void Walls_Box_4v4()
        {
            // Init
            var report = new TestReport();

            var puz = Puzzle.Builder.FromLines(new[]
            {
                "######",
                "#....#",
                "#....#",
                "#....#",
                "#....#",
                "######"
            });


            var stat = new StaticAnalysisMaps(puz);

            Assert.NotNull(stat.IndividualWalls);


            foreach (var wall in stat.IndividualWalls) report.WriteLine(wall);

            Assert.Equal(new TestReport(@"(1,1) => (4,1)
......
.XXXX.
......
......
......
......

(1,4) => (4,4)
......
......
......
......
.XXXX.
......

(1,1) => (1,4)
......
.X....
.X....
.X....
.X....
......

(4,1) => (4,4)
......
....X.
....X.
....X.
....X.
......
"), report);
        }
    }
}