using System;
using System.Linq;
using OpenCvSharp;

namespace q3
{
    class LabelingProcess
    {
        static void Main(string[] args)
        {
            try
            {
                /////画像の作成
                ////////////////////////////////////////////////////////////////////////////////////////////////
                //開く
                Mat src = new Mat(@"C:\WorkPRG\ProcessingReport2\q1\data\pic1.jpg");

                //グレースケール変換
                Cv2.CvtColor(src, src, ColorConversionCodes.BGRA2GRAY);

                //2値化
                Cv2.Threshold(src, src, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);

                /////ラベリング処理
                ////////////////////////////////////////////////////////////////////////////////////////////////

                var LabelImg = new MatOfInt();
                var stats = new MatOfInt();
                var centroids = new MatOfDouble();

                // ※ 8連結
                int nLab = Cv2.ConnectedComponentsWithStats(~src, LabelImg, stats, centroids);

                /////面積値の出力
                ////////////////////////////////////////////////////////////////////////////////////////////////
                var statsIndexer = stats.GetGenericIndexer<int>();

                Console.WriteLine("S(   x,   y ) = ???");
                Enumerable.Range(1, nLab - 1).ToList().ForEach(i =>
                {
                    Console.WriteLine("S( {0,3}, {1,3} ) = {2}",
                        statsIndexer[i, 0], statsIndexer[i, 1], statsIndexer[i, 4]);

                });

                /////画像の表示
                ////////////////////////////////////////////////////////////////////////////////////////////////
                using (new Window("2値化画像", src))
                {
                    Cv2.WaitKey();
                }

            }

            /////例外処理。
            ////////////////////////////////////////////////////////////////////////////////////////////////
            catch (System.IO.FileNotFoundException e)
            {
                Console.Error.WriteLine("ERROR : ファイルが開けませんでした。");
                Console.Error.WriteLine(e);
            }
        }
    }
}
