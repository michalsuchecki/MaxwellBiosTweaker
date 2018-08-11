using System.Windows.Forms;

internal class LvClocks : ListView
{
    public LvClocks()
    {
        this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        this.SetStyle(ControlStyles.EnableNotifyMessage, true);
    }

    protected override void OnNotifyMessage(Message m)
    {
        if (m.Msg == 20)
            return;

        base.OnNotifyMessage(m);
    }
}
