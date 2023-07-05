using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ticari_otomasyon
{
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        SqlBaglantisi con = new SqlBaglantisi();

        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_URUNLER", con.baglanti());
            da.Fill(dt);
            dtgUrunler.DataSource = dt;
        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            txtId.Text = dr["ID"].ToString();
            txtAd.Text = dr["URUNAD"].ToString();
            txtMarka.Text = dr["MARKA"].ToString();
            txtModel.Text = dr["MODEL"].ToString();
            mskYil.Text = dr["YIL"].ToString();
            nudAdet.Value = decimal.Parse(dr["ADET"].ToString());
            txtAlisFiyat.Text = dr["ALISFIYAT"].ToString();
            txtSatisFiyat.Text = dr["SATISFIYAT"].ToString();
            rchDetay.Text = dr["DETAY"].ToString();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutEkle =
                new SqlCommand(
                    "INSERT INTO TBL_URUNLER (URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) VALUES(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)",
                    con.baglanti()
                );

            try
            {
                komutEkle.Parameters.AddWithValue("@p1", txtAd.Text);
                komutEkle.Parameters.AddWithValue("@p2", txtMarka.Text);
                komutEkle.Parameters.AddWithValue("@p3", txtModel.Text);
                komutEkle.Parameters.AddWithValue("@p4", mskYil.Text);
                komutEkle.Parameters.AddWithValue("@p5", int.Parse((nudAdet.Value).ToString()));
                komutEkle.Parameters.AddWithValue("@p6", decimal.Parse(txtAlisFiyat.Text));
                komutEkle.Parameters.AddWithValue("@p7", decimal.Parse(txtSatisFiyat.Text));
                komutEkle.Parameters.AddWithValue("@p8", rchDetay.Text);

                komutEkle.ExecuteNonQuery();
                MessageBox.Show("Ürün sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürün eklenemedi:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.baglanti().Close();
                Listele();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSil = new SqlCommand("DELETE FROM TBL_URUNLER WHERE ID=@p1", con.baglanti());

            try
            {
                MessageBox.Show("Silmek istediğinizden emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (MessageBoxButtons.YesNo.Equals(1))
                {
                    komutSil.Parameters.AddWithValue("@p1", txtId.Text);

                    komutSil.ExecuteNonQuery();
                    MessageBox.Show("Ürün sistemden silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürün silinemedi:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.baglanti().Close();
                Listele();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand(
                "UPDATE TBL_URUNLER SET URUNAD=@p1,MARKA=@p2,MODEL=@p3,YIL=@p4,ADET=@p5,ALISFIYAT=@p6,SATISFIYAT=@p7,DETAY=@p8 WHERE ID=@p9", con.baglanti());
            try
            {
                komutGuncelle.Parameters.AddWithValue("@p1", txtAd.Text);
                komutGuncelle.Parameters.AddWithValue("@p2", txtMarka.Text);
                komutGuncelle.Parameters.AddWithValue("@p3", txtModel.Text);
                komutGuncelle.Parameters.AddWithValue("@p4", mskYil.Text);
                komutGuncelle.Parameters.AddWithValue("@p5", int.Parse((nudAdet.Value).ToString()));
                komutGuncelle.Parameters.AddWithValue("@p6", decimal.Parse(txtAlisFiyat.Text));
                komutGuncelle.Parameters.AddWithValue("@p7", decimal.Parse(txtSatisFiyat.Text));
                komutGuncelle.Parameters.AddWithValue("@p8", rchDetay.Text);
                komutGuncelle.Parameters.AddWithValue("@p9", txtId.Text);

                komutGuncelle.ExecuteNonQuery();
                MessageBox.Show("Ürün güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürün güncellenemedi:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.baglanti().Close();
                Listele();
            }
        }
    }
}
