using System.Text;
using Jay.Goldfisher.Extensions;

namespace Jay.Goldfisher.Fishers;

public class Results
{
    public int BelcherWin { get; set; }
    public int BelcherDrop { get; set; }
    public int[] Empty { get; set; }

    public Results()
    {
        BelcherWin = 0;
        BelcherDrop = 0;
        Empty = new int[20];
    }

    public void Add(Results results)
    {
        this.BelcherWin += results.BelcherWin;
        this.BelcherDrop += results.BelcherDrop;
        for (var i = 0; i < 20; i++)
        {
            this.Empty[i] += results.Empty[i];
        }
    }

    public override string ToString()
    {
        var totals = BelcherWin + BelcherDrop;
        Empty.ToList().ForEach(e => totals += e);

        var text = new StringBuilder();
        text.AppendLine("Belcher win: {0}%".FormatWith(100*BelcherWin/totals));
        text.AppendLine("Belcher drop: {0}%".FormatWith(100*BelcherDrop/totals));
        text.AppendLine("Empty the Warrens:");
        for (var i = 0; i < 20; i++)
        {
            text.AppendLine("{0}: {1}%".FormatWith(i*2, 100*Empty[i]/ totals));
        }

        return text.ToString();
    }
}