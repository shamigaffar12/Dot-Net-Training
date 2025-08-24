using System; using System.Data.SqlClient; using System.Configuration;
public partial class GenerateBill : System.Web.UI.Page
{
    protected void btnGen_Click(object s, EventArgs e)
    {
        string cons=txtConsumer.Text.Trim(); int units = int.TryParse(txtUnits.Text, out units)? units:0;
        // simple calc using tariffs from TariffSlabs (beginner approach)
        decimal amount = 0;
        string conn = ConfigurationManager.ConnectionStrings["EbDb"].ConnectionString;
        using(var c=new SqlConnection(conn)){ c.Open(); var cmd=new SqlCommand("SELECT MinUnits,MaxUnits,Rate FROM TariffSlabs ORDER BY SlabId",c); var r=cmd.ExecuteReader();
            while(r.Read()){ int min = Convert.ToInt32(r[0]); object maxObj = r[1]; int max = maxObj==DBNull.Value? int.MaxValue: Convert.ToInt32(maxObj); decimal rate = Convert.ToDecimal(r[2]);
                if (units>min){ int consumed = Math.Min(units,max)-min; if(consumed>0) amount += consumed*rate; }
            }
        }
        using(var c=new SqlConnection(conn)){ c.Open(); var cmd=new SqlCommand("INSERT INTO ElectricityBill(consumer_number,consumer_name,units_consumed,bill_amount,bill_date,status) VALUES(@c,@n,@u,@a,GETDATE(),'Unpaid')",c);
            cmd.Parameters.AddWithValue("@c",cons); cmd.Parameters.AddWithValue("@n", cons+" (user)"); cmd.Parameters.AddWithValue("@u", units); cmd.Parameters.AddWithValue("@a", amount); cmd.ExecuteNonQuery(); }
        litMsg.Text = "<div class='text-success'>Bill generated: ₹"+amount+"</div>";
    }
}