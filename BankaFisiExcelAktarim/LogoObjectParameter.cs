using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaFisiExcelAktarim 
{
    public class LogoObjectParameter
    {
        public int ReplicMode { get; set; } //CreatedBy, Data_SiteID gibi sistemin kendi set ettiği alanlara, sistemin bulduğu değerleri değil, bizim set etmek istediğimiz değerleri göndermek için kullandığımız metodtur.
        public int CheckParams { get; set; } //Bazı data nesnelerinde parametrelerin (örneğin Ambar Parametrelerinin) kontrol edilmesini engeller. 1 set edildiğinde kontroller yapılmaz, 0 set edildiğinde öndeğer haliyle, yani kontrol yapılacak şekilde çalışacaktır.
        public int CheckRight { get; set; } //Bazı data nesnelerinde yetki kontrolünü engellemek için kullanılır.1 set edildiğinde yetki kontrolleri yapılmaz, 0 set edildiğinde ise öndeğer haliyle, yani yetki kontrolü yapılacak şekilde çalışacaktır
        public int ApplyCampaign { get; set; } //"Kampanya uygula" işlemini yapar.
        public int ApplyCondition { get; set; } //"Satış Koşulu uygula" işlemini yapar.
        public int FillAccCodes { get; set; } //Bazı data nesneleri için muhasebe kodlarını otomatik doldurmak için kullanılır.
        public int FormSeriLotLines { get; set; } //Seri/Lot bilgilerini otomatik set etmek için kullanılır. Eğer Tiger Plus veya Tiger Enterprise içerisinde Seri/Lot bilgilerinin doldurulması konusunda uygun parametreler verilmiş ise kullanılabilir. 
        public int GetStockLinePrice { get; set; } // Son satırın istenilen fiyat bilgisini otomatik set etmek için kullanılır. Logo Objects tarafındaki karşılığı son satır için çalışmaktadır. Dolayısıyla bu parametre set edilir ise sadece son satır için çalışacaktır. Dolayısıyla içeriye veri göndermekten ziyade CalculateDataObject ile bir malzemenin fiyatını öğrenmek için kullanılması uygundur. 0 (sıfır) set edildiğinde işlem yapmayacaktır. Alabileceği değerler için tıklayınız.
        public int ExportAllData { get; set; } //Bir data nesnesi Read ile okunduğunda string değerler için boş, numerik değerler için sıfır değer taşıyan XML alanları gelmemektedir. Bu XML alanlarının da okunan blokta yer alması isteniyorsa bu parametre 1 set edilmeli.
        public int Validation { get; set; } //WCF servis geri planda Logo Objects'i kullanır ve Logo Objects veri aktarımında birçok Validasyon işlemi gerçekleştirir. Ancak bazı durumlarda validasyon işlemini geliştirici yapmak isteyebilir. Bu durumda bu parametre 0 set edildiğinde Logo Objects validasyon yapmaz. Oldukça dikkatli kullanılmalıdır. Doğru kullanılmadığında veri bütünlüğü bozulabilir.
        public int CheckApproveDate { get; set; } // Aktarım esnasında onaylama tarihlerinin kontrol edilmemesini istiyorsak bu parametre 0 set edilmelidir.
        public int Period { get; set; } //Aktarım işleminin yapılacağı dönemi belirtmek için kullanılır. Öndeğeri 0 (sıfır) olan bu parametre, set edilmediğinde ilgili firmanın öndeğer döneminde işlem yapmaktadır. Öndeğer dönem dışındaki bir dönemde işlem yapılmak isteniyorsa bu XML tag'ine istenilen dönemin numarası yazılmalıdır.
    }

}
