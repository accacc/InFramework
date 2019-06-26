//using System.Text;
//using System.Web;
//using System.Web.Mvc;
//using System;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.Drawing.Drawing2D;
//using System.Drawing.Text;
//using System.IO;
//using IF.Core.Image;
//using IF.Core;

//namespace Framework.Core.Mvc.ActionResults
//{
//    public class CaptchaImageResult : System.Web.Mvc.ActionResult
//    {

//        ICaptchaGenerator captchaGenerator = IocManager.Instance.Resolve<ICaptchaGenerator>();
//        IRandomGenerator randomGenerator = IocManager.Instance.Resolve<IRandomGenerator>();

//        public override void ExecuteResult(ControllerContext context)
//        {

//            var captchaString = randomGenerator.CreateCapcthaString();

//            context.HttpContext.Session["captchastring"] = captchaString.ToUpper();

//            var oOutputBitmap = captchaGenerator.Generate(captchaString);


//            HttpResponseBase response = context.HttpContext.Response;
//            response.ContentType = "image/jpeg";

//            oOutputBitmap.Save(response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
//            oOutputBitmap.Dispose();

//        }

        
//    }
//}

