using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Configuration;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using Moq;
using System.Threading.Tasks;

namespace Utilities.Test
{
    [TestClass]
    public class FileUploadHelperTest
    {
        FileStream _stream;

        [TestInitialize]
        public void SetUp()
        {
            //string UploadFile = ""; // ConfigurationManager.AppSettings["File"];
            //_stream = new FileStream(string.Format(
            //                UploadFile,
            //                AppDomain.CurrentDomain.BaseDirectory),
            //             FileMode.Open);
        }

        [TestMethod]
        public async Task TestUploadImageToAzureStorage_Successfully()
        {
            var postedFile = new Mock<HttpPostedFileBase>();
            //var httpRequest = new Mock<HttpRequest>();
            //httpRequest.Setup(x => x.Files).Returns(new File());

            using (var stream = new MemoryStream())
            using (var bmp = new Bitmap(24, 24))
            {
                var graphics = Graphics.FromImage(bmp);
                graphics.FillRectangle(Brushes.Black, 0, 0, 24, 24);
                bmp.Save(stream, ImageFormat.Jpeg);

                postedFile.Setup(pf => pf.InputStream).Returns(stream);
                postedFile.Setup(pf => pf.ContentLength).Returns(576);
                postedFile.Setup(pf => pf.ContentType).Returns("image/bmp");
                postedFile.Setup(pf => pf.FileName).Returns("image.bmp");

                // Assert something with postedFile here   
            }

            FileUploadHelper fileUploadHelper = new FileUploadHelper();
            await fileUploadHelper.UploadImageToAzureStorage(postedFile.Object);

            #region Mock HttpPostedFileBase
            /*
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var files = new Mock<HttpFileCollectionBase>();
            var file = new Mock<HttpPostedFileBase>();
            context.Setup(x => x.Request).Returns(request.Object);

            files.Setup(x => x.Count).Returns(1);

            // The required properties from the Controller side
            file.Setup(x => x.InputStream).Returns(_stream);
            file.Setup(x => x.ContentLength).Returns((int)_stream.Length);
            file.Setup(x => x.FileName).Returns(_stream.Name);

            files.Setup(x => x.Get(0).InputStream).Returns(file.Object.InputStream);
            request.Setup(x => x.Files).Returns(files.Object);
            request.Setup(x => x.Files[0]).Returns(file.Object);

            //_controller.ControllerContext = new ControllerContext(
            //                         context.Object, new RouteData(), _controller);
            */
            #endregion
        }
    }
}