using CVDynamicGenerator.WebApi.Entities.DTO;
using CVDynamicGenerator.WebApi.Utilities;
using iText.Forms;
using iText.Forms.Fields;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Net;
using iText.IO.Image;
using System.Text;
using iText.Kernel.Utils;

namespace CVDynamicGenerator.WebApi.Business.ApplicationService
{
    public class GService : IGService
    {
        private readonly string pathTemplate;
        private readonly string ttfLight;

        private readonly string ttfMedium;

        private readonly string ttfRegular;

        private readonly PdfFont light;
        private static PdfFont regular;
        private static PdfFont medium;

        private static float headerName = 34;
        private static float subHeader = 22;
        private static float titleSection = 20;
        private static float titleHistory = 14;
        private static readonly float subTitle = 12;
        private static float body = 11;

        public GService()
        {
            pathTemplate = System.IO.Path.GetFullPath("App_Data/pdf/Manual.pdf");
            ttfLight = System.IO.Path.GetFullPath("App_SetUp/Oswald-Light.ttf");
            ttfMedium = System.IO.Path.GetFullPath("App_SetUp/Oswald-Medium.ttf");
            ttfRegular = System.IO.Path.GetFullPath("App_SetUp/Oswald-Regular.ttf");
            light = PdfFontFactory.CreateFont(ttfLight);
            regular = PdfFontFactory.CreateFont(ttfRegular);
            medium = PdfFontFactory.CreateFont(ttfMedium);
        }

        public async Task<DefaultResponse> CVGenerator(DefaultRequest request)
        {
            MemoryStream ms = new MemoryStream();
            var reader = new PdfReader(pathTemplate);
            PdfWriter writer = new PdfWriter(ms);

            PdfDocument pdf = new PdfDocument(reader, writer);
            Document document = new Document(pdf);
            Document document2 = new Document(pdf);

            document2.SetMargins(50, 50, 50, 430);
            Paragraph saltoDeLinea = new Paragraph(" ");

            LineSeparator ls = new LineSeparator(new SolidLine()).SetWidth(340);

            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);

            document.Add(nameHeader(request.name));

            document.Add(headerSubTitle(request.profession));

            document.Add(sectionTitle("About me"));
            document.Add(ls);
            document.Add(bodyInfo(request.description));

            document.Add(sectionTitle("Work experience"));
            document.Add(ls);
            List<job> orderedList = request.jobs.OrderByDescending(o => o.endTime).ToList();
            foreach (job jobs in orderedList)
            {
                string time = FormatDateTime.MonthYear(jobs.startTime) + " - " + (jobs.isCurrently ? "Present" : FormatDateTime.MonthYear(jobs.endTime));
                document.Add(startEnd(jobs.position + " | " + time));
                document.Add(historyTitle(jobs.enterprise, jobs.location));
                foreach (string activities in jobs.activities)
                {
                    document.Add(bodyInfo("\u2022 " + activities));
                }
            }

            document.Add(sectionTitle("Education"));
            document.Add(ls);

            List<education> orderedEduList = request.educations.OrderByDescending(o => o.endTime).ToList();
            foreach (education educations in orderedEduList)
            {
                string time = FormatDateTime.MonthYear(educations.startTime) + " - " + FormatDateTime.MonthYear(educations.endTime);
                document.Add(startEnd(educations.courseName + " | " + time));
                document.Add(historyTitle(educations.institution, educations.location));

                foreach (string activities in educations.activities)
                {
                    document.Add(bodyInfo("\u2022 " + activities));
                }
            }
            SolidLine whiteLine = new SolidLine(2f);
            whiteLine.SetColor(ColorConstants.WHITE);
            LineSeparator ls2 = new LineSeparator(whiteLine).SetWidth(130);

            document2.Add(sectionTitle("Contact").SetFontColor(ColorConstants.WHITE));
            document2.Add(ls2);
            document2.Add(iconText("https://img.icons8.com/ios-filled/100/FFFFFF/google-pixel6.png", "", request.cel).SetFontColor(ColorConstants.WHITE));
            document2.Add(iconText("https://img.icons8.com/material-rounded/96/FFFFFF/place-marker.png", "", request.direction).SetFontColor(ColorConstants.WHITE));
            document2.Add(iconText("https://img.icons8.com/material-rounded/96/FFFFFF/mail.png", "", request.email).SetFontColor(ColorConstants.WHITE));

            foreach (link links in request.links)
            {
                Link link = new Link(links.name,
                PdfAction.CreateURI(links.liga));
                document2.Add(new Paragraph().Add(iconImage("https://img.icons8.com/ios-filled/100/FFFFFF/chain.png", "")).Add(" ").Add(link.SetFontSize(body).SetFont(medium).SetFontColor(ColorConstants.WHITE)).SetFixedLeading(body));
            }

            document2.Add(sectionTitle("Skills").SetFontColor(ColorConstants.WHITE));
            document2.Add(ls2);

            foreach (skill skills in request.skills)
            {
                document2.Add(bodyInfo(skills.name).SetFontColor(ColorConstants.WHITE));
                document2.Add(star(skills.level, 5));
            }

            document2.Add(sectionTitle("Languages").SetFontColor(ColorConstants.WHITE));
            document2.Add(ls2);

            foreach (language languages in request.language)
            {
                document2.Add(bodyInfo(languages.lang).Add(" " + languages.level).SetFontColor(ColorConstants.WHITE));
                document2.Add(star(languages.l, 6));
            }

            document2.Add(sectionTitle("Strengths").SetFontColor(ColorConstants.WHITE));
            document2.Add(ls2);

            foreach (string strength in request.strength)
            {
                document2.Add(bodyInfo("\u2022 " + strength).SetFontColor(ColorConstants.WHITE));
            }

            document2.Add(sectionTitle("Hobbies").SetFontColor(ColorConstants.WHITE));
            document2.Add(ls2);

            foreach (string hobby in request.hobby)
            {
                document2.Add(bodyInfo("\u2022 " + hobby).SetFontColor(ColorConstants.WHITE));
            }

            document2.Add(sectionTitle("Certifications").SetFontColor(ColorConstants.WHITE));
            document2.Add(ls2);

            foreach (certification certification in request.cert)
            {
                document2.Add(bodyInfo(certification.name).Add(", " + FormatDateTime.MonthYear(certification.time)).SetFontColor(ColorConstants.WHITE));
            }

            document2.Add(sectionTitle("Awards").SetFontColor(ColorConstants.WHITE));
            document2.Add(ls2);

            foreach (string award in request.award)
            {
                document2.Add(bodyInfo(award).SetFontColor(ColorConstants.WHITE));
            }

            //document2.Close();
            document.Close();
            pdf.Close();

            var bytes = ms.ToArray();
            var base64 = Convert.ToBase64String(bytes);
            return new DefaultResponse()
            {
                statusCode = StatusCodes.Status200OK,
                result = new Result() { base64 = base64 }
            };
        }

        public Paragraph nameHeader(string text)
        {
            return new Paragraph(text)
                .SetTextAlignment(TextAlignment.JUSTIFIED).SetWidth(340)
                .SetFontSize(headerName).SetFont(regular).SetFixedLeading(headerName);
        }

        public Paragraph headerSubTitle(string text)
        {
            return new Paragraph(text)
                .SetTextAlignment(TextAlignment.JUSTIFIED).SetWidth(340)
                .SetFontSize(subHeader).SetFont(regular).SetFixedLeading(subHeader);
        }

        public Paragraph sectionTitle(string text)
        {
            return new Paragraph(text)
                .SetTextAlignment(TextAlignment.JUSTIFIED).SetWidth(340)
                .SetFontSize(titleSection).SetFont(regular).SetFixedLeading(titleSection);
        }

        public Paragraph historyTitle(string text, string text2)
        {
            Color myColor = new DeviceRgb(187, 191, 198);
            Text position = new Text(text);
            Text location = new Text(text2);//.SetTextAlignment(TextAlignment.RIGHT);
            //Paragraph p = new Paragraph().Add().Add(" ").Add().SetFontSize(subTitle);
            return new Paragraph().SetWidth(340).Add(position).Add(" | ").Add(location)
                .SetFontSize(subTitle).SetFont(medium).SetFontColor(myColor).SetFixedLeading(titleHistory);
        }

        public Paragraph startEnd(string text)
        {
            return new Paragraph(text)
                .SetTextAlignment(TextAlignment.JUSTIFIED).SetWidth(340)
                .SetFontSize(titleHistory).SetFont(medium).SetFixedLeading(subTitle);
        }

        public Paragraph bodyInfo(string text)
        {
            return new Paragraph(text)
                .SetTextAlignment(TextAlignment.LEFT).SetWidth(340)
                .SetFontSize(body).SetFont(light).SetFixedLeading(body + 5);
        }
        public Image iconImage(string url, string altText)
        {
            WebClient client = new WebClient();
            byte[] iconBytes = client.DownloadData(url);
            return new Image(ImageDataFactory.Create(iconBytes)).ScaleAbsolute(14, 14).SetPaddingBottom(-50);//iconImage.SetAlt(altText);
        }
        public Paragraph iconText(string url, string altText, string info)
        {
            return new Paragraph().Add(iconImage(url, altText)).Add(" ").Add(info).SetTextAlignment(TextAlignment.JUSTIFIED).SetWidth(340)
                .SetFontSize(body).SetFont(light).SetFixedLeading(body); 
        }

        public Paragraph star(int level, int size)
        {
            Paragraph stars = new Paragraph();
            for (int i = 0; i < size; i++)
            {
                if (i < level)
                {
                    stars.Add(new Text("\u2022").SetFontColor(ColorConstants.WHITE));
                }
                else
                {
                    stars.Add(new Text("\u2022").SetFontColor(ColorConstants.GRAY));
                }// Inserts a yellow star in the middle
            }

            return stars.SetFontSize(headerName).SetTextAlignment(TextAlignment.JUSTIFIED).SetFixedLeading(0);
        }
    }
}
