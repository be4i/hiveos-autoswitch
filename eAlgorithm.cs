using System.ComponentModel.DataAnnotations;

namespace HiveOsAutomation
{
    public enum eAlgorithm
    {
        [Display(Name = "eth")]
        Ethash,
        [Display(Name = "gro")]
        Groestl,
        [Display(Name = "x11g")]
        X11Gost,
        [Display(Name = "cn")]
        CryptoNight,
        [Display(Name = "eq")]
        Equihash,
        [Display(Name = "lrev2")]
        Lyra2REv2,
        [Display(Name = "ns")]
        NeoScrypt,
        [Display(Name = "lbry")]
        LBRY,
        [Display(Name = "bk14")]
        Blake14r,
        [Display(Name = "pas")]
        Pascal,
        [Display(Name = "skh")]
        Skunkhash,
        [Display(Name = "n5")]
        NIST5,
        [Display(Name = "l2z")]
        Lyra2z
    }
}