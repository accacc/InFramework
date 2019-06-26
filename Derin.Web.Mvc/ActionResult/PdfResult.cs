//using System.Text;
//using System.Web;
//using System.Web.Mvc;
//using Winnovative.WnvHtmlConvert;
//using System;
//using Unisec.Core.Document;

//namespace Derin.Core.Mvc.ActionResults
//{
//    public class PdfResult : DocumentResult
//    {
//        private int PageWidth { get; set; }
//        private PDFPageOrientation Orientation { get; set; }

//        public PdfResult(string viewName, object model, string filename)
//            : base(viewName, model, filename)
//        {
//            Orientation = PDFPageOrientation.Portrait;
//            PageWidth = 1024;
//        }

//        public PdfResult(string viewName, object model, string filename, PdfDocumentPageOrientation orientation)
//            : this(viewName, model, filename)
//        {
//            Orientation = (PDFPageOrientation)((int)orientation);
//        }
//        public PdfResult(string viewName, object model, string filename, PdfDocumentPageOrientation orientation, int pageWidth)
//            : this(viewName, model, filename, orientation)
//        {
//            PageWidth = pageWidth;
//        }
//        public PdfResult(string viewName, object model, string filename, int pageWidth)
//            : this(viewName, model, filename)
//        {
//            PageWidth = pageWidth;
//        }
//        public override void ExecuteResult(ControllerContext context)
//        {
//            base.ExecuteResult(context);
//        }

//        protected override void WriteToFile(object content)
//        {
//            HttpContext context = HttpContext.Current;
//            context.Response.ContentEncoding = Encoding.UTF8;
//            context.Response.Clear();
//            context.Response.HeaderEncoding = Encoding.UTF8;
//            context.Response.AddHeader("content-disposition", String.Format("attachment;filename=\"{0}\"", filename));
//            context.Response.Charset = "";
//            context.Response.AddHeader("Content-Type", "application/pdf");
//            context.Response.BinaryWrite((byte[])content);
//            context.Response.End();
//        }

//        protected override object GetContent(string content)
//        {
//            PdfConverter pdfConverter = new PdfConverter { LicenseKey = "Q2hzY3Jjc2N3bXNjcHJtcnFtenp6eg==" };
//            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
//            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Best;
//            pdfConverter.PdfDocumentOptions.PdfPageOrientation = Orientation;
//            pdfConverter.PdfDocumentOptions.ShowHeader = false;
//            pdfConverter.PdfDocumentOptions.ShowFooter = false;
//            pdfConverter.PageWidth = PageWidth;
//            //pdfConverter.PdfStandardSubset = PdfStandardSubset.Full;
//            //pdfConverter.PdfDocumentOptions.LeftMargin = 0;
//            //pdfConverter.PdfDocumentOptions.RightMargin = 0;
//            //pdfConverter.PdfDocumentOptions.TopMargin = 0;
//            //pdfConverter.PdfDocumentOptions.BottomMargin = 0;
//            //pdfConverter.PdfDocumentOptions.FitWidth = true;
//            //pdfConverter.PdfDocumentOptions.EmbedFonts = true;
//            //pdfConverter.PdfDocumentOptions.JpegCompressionEnabled = true;
//            //pdfConverter.ScriptsEnabled = pdfConverter.ScriptsEnabledInImage = true;
//            return pdfConverter.GetPdfBytesFromHtmlString(content, HttpContext.Current.Request.UrlReferrer.AbsoluteUri);
//        }

//    }
//}
