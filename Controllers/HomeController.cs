using DataAccesLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BillApplication.Controllers
{
    public class HomeController : Controller
    {
        BillDBEntities dbContext = new BillDBEntities();

        public ActionResult Index()
        {
            return View(dbContext.BillTemplate.OrderByDescending(o => o.ID).ToList());
        }
        public ActionResult UploadBillTemplate()
        {
            return View("UploadBillTemplate");
        }
        [HttpPost]
        public ActionResult UploadBillTemplate(BillTemplate bt, HttpPostedFileBase TemplatePath)
        {
            List<BillTemplate> isInUsePasifeCekilecekListe;
            bool durum = false;

            if (TemplatePath != null)
            {
                string DosyaAdi = Path.GetFileNameWithoutExtension(TemplatePath.FileName);
                string uzanti = Path.GetExtension(TemplatePath.FileName);
                if (uzanti == ".csv")
                {
                    DosyaAdi = DosyaAdi + " - " + DateTime.Now.ToString("dd.MM.yyyy") + uzanti;
                    bt.TemplatePath = "/files/" + DosyaAdi;
                    if (Directory.Exists(Server.MapPath("/files")) == false)
                        Directory.CreateDirectory(Server.MapPath("/files"));
                    DosyaAdi = Path.Combine(Server.MapPath("/files/"), DosyaAdi);
                    TemplatePath.SaveAs(DosyaAdi);
                    isInUsePasifeCekilecekListe = dbContext.BillTemplate.Where(o => o.IsInUse == true).ToList();
                    for (int i = 0; i < isInUsePasifeCekilecekListe.Count; i++)
                    {
                        isInUsePasifeCekilecekListe[i].IsInUse = false;
                        dbContext.SaveChanges();
                    }
                    bt.IsActive = true;
                    bt.IsInUse = true;
                    bt.CreatedDate = DateTime.Now;
                    dbContext.BillTemplate.Add(bt);
                    dbContext.SaveChanges();
                    durum = TemplatedenGelenKisileriAyirKaydet();
                    if (durum == false)
                    {
                        return RedirectToAction("Index", "PageNotFound");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "PageNotFound");
                }
            }


            return RedirectToAction("Index");
        }
        public bool TemplatedenGelenKisileriAyirKaydet()
        {
            BillTemplate bt = dbContext.BillTemplate.Where(o => o.IsInUse == true)
                                                    .FirstOrDefault();
            string path = AppDomain.CurrentDomain.BaseDirectory + bt.TemplatePath.Substring(1, bt.TemplatePath.Length - 1).Replace("/", "\\");
            string dy = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"YaziTemplate\YaziText.txt"));
            string yaziText = System.IO.File.ReadAllText(dy, Encoding.UTF8);
            if (yaziText.Length <= 0)
            {

            }
            using (var reader = new StreamReader(path))
            {
                if (Directory.Exists(Server.MapPath("/OlusturulanFaturalar")) == false)
                    Directory.CreateDirectory(Server.MapPath("/OlusturulanFaturalar"));
                while (!reader.EndOfStream)
                {
                    string yazi = yaziText;
                    ContactBill cb = new ContactBill();
                    var values = reader.ReadLine();
                    string[] lines= values.Split(';');
                    cb.BillNumber = lines[0];
                    cb.Name = lines[1];
                    cb.Surname = lines[2];
                    cb.Total = lines[3];
                   
                    string fileName = cb.BillNumber+".txt";
                    cb.DownloadLink = @"/OlusturulanFaturalar/"+fileName;
                    fileName = Path.Combine(Server.MapPath("/OlusturulanFaturalar/"), fileName);
                    yazi = yazi.Replace("{TARIH}", DateTime.Now.ToString("dd/MM/yyyy"));
                    yazi = yazi.Replace("{AD}", cb.Name.ToUpper());
                    yazi = yazi.Replace("{SOYAD}", cb.Surname.ToUpper());
                    yazi = yazi.Replace("{TUTAR}", cb.Total);
                    yazi = yazi.Replace("{FATURANO}", cb.BillNumber);
                    System.IO.File.WriteAllText(fileName, yazi);
                    
                    dbContext.ContactBill.Add(cb);
                    dbContext.SaveChanges();
                }
            }


            return true;
        }
    }
}