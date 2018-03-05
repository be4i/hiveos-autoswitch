
using System.ComponentModel.DataAnnotations;

namespace HiveOsAutomation
{
    public enum eMinerSoftware
    {
        [Display(Name = "claymore")]
        Claymore,

        [Display(Name = "dstm")]
        Dstm,

        [Display(Name = "bminer")]
        Bminer,

        [Display(Name = "ccminer")]
        Ccminer,

        [Display(Name = "claymore-x")]
        ClaymoreX,

        [Display(Name = "claymore-z")]
        ClaymoreZ,

        [Display(Name = "ethminer")]
        Ethminer,

        [Display(Name = "ewbf")]
        Ewbf,

        [Display(Name = "sgminer-gm")]
        SgminerGm,

        [Display(Name = "xmrig")]
        Xmrig,

        [Display(Name = "xmr-stack")]
        XmrStack,

        [Display(Name = "xmr-stack-cpu")]
        XmrStackCpu
    }
}