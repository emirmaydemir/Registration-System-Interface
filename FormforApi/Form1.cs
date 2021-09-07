using Newtonsoft.Json;
using RestSharp;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace FormforApi
{
    public partial class Form1 : Form
    {
        string result;
        string result2;
        string result3;
        int a = 0;
        int sayac1 = 0;
        int sayac2 = -1;
        string tablo_ders;
        string tablo_hoca;
        string tablo_kod;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           fonk();
           fillcombo();
        }
        private void fonk()
        {
            /*using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8095/");
                HttpResponseMessage res = await client.GetAsync("api/Default/Get");
                result = await res.Content.ReadAsStringAsync();
                dataGridView1.DataSource = JsonConvert.DeserializeObject<List<Stay>>(result);

                //dataGridView1.DataSource = result.ToList();
                //doldur();
            }*/
            var client = new RestClient("http://localhost:8095/");
            var request = new RestRequest("api/Default/Get", Method.GET);
            var queryResult = client.Execute<List<Stay>>(request).Data;
            dataGridView1.DataSource = queryResult;
        }

        public async void fillcombo()
        {
            // using (var client = new HttpClient())
            // {
            //client.BaseAddress = new Uri("http://localhost:8095/");
            // HttpResponseMessage res = await client.GetAsync("api/Default/GetDersAdi");
            //result = await res.Content.ReadAsStringAsync();

            var client = new RestClient("http://localhost:8095/");
            var request = new RestRequest("api/Default/GetDersAdi", Method.GET);
            var queryResult = client.Execute<List<Stay>>(request).Data;
            //JsonConvert.DeserializeObject<List<Stay>>(result)

            foreach (var item in queryResult)
                {
                    comboBox1.Items.Add(item.DersAdi);
                }
           // }
        }

        /* public void doldur()
         {
             listele();
             int a = 0;
             int sayac1 = 0;
             int sayac2 = -1;
             dataGridView1.Rows.Clear();
             dataGridView1.Columns.Clear();
             result = result.Replace("[", "");
             result = result.Replace("]", "");
             result = result.Replace("\"", "");
             List<string> l = new List<string>();
             l.Add("Dersler"); l.Add("Kod"); l.Add("Hoca"); l.Add("Kredi"); l.Add("Saat");
             dataGridView1.Columns.Add("Dersler", "Dersler"); dataGridView1.Columns.Add("Kod", "Kod"); dataGridView1.Columns.Add("Hoca", "Hoca"); dataGridView1.Columns.Add("Kredi", "Kredi"); dataGridView1.Columns.Add("Saat", "Saat");

             foreach (string item in result.Split(','))
             {
                 a++;
                 if (a % 5 == 0)
                 {
                     dataGridView1.Rows.Add();
                 }
             }

             foreach (string item in result.Split(','))
             {
                 sayac2++;
                 dataGridView1.Rows[sayac1].Cells[l[sayac2]].Value = item;
                 if (sayac2 == 4)
                 {
                     sayac2 = -1;
                     sayac1++;
                 }
             }
         }*/

        /* public async void listele()
         {
             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri("http://localhost:8095/");
                 HttpResponseMessage res = await client.GetAsync("api/Default");
                 result = await res.Content.ReadAsStringAsync();
                 //dataGridView1.DataSource = result.ToList();
             }
         }
        */

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Ders_btn_Click(object sender, EventArgs e)
        {
            string ders = Convert.ToString(comboBox1.Text);
            string dersKodu = Convert.ToString(comboBox2.Text);
            string kredi = Convert.ToString(Kredi.Text);
            string saat = Convert.ToString(Saat.Text);
            string hoca = Convert.ToString(comboBox3.Text);

            Stay s = new Stay();
            s.DersAdi = ders;
            s.DersKodu = dersKodu;
            s.Kredi = kredi;
            s.Saat = saat;
            s.Hoca = hoca;

            /*using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8095/");
                // client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(
                //new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //var res = client.PostAsync("api/Default", s).Result;,
                var json = JsonConvert.SerializeObject(s);
                var asd = new JavaScriptSerializer().Serialize(s);
                var res = client.PostAsync("api/Default/Post", new StringContent(asd
                     , Encoding.UTF8, "application/json")).Result;
                string resultContent =  res.Content.ReadAsStringAsync().Result;
                MessageBox.Show(resultContent);
                //dataGridView1.DataSource = result.ToList();
                //doldur();
                fonk();
            }*/

            var client = new RestClient("http://localhost:8095/");
            var request = new RestRequest("api/Default/Post", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new Stay
            {
            DersAdi = ders,
            DersKodu = dersKodu,
            Kredi = kredi,
            Saat = saat,
            Hoca = hoca
        });
            client.Execute(request);
            fonk();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["DersAdi"].FormattedValue.ToString();
            comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["DersKodu"].FormattedValue.ToString();
            comboBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["Hoca"].FormattedValue.ToString();
            Kredi.Text = dataGridView1.Rows[e.RowIndex].Cells["Kredi"].FormattedValue.ToString();
            Saat.Text = dataGridView1.Rows[e.RowIndex].Cells["Saat"].FormattedValue.ToString();
        }

        private async void Sil_Click(object sender, EventArgs e)
        {
            string ders = Convert.ToString(comboBox1.Text);
            string dersKodu = Convert.ToString(comboBox2.Text);
            string kredi = Convert.ToString(Kredi.Text);
            string saat = Convert.ToString(Saat.Text);
            string hoca = Convert.ToString(comboBox3.Text);

           /* Stay s2 = new Stay();
            //s.Kredi = kredi;
            //s.Saat = saat;
            s2.Hoca = hoca;*/

            

           // using (var client = new HttpClient())
           // {
                /*client.BaseAddress = new Uri("http://localhost:8095/");
                // client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(
                //new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //var res = client.PostAsync("api/Default", s).Result;
                //await client.DeleteAsync("api/Default" , s2);
                //var res = client.DeleteAsync("api/Default", new StringContent(
                //      new JavaScriptSerializer().Serialize(s2), Encoding.UTF8, "application/json")).Result;
                // string resultContent = res.Content.ReadAsStringAsync().Result;
                //MessageBox.Show(response.ToString());
                //dataGridView1.DataSource = result.ToList();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri("http://localhost:8095/api/Default/Delete"),
                    Content = new StringContent(JsonConvert.SerializeObject(s2), Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);*/
               
                var c = new RestClient("http://localhost:8095/");
                var request = new RestRequest("api/Default/Delete", Method.DELETE);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new Stay
                {
                    DersAdi = ders,
                    DersKodu = dersKodu,
                    Kredi = kredi,
                    Saat = saat,
                    Hoca = hoca
                });
                c.Execute(request);
                fonk();

            //}



        }

        private void Güncelle_Click(object sender, EventArgs e)
        {
            string ders = Convert.ToString(comboBox1.Text);
            string dersKodu = Convert.ToString(comboBox2.Text);
            string kredi = Convert.ToString(Kredi.Text);
            string saat = Convert.ToString(Saat.Text);
            string hoca = Convert.ToString(comboBox3.Text);
            /*
                        Stay s3 = new Stay();
                        s3.DersAdi = ders;
                        s3.DersKodu = dersKodu;
                        s3.Kredi = kredi;
                        s3.Saat = saat;
                        s3.Hoca = hoca;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri("http://localhost:8095/");


                            var res = client.PutAsync("api/Default/Put", new StringContent(
                                     new JavaScriptSerializer().Serialize(s3), Encoding.UTF8, "application/json")).Result;
                            string resultContent = res.Content.ReadAsStringAsync().Result;
                            MessageBox.Show(resultContent);
                            //dataGridView1.DataSource = result.ToList();
                            fonk();
                        }*/

            var c = new RestClient("http://localhost:8095/");
            var request = new RestRequest("api/Default/Put", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new Stay
            {
                DersAdi = ders,
                DersKodu = dersKodu,
                Kredi = kredi,
                Saat = saat,
                Hoca = hoca
            });
            c.Execute(request);
            fonk();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            string bul = comboBox1.SelectedItem.ToString();
            //Stay sa = new Stay();
            //sa.DersAdi=bul;

            // using (var client = new HttpClient())
            //  {
            //  client.BaseAddress = new Uri("http://localhost:8095/");
            //  HttpResponseMessage res2 = await client.GetAsync("api/Default/GetDersKodu/"+bul);
            //  result2 = await res2.Content.ReadAsStringAsync();
            // dataGridView1.DataSource = JsonConvert.DeserializeObject<List<Stay>>(result);
            //  HttpResponseMessage res3 = await client.GetAsync("api/Default/GetHoca/"+bul);
            //  result3 = await res3.Content.ReadAsStringAsync();
            //JsonConvert.DeserializeObject<List<Stay>>(result2);
            // }

            var client = new RestClient("http://localhost:8095/");
            var request = new RestRequest("api/Default/GetDersKodu/"+bul, Method.GET);
            var queryResult = client.Execute<List<Stay>>(request).Data;
            var request2 = new RestRequest("api/Default/GetHoca/"+ bul, Method.GET);
            var queryResult2 = client.Execute<List<Stay>>(request2).Data;

            //JsonConvert.DeserializeObject<List<Stay>>(result2)
            //JsonConvert.DeserializeObject<List<Stay>>(result3)

            foreach (var item in queryResult)
            {
                comboBox2.Items.Add(item.DersKodu);
            }

           foreach (var item in queryResult2)
            {
                comboBox3.Items.Add(item.Hoca);
            }
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            string aranan = textBox1.Text;

            /*using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8095/");
                HttpResponseMessage res = await client.GetAsync("api/Default/GetFind/"+aranan);
                result = await res.Content.ReadAsStringAsync();
                dataGridView1.DataSource = JsonConvert.DeserializeObject<List<Stay>>(result);

                //dataGridView1.DataSource = result.ToList();
                //doldur();
            }*/

            if (aranan == "")
            {
                fonk();
            }
            else
            {
                var client = new RestClient("http://localhost:8095/");
                var request = new RestRequest("api/Default/GetFind/" + aranan, Method.GET);
                var queryResult = client.Execute<List<Stay>>(request).Data;
                dataGridView1.DataSource = queryResult;
            }
            
        }
    }

    public class Stay
    {
        public string DersAdi { get; set; }
        public string DersKodu { get; set; }
        public string Kredi { get; set; }
        public string Saat { get; set; }
        public string Hoca { get; set; }

    }
}
