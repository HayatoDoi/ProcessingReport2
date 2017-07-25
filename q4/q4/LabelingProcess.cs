using System;
using System.Linq;
using OpenCvSharp;

namespace q4
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
                Mat src = new Mat(@"C:\file\path");

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

                /////ラベリング結果の描画色を決定
                ////////////////////////////////////////////////////////////////////////////////////////////////

                Scalar[] colors = Enumerable.Range(1, nLab).Select(_ =>
                    Scalar.RandomColor()).ToArray();
                colors[0] = Scalar.White;

                /////ラベリング結果の描画
                ////////////////////////////////////////////////////////////////////////////////////////////////

                Mat dst = new Mat(src.Height, src.Width, MatType.CV_8UC3);

                var labelIndexer = LabelImg.GetGenericIndexer<int>();
                var dstIndexer = dst.GetGenericIndexer<Vec3b>();

                Enumerable.Range(0, dst.Height).ToList().ForEach(i =>
                {
                    Enumerable.Range(0, dst.Width).ToList().ForEach(j =>
                    {
                        dstIndexer[i, j] = colors[labelIndexer[i, j]].ToVec3b();
                    });
                });

                /////画像の表示
                ////////////////////////////////////////////////////////////////////////////////////////////////
                using (new Window("ラベリング画像", dst))
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
