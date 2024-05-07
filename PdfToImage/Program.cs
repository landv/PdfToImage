using PdfiumViewer;

using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace PdfToImage
{
    class Program
	{

        static void Main(string[] args)
		{

            Console.Title = Guid.NewGuid().ToString();
            ConsoleHelper.hideConsole();

            try
			{
				using (var document = PdfDocument.Load(args[0]))
				{
					var pageCount = document.PageCount;

					for (int i = 0; i < pageCount; i++)
					{
						//var dpi = 300;
						// 图片质量 150就够了
						var dpi = 150;
						
						using (var image = document.Render(i, dpi, dpi, PdfRenderFlags.CorrectFromDpi))
						{
							var encoder = ImageCodecInfo.GetImageEncoders()
								.First(c => c.FormatID == ImageFormat.Jpeg.Guid);
							var encParams = new EncoderParameters(1);
							encParams.Param[0] = new EncoderParameter(
                                Encoder.Quality, 100L);

							image.Save(Path.GetFileNameWithoutExtension(args[0]) + i + ".png", encoder, encParams);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}
	}
}
